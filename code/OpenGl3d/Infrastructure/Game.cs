using OpenGl3d.Infrastructure.Projection;
using OpenGl3d.Infrastructure.Shapes;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System.Diagnostics;
using Key = OpenTK.Windowing.GraphicsLibraryFramework.Keys;

namespace OpenGl3d
{
    public class Game : GameWindow
    {
        private readonly Camera _camera;
        private readonly OpenGlHandler _handler;

        public Game(int width, int height, string title, IShapeWrapper shape, Camera camera) : base(new GameWindowSettings(), new NativeWindowSettings()
        {
            Title = title,
            Size = new Vector2i(width, height)
        })
        {
            _camera = camera;
            _handler = new OpenGlHandler(shape, camera);
        }

        protected override void OnLoad()
        {
            _handler.OnLoad();
            CursorGrabbed = true;
            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            _handler.OnRenderFrame();
            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            if (!IsFocused)
                return;

            var input = KeyboardState;

            if (input.IsKeyDown(Key.W))
                _camera.Forward(args.Time);
            if (input.IsKeyDown(Key.S))
                _camera.Backwards(args.Time);
            if (input.IsKeyDown(Key.A))
                _camera.Left(args.Time);
            if (input.IsKeyDown(Key.D))
                _camera.Right(args.Time);
            if (input.IsKeyDown(Key.Space))
                _camera.Up(args.Time);
            if (input.IsKeyDown(Key.LeftShift))
                _camera.Down(args.Time);

            var mouse = MouseState;

            _camera.MouseMove(mouse.X, mouse.Y);

            base.OnUpdateFrame(args);
        }


        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, 800, 600);

            base.OnResize(e);
        }

        protected override void OnUnload()
        {
            _handler.Dispose();
            base.OnUnload();
        }
    }
}