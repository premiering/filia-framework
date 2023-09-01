using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Silk.NET.OpenGL;

namespace FiliaFramework.Graphics.Resources
{
    public class Material
    {
        public string Name { get; set; } = "Unknown";
        public Vector4 AmbientColor { get; set; } = Vector4.One;
        public Vector4 DiffuseColor { get; set; } = Vector4.One;
        public Vector4 SpecularColor { get; set; } = Vector4.One;
        public float SpecularHighlights { get; set; } = 1000;//0 - 1000
        public float OpticalDensity { get; set; } = 1.0f;//1 means no bending when light passes through
        public float Dissolve { get; set; } = 0;
        public Texture? DiffuseTexture { get; set; } = null;
        public int IlluminationMode { get; set; } = 2;

        public override string ToString()
        {
            return $"Material{{ Name:{Name}, Ambient:{AmbientColor}, Diffuse:{DiffuseColor}, Specular:{SpecularColor}, SpecHighlights:{SpecularHighlights}, OpticDens:{OpticalDensity}, Dissolve:{Dissolve}, IllumMode:{IlluminationMode}, HasDiffuseText:{DiffuseTexture != null} }}";
        }
    }
}
