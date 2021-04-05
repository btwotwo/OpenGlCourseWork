using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace OpenGl3d
{
    public class Camera
    {
        float _speed;

        Vector3 _position;
        Vector3 _front;
        private Vector3 _right;
        Vector3 _up;
        Matrix4 _view;
        private Vector2 _lastPos;
        private bool _firstMove;

        float _yaw = -MathHelper.PiOver2;
        float _pitch;

        public Camera(Vector3? position = null)
        {
            _speed = 1.5f;

            _position = position ?? new Vector3(0f, 0f, 0f);
            _front = new Vector3(0f, 0f, -1f);
            _up = new Vector3(0f, 1f, 0f);

            _view = Matrix4.LookAt(_position, _position + _front, _up);

            _firstMove = true;
        }


        public void Forward(double? time = .01) => _position += _front * _speed * (float)time;
        public void Backwards(double? time = .01) => _position -= _front * _speed * (float)time;
        public void Left(double? time = .01) => _position -= Vector3.Normalize(Vector3.Cross(_front, _up)) * _speed * (float)time;
        public void Right(double? time = .01) => _position += Vector3.Normalize(Vector3.Cross(_front, _up)) * _speed * (float)time;
        public void Up(double? time = .01) => _position += _up * _speed * (float)time;
        public void Down(double? time = .01) => _position -= _up * _speed * (float)time;

        public void MouseMove(float x, float y)
        {
            const float sensitivity = 0.2f;

            if (_firstMove) // this bool variable is initially set to true
            {
                _lastPos = new Vector2(x, y);
                _firstMove = false;
            }
            else
            {
                // Calculate the offset of the mouse position
                var deltaX = x - _lastPos.X;
                var deltaY = y - _lastPos.Y;
                _lastPos = new Vector2(x, y);

                // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                _yaw += MathHelper.DegreesToRadians(deltaX * sensitivity);
                var angle = MathHelper.Clamp(deltaY * sensitivity, -89, 89f);
                _pitch += MathHelper.DegreesToRadians(angle);

                _front.X = MathF.Cos(_pitch) * MathF.Cos(_yaw);
                _front.Y = MathF.Sin(_pitch);
                _front.Z = MathF.Cos(_pitch) * MathF.Sin(_yaw);

                _front = Vector3.Normalize(_front);
                _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
                _up = Vector3.Normalize(Vector3.Cross(_right, _front));
            }
        }

        public void ResetMouse()
        {
            _firstMove = true;
        }

        public Matrix4 GetViewMatrix() => Matrix4.LookAt(_position, _position + _front, _up);
        public Matrix4 GetProjectionMatrix() => Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), 800 / 600, 0.001f, 100f);
    }
}