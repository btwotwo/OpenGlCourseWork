using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGl3d.Infrastructure.Shapes
{
    static class Tetrahedron
    {
        public static readonly IReadOnlyList<float> Vertices = new[]
        {  // Vertices of the 4 faces

// FRONT Face

      -1.0f, -1.0f,  1.0f,
       1.0f, -1.0f,  1.0f,
      1.0f,  1.0f,  1.0f,  

//Right face

      -1.0f, -1.0f,  1.0f,
      1.0f,  1.0f, 1.0f,
      -1.0f, -1.0f, -1.0f, 

// Left Face

       1.0f, -1.0f, 1.0f,
      -1.0f, -1.0f, -1.0f,
       1.0f,  1.0f, 1.0f,  

// BOTTOM

      -1.0f, -1.0f, 1.0f,
      -1.0f, -1.0f, -1.0f,
      1.0f, -1.0f,  1.0f

   };

        public static readonly IReadOnlyList<float> Colors = new[]
        {
            1, 0.5f, 0,
            1, 1, 1,
            1, 0, 0,

            0, 0, 0,
            1, 0.3f, 0,
            0, 0, 0.123f,

            0.311f, 0, 0,
            0.3123f, .1231f, .632f,
            .91f, .59f, .34f,

            .43f, .12f, .09f,
            .681f, .1231f, .451f,
            .666f, .555f, 0
        };

        public static readonly int ArrayCount = Vertices.Count / 3;
    }

    public class TetrahedronWrapper : IShapeWrapper
    {
        public void FillElementBufferObject(int buffer)
        {
        }

        public int GetArrayCount()
        {
            return Tetrahedron.ArrayCount;
        }

        public IReadOnlyList<float> GetColors()
        {
            return Tetrahedron.Colors;
        }

        public IReadOnlyList<float> GetVertices()
        {
            return Tetrahedron.Vertices;
        }

        public void Render()
        {
            GL.DrawArrays(PrimitiveType.Triangles, 0, GetArrayCount());
        }
    }
}
