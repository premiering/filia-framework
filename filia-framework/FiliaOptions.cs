using FiliaFramework.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework
{
    public struct FiliaOptions
    {
        public static FiliaOptions Default { get; } = new FiliaOptions("Unnamed application", new Vector2(1600, 900), true, RendererType.OpenGL);

        public string WindowTitle;
        public Vector2 DefaultSize;
        public bool CenterWindow;
        public RendererType RendererType;
        
        public FiliaOptions(string windowTitle, Vector2 defaultSize, bool centerWindow, RendererType rendererType)
        {
            WindowTitle = windowTitle;
            DefaultSize = defaultSize;
            CenterWindow = centerWindow;
            RendererType = rendererType;
        }
    }
}
