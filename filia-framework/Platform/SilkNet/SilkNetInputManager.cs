using FiliaFramework.Input;
using Silk.NET.Input;
using Silk.NET.Windowing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FiliaFramework.Platform.SilkNet
{
    public class SilkNetInputManager : IInputManager
    {
        private IWindow _window;
        private IInputContext _inputCtx;

        private IMouse? _activeMouse;
        private IKeyboard? _activeKeyboard;

        private HashSet<int> _keysDown = new HashSet<int>();

        public SilkNetInputManager(IWindow window)
        {
            _window = window;

            _inputCtx = _window.CreateInput();
            RegisterMice();
            RegisterKeyboards();
        }

        public void CaptureCursor(bool captured)
        {
            _activeMouse!.Cursor.CursorMode = captured ? CursorMode.Raw : CursorMode.Normal;
        }

        public int GetMouseX()
        {
            return (int) _activeMouse!.Position.X;
        }

        public int GetMouseY()
        {
            return (int) _activeMouse!.Position.Y;
        }

        public bool IsMouseDown(int button)
        {
            return _activeMouse!.IsButtonPressed((MouseButton) button);
        }

        public bool IsMouseUp(int button)
        {
            return !(_activeMouse!.IsButtonPressed((MouseButton)button));
        }

        public bool IsCharDown(char c)
        {
            return _keysDown.Contains(c);
        }

        public bool IsCharUp(char c)
        {
            return !_keysDown.Contains(c);
        }

        public bool IsKeyDown(Input.Key key)
        {
            return !_keysDown.Contains((int) key);
        }

        public bool IsKeyUp(Input.Key key)
        {
            return _keysDown.Contains((int) key);
        }

        private void RegisterMice()
        {
            foreach (var mice in _inputCtx.Mice)
            {
                mice.MouseMove += OnMouseMove;
                mice.MouseDown += OnMouseDown;
                mice.MouseUp += OnMouseUp;
            }
        }

        private void OnMouseMove(IMouse mouse, Vector2 pos)
        {
            _activeMouse = mouse;
        }

        private void OnMouseDown(IMouse mouse, MouseButton mb)
        {
            _activeMouse = mouse;
        }

        private void OnMouseUp(IMouse mouse, MouseButton mb)
        {
            _activeMouse = mouse;
        }

        private void RegisterKeyboards()
        {
            foreach (var keyboard in _inputCtx.Keyboards)
            {
                keyboard.KeyDown += OnKeyDown;
                keyboard.KeyUp += OnKeyUp;
            }
        }

        private void OnKeyDown(IKeyboard keyboard, Silk.NET.Input.Key key, int keyCode)
        {
            _activeKeyboard = keyboard;
            _keysDown.Add((char) key);
        }

        private void OnKeyUp(IKeyboard keyboard, Silk.NET.Input.Key key, int keyCode)
        {
            _activeKeyboard = keyboard;
            _keysDown.Remove((char)key);
        }
    }
}
