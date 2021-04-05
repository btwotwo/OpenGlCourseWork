using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGl3d.Infrastructure.Projection
{
    static class ProjectionUtils
    {
        public static Matrix4 CreateThreePointProjection(float fovy, float aspect, float depthNear, float depthFar)
        {
            var result = Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, depthNear, depthFar);

            result.Row0.W = .1f;
            result.Row1.W = .1f;
            result.Row2.W = -.5f;

            return result;
        }
    }
}
