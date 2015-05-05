using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CrossSectionalPlanePreview
{
    public class Plane
    {
        public Vector3d normal;
        public double offset;
        public Points points;
        public Plane(Vector3d normal, double offset)
        {
            this.normal = normal;
            this.offset = offset;
        }

        public Plane(double a, double b, double c, double d)
        {
            this.normal = new Vector3d(a, b, c);
            this.offset = d;
        }


        public Points getCrossSectionalPoints(Wire wire)
        {
            Points points = new Points();


            wire.wireList.ForEach((v) =>
            {
                if (normal.X * v.from.X + normal.Y * v.from.Y + normal.Z * v.from.Z == offset)
                {
                    points.Add(v.from);
                }
                else if ((offset - normal.X * v.to.X - normal.Y * v.to.Y - normal.Z * v.to.Z) / (normal.X * v.from.X + normal.Y * v.from.Y + normal.Z * v.from.Z - offset) >= 0)
                {
                    double m = (offset - normal.X * v.to.X - normal.Y * v.to.Y - normal.Z * v.to.Z) / (normal.X * v.from.X + normal.Y * v.from.Y + normal.Z * v.from.Z - offset);

                    Vector3d point = (m * v.from + v.to) / (1 + m);
                    bool dist = false;
                    points.pointsList.ForEach((w) =>
                    {
                        if (Vectors.Abs(w.point - point) <= 0.0000001)
                        {
                            dist = true;
                        }
                    });
                    if (dist == false)
                    {
                        points.Add(point);
                    }


                    //Console.WriteLine(point.X + "," + point.Y + "," + point.Z);
                }
            });


            return points;

        }

        private int compareDouble(Point a, Point b)
        {
            if (a.aTan - b.aTan >= 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }

        }
        public void Draw(Wire wire)
        {
            points = getCrossSectionalPoints(wire);
            points.aTan(normal);
            points.pointsList.Sort(compareDouble);
            
            GL.Begin(BeginMode.Polygon);
            if (points.pointsList.Count != 0)
            {
                points.pointsList.ForEach((v) =>
                {
                    GL.Vertex3(v.point);
                });
            }



            GL.End();

        }

        public void Draw(Wire wire, Vector3 color)
        {
            GL.Color3(color);
            Draw(wire);
        }

        public void Print()
        {
            points.pointsList.ForEach((v) =>
            {
                Console.WriteLine(v.point.X + "," + v.point.Y + "," + v.point.Z + "," + v.aTan);
            });
        }

        public void Rotate(double theta, double phi, double psi)
        {
            normal = Vectors.multiMatrix3d(Vectors.rot(theta, phi, psi), normal);
        }
    }

}
