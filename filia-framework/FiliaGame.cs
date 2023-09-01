using FiliaFramework.Input;
using FiliaFramework.Platform;
using FiliaFramework.Renderer.Common;
using FiliaFramework.Renderer.OpenGL;
using Silk.NET.Vulkan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework
{
    public abstract class FiliaGame
    {
        private static FiliaGame _instance;
        public static FiliaGame Instance
        {
            get { return _instance; }
        }

        public Renderer.Common.Renderer Renderer { get; protected set; }
        public IInputManager InputManager { get; protected set; }

        public FiliaOptions Options { get; private set; }

        public FiliaGame(FiliaOptions options)
        {
            _instance = this;
            Options = options;
        }

        public void SetupPlatform(Platform.Platform platform)
        {
            switch (Options.RendererType)
            {
                case FiliaFramework.Renderer.RendererType.OpenGL:
                    Renderer = platform.CreateGLRenderer();
                    break;
            }
            InputManager = platform.CreateInputManager();
        }

        public abstract void Load();
    }
}
