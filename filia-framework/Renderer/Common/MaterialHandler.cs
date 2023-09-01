using FiliaFramework.Graphics.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Renderer.Common
{
    public abstract class MaterialHandler
    {
        public abstract RenderId UploadMaterial(Material material);//technically not uploaded since they're usually just uniforms, oh well
        public abstract void DisposeMaterial(RenderId rid);
        public abstract void UpdateMaterial(RenderId rid, Material material);
    }
}
