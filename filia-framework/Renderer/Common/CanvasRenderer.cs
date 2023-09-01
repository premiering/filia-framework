using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Renderer.Common
{
    public abstract class CanvasRenderer
    {
        public abstract void BeginCanvasRender();
        public abstract void EndCanvasRender();
    }
}
