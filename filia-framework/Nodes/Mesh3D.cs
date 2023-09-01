using FiliaFramework.Graphics.Resources;
using FiliaFramework.Renderer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Nodes
{
    public class Mesh3D : Visual3D
    {
        public Mesh? Mesh { get; set; }
        public RenderId MeshRid { get; protected set; }

        public override void Load()
        {
            base.Load();

            MeshRid = Renderer.MeshHandler.UploadMesh(Mesh);
            //Renderer.LoadMesh3DNode(this);
        }

        public override void OnFrame(float deltaTime)
        {
            base.OnFrame(deltaTime);

            Renderer.SceneRenderer.RenderMesh(MeshRid);
        }
    }
}
