using FiliaFramework.Input;
using FiliaFramework.Renderer.OpenGL;
using Silk.NET.OpenGL;
using Silk.NET.Vulkan;
using Silk.NET.Windowing;

namespace FiliaFramework.Platform.SilkNet
{
    //Covers most mobile and desktop platforms
    public class SilkNetPlatform : Platform
    {
        private IWindow _window;
        private Renderer.Common.Renderer _renderer;
        private SilkNetInputManager _inputManager;

        public SilkNetPlatform(FiliaGame game) : base(game)
        {
        }

        public override Renderer.Common.Renderer CreateGLRenderer()
        {
            return _renderer = new GLRenderer(Game, GL.GetApi(_window));
        }

        public override Renderer.Common.Renderer CreateVulkanRenderer()
        {
            return null;
        }

        public override IInputManager CreateInputManager()
        {
            return _inputManager = new SilkNetInputManager(_window);
        }

        public override void Run()
        {
            var options = WindowOptions.Default;
            options.Size = new Silk.NET.Maths.Vector2D<int>((int) Game.Options.DefaultSize.X, (int) Game.Options.DefaultSize.Y);
            options.Title = Game.Options.WindowTitle;
            _window = Window.Create(options);

            _window.Load += OnLoad;
            _window.Render += OnRender;

            _window.Run();
        }

        private void OnLoad()
        {
            Game.SetupPlatform(this);
            _renderer.Initialise();
            Game.Load();
        }

        private void OnRender(double dt)
        {
            _renderer.RenderFrame((float) dt);
        }
    }
}