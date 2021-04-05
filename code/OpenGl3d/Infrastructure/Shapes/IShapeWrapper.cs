using System;
using System.Collections.Generic;

namespace OpenGl3d.Infrastructure.Shapes
{
    public interface IShapeWrapper
    {
        IReadOnlyList<float> GetVertices();
        IReadOnlyList<float> GetColors();
        int GetArrayCount();

        void FillElementBufferObject(int buffer);
        void Render();
    }
}
