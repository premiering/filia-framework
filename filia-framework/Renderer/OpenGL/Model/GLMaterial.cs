using FiliaFramework.Graphics.Resources;
using FiliaFramework.Renderer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Renderer.OpenGL.Model
{
    public class GLMaterial
    {
        public Material Material { get; set; }
        public RenderId DiffuseTexture { get; set; }

        public GLMaterial(Material material, RenderId diffuseTexture) 
        {
            Material = material;
            DiffuseTexture = diffuseTexture;
        }
    }
}
