using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Tutorial02;

namespace Tutorial02
{
    public class Tutorial : GameWindow
    {
        const string TITLE = "Tutorial #2";
        const int WIDTH = 800;
        const int HEIGHT = 600;

        float speed = 0.01f;
        float scaleSpeed = 0.01f;

        public Tutorial() : base(WIDTH, HEIGHT, GraphicsMode.Default, TITLE) { }

        Triangulo triangulo;

        Vector2 curScale;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.1f, 0.1f, 0.1f, 1f);

            var vector1 = new Vector2() { x = -0.5f, y = 0 };
            var vector2 = new Vector2() { x = 0.5f, y = 0 };
            var vector3 = new Vector2() { x = 0, y = 2f };

            var pivo = new Vector2() { x = 0, y = 1 };

            curScale = new Vector2() { x = 0.2f, y = 0.2f };

            triangulo = new Triangulo(vector1, vector2, vector3, pivo);

            triangulo.Scale(curScale);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
        }

        

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            triangulo?.DrawTriangle();

            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[Key.E])
            {
                triangulo.LocalRotate(-1);
            }
            else if (Keyboard[Key.Q])
            {
                triangulo.LocalRotate(1);
            }


            if (Keyboard[Key.A])
            {
                triangulo.Tranlate(new Vector2() { x = -speed, y = 0 });
            }
            if (Keyboard[Key.D])
            {
                triangulo.Tranlate(new Vector2() { x = speed, y = 0 });
            }
            if (Keyboard[Key.W])
            {
                triangulo.Tranlate(new Vector2() { x = 0, y = speed });
            }
            if (Keyboard[Key.S])
            {
                triangulo.Tranlate(new Vector2() { x = 0, y = -speed });
            }

            if (Keyboard[Key.Up])
                triangulo.Scale(new Vector2() { x = 1,y = 1+scaleSpeed } );
            if (Keyboard[Key.Down])
                triangulo.Scale(new Vector2() { x = 1,y = 1-scaleSpeed } );
            if (Keyboard[Key.Left])
                triangulo.Scale(new Vector2() { x = 1-scaleSpeed,y = 1 } );
            if (Keyboard[Key.Right])
                triangulo.Scale(new Vector2() { x = 1+scaleSpeed,y = 1 } );

            
        }

        static void Main(string[] args)
        {
            using (Tutorial tutorial = new Tutorial())
            {
                tutorial.Run();
            }
        }
    }
}
