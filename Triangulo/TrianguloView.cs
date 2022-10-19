using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class TrianguloView : GameWindow
{
    private int _vertexBufferObject;
    private int _vertexArrayObject;
    private Shader _shader;

    float speed = 0.01f;
    float scaleSpeed = 0.01f;

    Triangulo _triangulo;

    bool tabPressed;

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

        _vertexBufferObject = GL.GenBuffer();

        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

        _vertexArrayObject = GL.GenVertexArray();

        // var vertices = _triangulo.DrawTriangle();

        // GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

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
        var vertices = _triangulo.DrawTriangle();

        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StreamDraw);

        // Bind the VAO
        GL.BindVertexArray(_vertexArrayObject);

        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        SwapBuffers();
    }


    public TrianguloView(Triangulo triangulo, GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
        _triangulo = triangulo;
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
            _triangulo.ChangePivot();
            tabPressed = true;
        }

        if (Keyboard.IsKeyDown(Keys.E))
        {
            _triangulo.LocalRotate(-1);
        }
        else if (Keyboard.IsKeyDown(Keys.Q))
        {
            _triangulo.LocalRotate(1);
        }

        if (Keyboard.IsKeyDown(Keys.A))
        {
            _triangulo.Tranlate(new Vector2() { x = -speed, y = 0 });
        }
        if (Keyboard.IsKeyDown(Keys.D))
        {
            _triangulo.Tranlate(new Vector2() { x = speed, y = 0 });
        }
        if (Keyboard.IsKeyDown(Keys.W))
        {
            _triangulo.Tranlate(new Vector2() { x = 0, y = speed });
        }
        if (Keyboard.IsKeyDown(Keys.S))
        {
            _triangulo.Tranlate(new Vector2() { x = 0, y = -speed });
        }

        if (Keyboard.IsKeyDown(Keys.Up))
            _triangulo.Scale(new Vector2() { x = 1, y = 1 + scaleSpeed });
        if (Keyboard.IsKeyDown(Keys.Down))
            _triangulo.Scale(new Vector2() { x = 1, y = 1 - scaleSpeed });
        if (Keyboard.IsKeyDown(Keys.Left))
            _triangulo.Scale(new Vector2() { x = 1 - scaleSpeed, y = 1 });
        if (Keyboard.IsKeyDown(Keys.Right))
            _triangulo.Scale(new Vector2() { x = 1 + scaleSpeed, y = 1 });
    }
}
