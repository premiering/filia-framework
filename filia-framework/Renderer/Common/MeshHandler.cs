using FiliaFramework.Graphics.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Renderer.Common
{
    public abstract class MeshHandler
    {
        public abstract RenderId UploadMesh(Mesh mesh);
        public abstract void DisposeMesh(RenderId rid);
        public abstract void UpdateMaterial(RenderId rid, Material material);
    }
}
