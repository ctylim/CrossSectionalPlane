using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

// References
// http://ameblo.jp/nishi-u6fa4/entry-10659712465.html

namespace CrossSectionalPlanePreview
{
    public partial class TKForm : Form
    {
        Wire wire, wire2, wire3;
        Plane plane;
        double scale = 1.0;
        double offset = 0.0;
        double rot_wire = 0.0;
        double rot_plane = 0.0;
        double origin_x = 0.0;

        public TKForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            GL.ClearColor(glControl.BackColor);

            // Projection の設定
            SetProjection();

            // 視界の設定
            Vector3 vec = new Vector3(1.2f, 1.5f, 1.0f);
            //Matrix4 look = Matrix4.LookAt(5.0f * Vector3.UnitZ, Vector3.Zero, Vector3.UnitY);
            Matrix4 look = Matrix4.LookAt(1.0f * vec, Vector3.Zero, Vector3.UnitY);
            GL.LoadMatrix(ref look);

            // デプスバッファの使用
            GL.Enable(EnableCap.DepthTest);
        }

        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // 描画
            wire = new Cube(new Vector3d(origin_x, 0, 0), 0.5, 0.5, 0.5);
            wire.Scale(scale);
            wire.Rotate(0.0, rot_wire, 0.0);
            wire.Draw(new Vector3(1.0f, 0.0f, 0.0f));

            wire2 = new Cube(new Vector3d(0, 0, 0), 0.35, 0.35, 0.35);
            wire2.Scale(scale);
            wire2.Rotate(0.0, rot_wire, 0.0);
            wire2.Draw(new Vector3(1.0f, 0.0f, 0.0f));

            wire3 = new Cube(new Vector3d(0, 0, 0), 0.2, 0.2, 0.2);
            wire3.Scale(scale);
            wire3.Rotate(0.0, rot_wire, 0.0);
            wire3.Draw(new Vector3(1.0f, 0.0f, 0.0f));

            //wire.Print();

            plane = new Plane(new Vector3d(1, 1, 1), offset);
            plane.Rotate(0.0, rot_plane, 0.0);

            plane.Draw(wire3, new Vector3(1.0f, 1.0f, 0.0f));
            plane.Draw(wire2, new Vector3(1.0f, 0.0f, 0.0f));
            plane.Draw(wire, new Vector3(0.0f, 0.0f, 1.0f));
            //plane.Print();


            glControl.SwapBuffers();
            
        }



        private void SetProjection()
        {
            // ビューポートの設定
            GL.Viewport(0, 0, glControl.Width, glControl.Height);

            // 視体積の設定
            GL.MatrixMode(MatrixMode.Projection);
            Matrix4 proj = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver3, glControl.AspectRatio, 0.1f, 10.0f);
            GL.LoadMatrix(ref proj);

            // MatrixMode を元に戻す
            GL.MatrixMode(MatrixMode.Modelview);
        }

        private void glControl_Resize(object sender, EventArgs e)
        {

            // Projection の設定
            SetProjection();

            // 再描画
            glControl.Refresh();
        }

        private void TKForm_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyCode);
            switch (e.KeyCode)
            {
                case Keys.U:
                    scale += 0.05;
                    Console.WriteLine("scale = " + scale);
                    break;
                    
                case Keys.J:
                    scale -= 0.05;
                    if (scale < 0)
                    {
                        scale = 0;
                    }
                    Console.WriteLine("scale = " + scale);
                    break;

                case Keys.I:
                    scale = 1;
                    offset = 0;
                    rot_wire = 0;
                    rot_plane = 0;
                    origin_x = 0.0;
                    Console.WriteLine("Initialized");
                    break;

                case Keys.O:
                    offset += 0.1;
                    Console.WriteLine("offset = " + offset);
                    break;

                case Keys.L:
                    offset -= 0.1;
                    Console.WriteLine("offset = " + offset);
                    break;

                case Keys.R:
                    rot_wire += 5;
                    Console.WriteLine("rot_wire = " + rot_wire);
                    break;

                case Keys.F:
                    rot_wire -= 5;
                    Console.WriteLine("rot_wire = " + rot_wire);
                    break;

                case Keys.Q:
                    rot_plane += 5;
                    Console.WriteLine("rot_plane = " + rot_plane);
                    break;

                case Keys.W:
                    origin_x += 0.05;
                    Console.WriteLine("origin_x = " + origin_x);
                    break;

                case Keys.S:
                    origin_x -= 0.05;
                    Console.WriteLine("origin_x = " + origin_x);
                    break;

                case Keys.Escape:
                    this.Close();
                    break;
            }

            glControl.Refresh();
        }



    }
}
