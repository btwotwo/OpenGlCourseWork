using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGl3d.Infrastructure.Shapes
{
    static class Octahedron
    {
        public static readonly IReadOnlyList<float> Vertices = new float[]
        {
              1, 0, 0,
              -1, 0, 0,
              0, 1, 0,
              0, -1, 0,
              0, 0, 1,
              0, 0, -1
        };

        public static readonly IReadOnlyList<uint> Indices = new uint[]
        {
              0, 4, 2,
              1, 2, 4,
              0, 3, 4,
              1, 4, 3,
              0, 2, 5,
              1, 5, 2,
              0, 5, 3,
              1, 3, 5
        };

        public static readonly IReadOnlyList<float> Colors = new float[]
        {
            .1f, .512f, .021f,
            .0012f, .5123f, .011f,
            0.6f, 0.4f, 0.3f,
            .001f, .0231f, .001f,
            .0123f, 0, .0023f,
            .1f, 0, 
        };
    }

    public class OctahedronWrapper : IShapeWrapper
    {
        public void FillElementBufferObject(int buffer)
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, buffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Octahedron.Indices.Count * sizeof(uint), (uint[])Octahedron.Indices, BufferUsageHint.DynamicDraw);
        }

        public int GetArrayCount()
        {
            return 0;
        }

        public IReadOnlyList<float> GetColors()
        {
            return Octahedron.Colors;
        }

        public IReadOnlyList<float> GetVertices()
        {
            return Octahedron.Vertices;
        }

        public void Render()
        {
            GL.DrawElements(PrimitiveType.Triangles, Octahedron.Indices.Count, DrawElementsType.UnsignedInt, 0);
        }
    }
}
