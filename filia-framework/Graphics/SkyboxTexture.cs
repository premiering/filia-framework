using FiliaFramework;
using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Graphics
{
    /*public class SkyboxTexture : Texture
    {
        public SkyboxTexture(Silk.NET.OpenGL.GL gl, string path, Silk.NET.Assimp.TextureType type = Silk.NET.Assimp.TextureType.TextureTypeNone) : base(gl, path, type)
        {
        }

        public SkyboxTexture(Silk.NET.OpenGL.GL gl, Span<byte> data, uint width, uint height) : base(gl, data, width, height)
        {
        }

        protected override void SetParameters()
        {
            _gl.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)GLEnum.ClampToEdge);
            _gl.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)GLEnum.ClampToEdge);
            _gl.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)GLEnum.LinearMipmapLinear);
            _gl.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)GLEnum.Linear);
            _gl.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureBaseLevel, 0);
            _gl.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMaxLevel, 8);
            _gl.GenerateMipmap(TextureTarget.TextureCubeMap);
        }
    }*/
}
