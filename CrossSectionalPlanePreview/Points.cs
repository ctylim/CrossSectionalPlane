using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CrossSectionalPlanePreview
{
    public class Point
    {
        public Vector3d point;
        public double aTan;

        public Point(Vector3d point, double aTan)
        {
            this.point = point;
            this.aTan = aTan;
        }

    }
    public class Points
    {
        public List<Point> pointsList;

        public Points()
        {
            pointsList = new List<Point>();
        }

        public void Add(Vector3d point)
        {
            pointsList.Add(new Point(point, 0.0f));
        }
        


        /// <summary>
        /// 重心を求める
        /// </summary>
        public Vector3d gravity()
        {
            Vector3d gravity = new Vector3d();
            pointsList.ForEach((v) =>
            {
                gravity += v.point;
            });
            gravity /= pointsList.Count;
            return gravity;
        }

        public void aTan(Vector3d normal)
        {
            if (pointsList.Count != 0)
            {
                pointsList.First().aTan = 0.0f;
                pointsList.ForEach((v) =>
                {
                    v.aTan = Vectors.aTan(pointsList.First().point, v.point, normal);

                });
            }
        }

        
    }
}
