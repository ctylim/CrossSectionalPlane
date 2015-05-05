using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CrossSectionalPlanePreview
{
    public class Vec
    {
        public Vector3d from;
        public Vector3d to;

        public Vec(Vector3d from, Vector3d to)
        {
            this.from = from;
            this.to = to;
        }

        public Vec(double from_x, double from_y, double from_z, double to_x, double to_y, double to_z)
        {
            from.X = from_x;
            from.Y = from_y;
            from.Z = from_z;
            to.X = to_x;
            to.Y = to_y;
            to.Z = to_z;
        }

        public void Add(Vector3d offset)
        {
            from += offset;
            to += offset;
           
        }

    }

    public class Pole : Wire
    {
        public double radius;
        public double height;
        public double divisions;

        public Pole(double radius, double height, double divisions)
        {
            this.radius = radius;
            this.height = height;
            this.divisions = divisions;
        }

        //public Vec[] getVec()
        //{

        //}
    }

    public class Cube : Wire
    {
        

        public double width;
        public double depth;
        public double height;

        public Cube(Vector3d origin, double width, double depth, double height)
        {
            this.origin = origin;
            this.width = width;
            this.depth = depth;
            this.height = height;
            Add(getVec());
        }

        

        public Vec[] getVec()
        {
            Vec[] vec = new Vec[12];

            vec[0] = new Vec(new Vector3d(width, depth, height), new Vector3d(width, depth, -height));
            vec[1] = new Vec(new Vector3d(width, depth, height), new Vector3d(width, -depth, height));
            vec[2] = new Vec(new Vector3d(width, depth, height), new Vector3d(-width, depth, height));
            vec[3] = new Vec(new Vector3d(-width, -depth, height), new Vector3d(-width, -depth, -height));
            vec[4] = new Vec(new Vector3d(-width, -depth, height), new Vector3d(-width, depth, height));
            vec[5] = new Vec(new Vector3d(-width, -depth, height), new Vector3d(width, -depth, height));
            vec[6] = new Vec(new Vector3d(width, -depth, -height), new Vector3d(-width, -depth, -height));
            vec[7] = new Vec(new Vector3d(width, -depth, -height), new Vector3d(width, depth, -height));
            vec[8] = new Vec(new Vector3d(width, -depth, -height), new Vector3d(width, -depth, height));
            vec[9] = new Vec(new Vector3d(-width, depth, -height), new Vector3d(width, depth, -height));
            vec[10] = new Vec(new Vector3d(-width, depth, -height), new Vector3d(-width, -depth, -height));
            vec[11] = new Vec(new Vector3d(-width, depth, -height), new Vector3d(-width, depth, height));

            for (int i = 0; i < vec.Count(); i++)
            {
                vec[i].Add(origin);
            }

            return vec;
        }


    }

    public class Wire
    {
        public Vector3d origin;
        public List<Vec> wireList;

        public Wire()
        {
            wireList = new List<Vec>();

        }

        public int getLength()
        {
            return wireList.Count;
        }

        public void Add(Vector3d vec_from, Vector3d vec_to)
        {
            wireList.Add(new Vec(vec_from, vec_to));
        }

        public void Add(Vec[] vec)
        {
            for (int i = 0; i < vec.GetLength(0); i++)
            {
                wireList.Add(vec[i]);
            }
        }

        public void Print()
        {
            wireList.ForEach((v) =>
            {
                Console.WriteLine(v.from.X + "," + v.from.Y + "," + v.from.Z + "," + v.to.X + "," + v.to.Y + "," + v.to.Z);
            });
        }

        public void Draw()
        {


            

            GL.Begin(BeginMode.Lines);
            {
                wireList.ForEach((v) =>
                {
                    GL.Vertex3(v.from);
                    GL.Vertex3(v.to);

                });
                
            }
            
            GL.End();
            
        }

        public void Draw(Vector3 color)
        {
            GL.Color3(color);
            Draw();
        }


        /// <summary>
        /// θ[deg],Φ[deg],ψ[deg]の回転行列
        /// </summary>
        /// <param name="theta">x軸回転</param>
        /// <param name="phi">y軸回転</param>
        /// <param name="psi">z軸回転</param>
        public void Rotate(double theta, double phi, double psi)
        {
            wireList.ForEach((v) =>
            {
                v.from = Vectors.multiMatrix3d(Vectors.rot(theta, phi, psi), v.from);
                v.to = Vectors.multiMatrix3d(Vectors.rot(theta, phi, psi), v.to);
            });
        }
        /// <summary>
        /// 原点中心に拡大縮小
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(double scale)
        {
            wireList.ForEach((v) =>
            {
                v.from *= scale;
                v.to *= scale;
            });
        }
    }


}
