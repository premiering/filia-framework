using FiliaFramework.Graphics;
using FiliaFramework.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Renderer.Common
{
    public abstract class Renderer
    {
        private static Renderer _instance;

        public static Renderer Instance
        {
            get { return _instance; }
        }

        protected FiliaGame Game;

        //Non-API specific
        public TextureLoader TextureLoader { get; protected set; }
        public ModelLoader ModelLoader { get; protected set; }

        //API specific
        public MaterialHandler MaterialHandler { get; protected set; }
        public MeshHandler MeshHandler { get; protected set; }
        public SceneRenderer SceneRenderer { get; protected set; }
        public TextureHandler TextureHandler { get; protected set; }

        public Node RootNode { get; set; } = new Node();

        protected Camera? _activeCamera;

        public Renderer(FiliaGame game)
        {
            Game = game;

            TextureLoader = new TextureLoader();
            ModelLoader = new ModelLoader(TextureLoader);
        }

        public abstract void Initialise();
        public abstract void Stop();
        //public abstract void LoadVisual3DNode(Visual3D node);
        //public abstract void LoadMesh3DNode(Mesh3D node);
        public abstract void StartFrame();
        public abstract void EndFrame();

        public void RenderFrame(float deltaTime)
        {
            StartFrame();

            SceneRenderer.BeginSceneRender();

            UpdateAllChildren(deltaTime, RootNode);
            if (_activeCamera != null)
                SceneRenderer.SetActiveCamera(_activeCamera);
            RenderAllChildren(deltaTime, RootNode);

            SceneRenderer.EndSceneRender();

            //Then we would do canvas rendering...

            EndFrame();
        }

        protected void UpdateAllChildren(float deltaTime, Node root)
        {
            foreach (var child in root.GetChildren())
            {
                child.OnUpdate(deltaTime);
                UpdateAllChildren(deltaTime, child);

                if (child is Camera3D)
                {
                    Camera3D camera = (Camera3D) child;
                    if (camera.IsActive)
                        _activeCamera = camera.Camera;//weird nesting here idk tho
                }
            }
        }

        protected void RenderAllChildren(float deltaTime, Node root)
        {
            foreach (var child in root.GetChildren())
            {
                child.OnFrame(deltaTime);
                UpdateAllChildren(deltaTime, child);
            }
        }
    }
}
