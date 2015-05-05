using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CrossSectionalPlanePreview
{
    
    class BasicShapes
    {

        public static void DrawSquare(double x, double y, double z)
        {
            GL.Begin(BeginMode.TriangleStrip);
            {
                GL.Vertex3(x + 1.0, y + 1.0, z);
                GL.Vertex3(x - 1.0, y + 1.0, z);
                GL.Vertex3(x + 1.0, y - 1.0, z);
                GL.Vertex3(x - 1.0, y - 1.0, z);
            }
            GL.End();
        }

        public static void DrawCubeWire(double x)
        {
            Wire wire = new Wire();

            

            wire.Add(new Vector3d(x, x, x), new Vector3d(x, x, -x));
            wire.Add(new Vector3d(x, x, x), new Vector3d(x, -x, x));
            wire.Add(new Vector3d(x, x, x), new Vector3d(-x, x, x));
            wire.Add(new Vector3d(-x, -x, x), new Vector3d(-x, -x, -x));
            wire.Add(new Vector3d(-x, -x, x), new Vector3d(-x, x, x));
            wire.Add(new Vector3d(-x, -x, x), new Vector3d(x, -x, x));
            wire.Add(new Vector3d(x, -x, -x), new Vector3d(-x, -x, -x));
            wire.Add(new Vector3d(x, -x, -x), new Vector3d(x, x, -x));
            wire.Add(new Vector3d(x, -x, -x), new Vector3d(x, -x, x));
            wire.Add(new Vector3d(-x, x, -x), new Vector3d(x, x, -x));
            wire.Add(new Vector3d(-x, x, -x), new Vector3d(-x, -x, -x));
            wire.Add(new Vector3d(-x, x, -x), new Vector3d(-x, x, x));

            wire.Draw();


        }
    }
}
