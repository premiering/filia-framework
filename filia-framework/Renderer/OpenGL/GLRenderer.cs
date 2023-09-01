using FiliaFramework.Graphics;
using FiliaFramework.Nodes;
using FiliaFramework.Renderer.Common;
using FiliaFramework.Renderer.OpenGL.Model;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.SDL;
using Silk.NET.Vulkan;
using Silk.NET.Windowing;
using System.Drawing;
using System.Numerics;

namespace FiliaFramework.Renderer.OpenGL
{
    public class GLRenderer : Renderer.Common.Renderer
    {
        public static GL Gl { get; private set; }

        public GLRenderer(FiliaGame game, GL glContext) : base(game)
        {
            Gl = glContext;
        }

        public override void Initialise()
        {
            TextureHandler = new GLTextureHandler();
            MaterialHandler = new GLMaterialHandler((GLTextureHandler) TextureHandler);
            MeshHandler = new GLMeshHandler(MaterialHandler);
            SceneRenderer = new GLSceneRenderer((GLMaterialHandler) MaterialHandler, (GLMeshHandler)MeshHandler, (GLTextureHandler) TextureHandler);
        }

        public override void Stop()
        {
            
        }

        public override void StartFrame()
        {
            //Gl.Viewport(new System.Drawing.Rectangle(0, 0, (int) GetWindowWidth(), (int) GetWindowHeight()));
            Gl.Enable(EnableCap.DepthTest);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Gl.ClearColor(System.Drawing.Color.Black);
        }

        public override void EndFrame()
        {

        }
    }
}
