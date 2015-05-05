using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CrossSectionalPlanePreview
{
    public class Vectors
    {
        public static Vector3d Cross(Vector3d v1, Vector3d v2)
        {
            return new Vector3d(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);
        }

        public static double Inner(Vector3d v1, Vector3d v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static double Abs(Vector3d vec)
        {
            return Math.Sqrt(Math.Pow(vec.X, 2) + Math.Pow(vec.Y, 2) + Math.Pow(vec.Z, 2));
        }

        public static double Abs2(Vector3d vec)
        {
            return Math.Pow(vec.X, 2) + Math.Pow(vec.Y, 2) + Math.Pow(vec.Z, 2);
        }

        public static Vector3d multiMatrix3d(Matrix3d mat, Vector3d vec)
        {
            return new Vector3d(Inner(mat.Row0, vec), Inner(mat.Row1, vec), Inner(mat.Row2, vec));
        }

        public static Matrix3d multiMatrix3d(Matrix3d mat1, Matrix3d mat2)
        {
            return new Matrix3d(new Vector3d(Inner(mat1.Row0, mat2.Column0), Inner(mat1.Row0, mat2.Column1), Inner(mat1.Row0, mat2.Column2)), new Vector3d(Inner(mat1.Row1, mat2.Column0), Inner(mat1.Row1, mat2.Column1), Inner(mat1.Row1, mat2.Column2)), new Vector3d(Inner(mat1.Row2, mat2.Column0), Inner(mat1.Row2, mat2.Column1), Inner(mat1.Row2, mat2.Column2)));
        }

        public static double aTan(Vector3d vec1, Vector3d vec2, Vector3d normal)
        {
            double sign;

            if (Inner(normal, Cross(vec1, vec2)) >= 0)
            {
                sign = 1;
            }
            else
            {
                sign = -1;
            }

            double x, y;
            y = (Abs2(vec1) * Abs2(vec2) - Math.Pow(Inner(vec1, vec2), 2)) / Abs2(vec1);
            //Console.WriteLine("y=" + y);
            if (y < 0)
            {
                y = 0;
            }
            y = Math.Sqrt(y);
            x = Inner(vec1, vec2) / Abs(vec1);
            if (x == 0)
            {
                return sign * Math.PI / 2;
            }
            

            return sign * Math.Atan2(y, x);
        }

        public static double cos(double angle)
        {
            return Math.Cos(angle);
        }

        public static double sin(double angle)
        {
            return Math.Sin(angle);
        }

        public static Matrix3d rotX(double theta)
        {
            theta *= Math.PI / 180;

            return new Matrix3d(new Vector3d(1, 0, 0), new Vector3d(0, cos(theta), -sin(theta)), new Vector3d(0, sin(theta), cos(theta)));
        }

        public static Matrix3d rotY(double phi)
        {
            phi *= Math.PI / 180;

            return new Matrix3d(new Vector3d(cos(phi), 0, -sin(phi)), new Vector3d(0, 1, 0), new Vector3d(sin(phi), 0, cos(phi)));
        }

        public static Matrix3d rotZ(double psi)
        {
            psi *= Math.PI / 180;

            return new Matrix3d(new Vector3d(cos(psi), -sin(psi), 0), new Vector3d(sin(psi), cos(psi), 0), new Vector3d(0, 0, 1));
        }


        public static Matrix3d rot(double theta, double phi, double psi)
        {

            return multiMatrix3d(rotZ(psi), multiMatrix3d(rotY(phi), rotX(theta)));
        }
    }
}
