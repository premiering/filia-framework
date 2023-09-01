using FiliaFramework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Renderer.Common
{
    public abstract class TextureHandler
    {
        public abstract RenderId UploadTexture(Graphics.Resources.Texture texture);
        public abstract void DisposeTexture(RenderId rid);
        public abstract RenderId GetDefaultWhiteTexture();
        //public abstract RenderId GetDefaultBlackTexture();
    }
}
