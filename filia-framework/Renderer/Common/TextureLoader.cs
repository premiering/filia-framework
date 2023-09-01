using FiliaFramework.Graphics.Resources;
using Silk.NET.OpenGL;
using SixLabors.ImageSharp.Advanced;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Renderer.Common
{
    public class TextureLoader
    {
        public unsafe Graphics.Resources.Texture LoadTexture(string path)
        {
            var img = Image.Load<Rgba32>(path);

            //brain rot from stackoverflow
            var memGroup = img.GetPixelMemoryGroup();
            var memGroupArr = memGroup.ToArray()[0];
            var data = MemoryMarshal.AsBytes(memGroupArr.Span).ToArray();

            var tex = new Graphics.Resources.Texture((uint) img.Width, (uint) img.Height, data.ToArray());
            return tex;
        }
    }
}
