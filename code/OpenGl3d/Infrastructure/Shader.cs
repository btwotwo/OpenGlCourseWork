using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.IO;
using System.Text;

namespace OpenGl3d
{
    public class Shader : IDisposable
    {
        public int Handle {get; private set;}

        private bool disposedValue = false;

        public Shader(string vertexPath, string fragmentPath)
        {
            int vertexShader, fragmentShader;

            string vertexShaderSource, fragmentShaderSource;

            using(var reader = new StreamReader(vertexPath, Encoding.UTF8))
            {
                vertexShaderSource = reader.ReadToEnd();
            } 

            using(var reader = new StreamReader(fragmentPath, Encoding.UTF8))
            {
                fragmentShaderSource = reader.ReadToEnd();
            }


            vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);

            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);


            GL.CompileShader(vertexShader);
            ValidateErrors(vertexShader);

            GL.CompileShader(fragmentShader);
            ValidateErrors(fragmentShader);

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);

            GL.LinkProgram(Handle);

            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(Handle);
        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }


        public void SetMatrix4(string name, Matrix4 matrix)
        {
            var loc = GL.GetUniformLocation(Handle, name);
            GL.UniformMatrix4(loc, false, ref matrix);
        }

        void ValidateErrors(int shader)
        {
            var log = GL.GetShaderInfoLog(shader);

            if (log != string.Empty)
            {
                Console.WriteLine(log);
            }
        }
    }
} 