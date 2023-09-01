using FiliaFramework;
using FiliaFramework.Graphics;
using FiliaFramework.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace FiliaSandbox
{
    public class SandboxGame : FiliaGame
    {
        public SandboxGame(FiliaOptions options) : base(options)
        {

        }

        public override void Load()
        {
            var scene = new Node();
            LoadModelToNode(scene, Renderer.ModelLoader.LoadModel("assets/ac house red.obj"));
            scene.AddChild(new FreeflyCamera());
            Renderer.RootNode = scene;
        }

        private void LoadModelToNode(Node root, Model model)
        {
            foreach (var mesh in model.ModelMeshes)
            {
                Mesh3D mesh3D = new Mesh3D();
                mesh3D.Mesh = mesh;
                root.AddChild(mesh3D);
            }
        }
    }
}
