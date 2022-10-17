using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Tutorial02;

namespace Tutorial02
{
    public class Tutorial : GameWindow
    {
        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private Shader _shader;

        float speed = 0.01f;
        float scaleSpeed = 0.01f;

        Triangulo triangulo;

        Vector2 curScale;


        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            base.OnLoad();

            _vertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);


            Console.WriteLine("Usar Valores padrao? s = sim");

            string resposta = Console.ReadLine();
            bool defaultValue = resposta == "s" || resposta == "S";

            double x1, x2, x3, y1, y2, y3;
            x1 = x2 = x3 = y1 = y2 = y3 = 0;
            if (!defaultValue)
            {

                Console.WriteLine("Primeiro Ponto");
                Console.WriteLine("X: ");
                x1 = double.Parse(Console.ReadLine());
                Console.WriteLine("Y: ");
                y1 = double.Parse(Console.ReadLine());

                Console.WriteLine("Segundo Ponto");
                Console.WriteLine("X: ");
                x2 = double.Parse(Console.ReadLine());
                Console.WriteLine("Y: ");
                y2 = double.Parse(Console.ReadLine());

                Console.WriteLine("Terceiro Ponto");
                Console.WriteLine("X: ");
                x3 = double.Parse(Console.ReadLine());
                Console.WriteLine("Y: ");
                y3 = double.Parse(Console.ReadLine());

            }

            var vector1 = new Vector2() { x = -0.5, y = -1 };
            var vector2 = new Vector2() { x = 0, y = -0.5 };
            var vector3 = new Vector2() { x = 0.5, y = -1 };
            var pivo = new Vector2() { x = 0, y = 1 };

            if (!defaultValue)
            {
                vector1.x = x1;
                vector1.y = y1;
                vector2.x = x2;
                vector2.y = y2;
                vector3.x = x3;
                vector3.y = y3;
            }

            curScale = new Vector2() { x = 1f, y = 1f };

            triangulo = new Triangulo(vector1, vector2, vector3, pivo);

            triangulo.Scale(curScale);

            triangulo?.DrawTriangle();


            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            GL.EnableVertexAttribArray(0);
            _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");

            _shader.Use();
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            _shader.Use();

            var vertices = triangulo.DrawTriangle();

            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // Bind the VAO
            GL.BindVertexArray(_vertexArrayObject);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            SwapBuffers();
        }

        bool tabPressed;

        public Tutorial(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            var Keyboard = KeyboardState;

            if (tabPressed && !Keyboard.IsKeyDown(Keys.Tab))
            {
                tabPressed = false;
            }
            else if (!tabPressed && Keyboard.IsKeyDown(Keys.Tab))
            {
                triangulo.SetPivo();
                tabPressed = true;
            }

            if (Keyboard.IsKeyDown(Keys.E))
            {
                triangulo.LocalRotate(-1);
            }
            else if (Keyboard.IsKeyDown(Keys.Q))
            {
                triangulo.LocalRotate(1);
            }

            if (Keyboard.IsKeyDown(Keys.A))
            {
                Console.WriteLine("Apertou A");
                triangulo.Tranlate(new Vector2() { x = -speed, y = 0 });
            }
            if (Keyboard.IsKeyDown(Keys.D))
            {
                triangulo.Tranlate(new Vector2() { x = speed, y = 0 });
            }
            if (Keyboard.IsKeyDown(Keys.W))
            {
                triangulo.Tranlate(new Vector2() { x = 0, y = speed });
            }
            if (Keyboard.IsKeyDown(Keys.S))
            {
                triangulo.Tranlate(new Vector2() { x = 0, y = -speed });
            }

            if (Keyboard.IsKeyDown(Keys.Up))
                triangulo.Scale(new Vector2() { x = 1, y = 1 + scaleSpeed });
            if (Keyboard.IsKeyDown(Keys.Down))
                triangulo.Scale(new Vector2() { x = 1, y = 1 - scaleSpeed });
            if (Keyboard.IsKeyDown(Keys.Left))
                triangulo.Scale(new Vector2() { x = 1 - scaleSpeed, y = 1 });
            if (Keyboard.IsKeyDown(Keys.Right))
                triangulo.Scale(new Vector2() { x = 1 + scaleSpeed, y = 1 });
        }
    }
}
