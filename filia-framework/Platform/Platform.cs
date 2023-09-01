using FiliaFramework.Input;
using FiliaFramework.Platform.SilkNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Platform
{
    public abstract class Platform
    {
        public FiliaGame Game { get; protected set; }

        public Platform(FiliaGame game)
        {
            Game = game;
        }

        //Platform API
        public abstract Renderer.Common.Renderer CreateGLRenderer();
        public abstract Renderer.Common.Renderer CreateVulkanRenderer();
        public abstract IInputManager CreateInputManager();

        public abstract void Run();
    }
}
