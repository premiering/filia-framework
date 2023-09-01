using FiliaFramework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Renderer.Common
{
    public abstract class SceneRenderer
    {
        public abstract void BeginSceneRender();
        public abstract void EndSceneRender();

        //Camera
        public abstract void SetActiveCamera(Camera camera);

        //Meshes
        public abstract void RenderMesh(RenderId renderId);

        //Materials
        public abstract void UseMaterial(RenderId renderId);
    }
}
