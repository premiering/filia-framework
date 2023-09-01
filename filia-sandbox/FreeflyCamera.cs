using FiliaFramework.Graphics;
using FiliaFramework.Input;
using FiliaFramework.Nodes;
using FiliaFramework.Renderer;
using Silk.NET.Assimp;
using Silk.NET.Windowing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FiliaSandbox
{
    public class FreeflyCamera : Camera3D
    {
        private IInputManager _inputManager;

        private Vector2 _position;
        private Vector2 _lastMousePosition;

        public override void Load()
        {
            base.Load();



            _inputManager = Game.InputManager;
        }

        public override void OnFrame(float deltaTime)
        {
            base.OnFrame(deltaTime);

            _inputManager.CaptureCursor(true);

            var moveSpeed = 10f * (float)deltaTime;

            if (_inputManager.IsKeyDown(Key.W))
            {
                //Move forwards
                Camera.CameraPosition -= moveSpeed * Camera.CameraFront;
            }
            if (_inputManager.IsKeyDown(Key.S))
            {
                //Move backwards
                Camera.CameraPosition += moveSpeed * Camera.CameraFront;
            }
            if (_inputManager.IsKeyDown(Key.A))
            {
                //Move left
                Camera.CameraPosition += Vector3.Normalize(Vector3.Cross(Camera.CameraFront, FiliaFramework.Graphics.Camera.CameraUp)) * moveSpeed;
            }
            if (_inputManager.IsKeyDown(Key.D))
            {
                //Move right
                Camera.CameraPosition -= Vector3.Normalize(Vector3.Cross(Camera.CameraFront, FiliaFramework.Graphics.Camera.CameraUp)) * moveSpeed;
            }

            _position = new Vector2(_inputManager.GetMouseX(), _inputManager.GetMouseY());

            var lookSensitivity = 0.1f;
            if (_lastMousePosition == default)
            {
                _lastMousePosition = _position;
            }
            else
            {
                var xOffset = (_position.X - _lastMousePosition.X) * lookSensitivity;
                var yOffset = (_position.Y - _lastMousePosition.Y) * lookSensitivity;
                _lastMousePosition = _position;

                Camera.CameraYaw += xOffset;
                Camera.CameraPitch -= yOffset;

                //We don't want to be able to look behind us by going over our head or under our feet so make sure it stays within these bounds
                Camera.CameraPitch = Math.Clamp(Camera.CameraPitch, -89.0f, 89.0f);

                Camera.CameraDirection.X = MathF.Cos(MathHelper.DegreesToRadians(Camera.CameraYaw)) * MathF.Cos(MathHelper.DegreesToRadians(Camera.CameraPitch));
                Camera.CameraDirection.Y = MathF.Sin(MathHelper.DegreesToRadians(Camera.CameraPitch));
                Camera.CameraDirection.Z = MathF.Sin(MathHelper.DegreesToRadians(Camera.CameraYaw)) * MathF.Cos(MathHelper.DegreesToRadians(Camera.CameraPitch));
                Camera.CameraFront = Vector3.Normalize(Camera.CameraDirection);
            }
        }
    }
}
