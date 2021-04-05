using OpenGl3d.Infrastructure.Shapes;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;

namespace OpenGl3d
{
    public class OpenGlHandler: IDisposable
    {
        IReadOnlyList<float> _color;
        IReadOnlyList<float> _vertices; 
        IShapeWrapper _shape;
        
        Camera _camera;
        Shader _shader;

        int _vertexBufferObject;
        int _vertexArrayObject;
        int _colorBufferObject;

        int _elementBufferObject;

        public OpenGlHandler(IShapeWrapper shape, Camera camera)        
        {
            _shape = shape;

            _vertices = shape.GetVertices();
            _color = shape.GetColors();
            _camera = camera;
        }


        public void SetShape(IShapeWrapper shape)
        {
            _shape = shape;
            _vertices = shape.GetVertices();
            _color = shape.GetColors();
            
            GL.BindVertexArray(_vertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count * sizeof(float), (float[])_vertices, BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _colorBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _color.Count * sizeof(float), (float[])_color, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            _shape.FillElementBufferObject(_elementBufferObject);
  
        }

        public void SetWired(bool wired)
        {
            if (wired)
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            }
            else
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            } 
        }

        public void OnLoad()
        {
            _shader = new("Infrastructure/Shaders/shader.vert", "Infrastructure/Shaders/shader.frag");

            // Color of the window after it gets cleared between frames
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            _vertexBufferObject = GL.GenBuffer();
            _vertexArrayObject = GL.GenVertexArray();
            _colorBufferObject = GL.GenBuffer();
            _elementBufferObject = GL.GenBuffer();

            SetShape(_shape);

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
           
            GL.Enable(EnableCap.DepthTest);

            _shader.Use();
        }

        public void OnRenderFrame()
        {
            // Clears the screen using the color set in OnLoad
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            var persp = new Matrix4(new (1, 0, 0, -0.3f),
                                        new (0, 1, 0, 0.3f),
                                        new (0, 0, 1, 0.3f),
                                        new (0f, 0, 0, 1)
                                   );


            var model = persp;
            persp = _camera.GetProjectionMatrix();
            
            _shader.Use();
   
            _shader.SetMatrix4("model", model);
            _shader.SetMatrix4("view", _camera.GetViewMatrix());
            _shader.SetMatrix4("projection", persp);


            GL.BindVertexArray(_vertexArrayObject);
             _shape.Render();
        }

        public void Dispose()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);

            _shader.Dispose();
        }
    }
}