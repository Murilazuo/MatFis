using System;
using System.Collections.Generic;
using System.Text;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Tutorial02
{
    public struct Vector2
    {
        public double x;
        public double y;
    }

    public enum TipoTriangulo
    {
        Escaleno,
        Isosceles,
        Equilatero = 3,

    }

    public class Triangulo
    {
        TipoTriangulo tipo;
        public Vector2[] pontos;
        double[] arestas;
        Vector2 pivo;
        int currentPivo;

        
        public Triangulo(Vector2 a, Vector2 b, Vector2 c, Vector2 newPivo)
        { //contrutor
            pontos = new Vector2[3];
            arestas = new double[3];

            currentPivo = 0;
            
            pontos[0] = a;
            pontos[1] = b;
            pontos[2] = c;
            
            pivo = a;

            arestas[0] = Distance(a, b);
            arestas[1] = Distance(b, c);
            arestas[2] = Distance(c, a);

            if (arestas[0] >= arestas[1] + arestas[2])
                Console.WriteLine("NAO EH TRIANGULO >:(");
                //throw new Exception("\n\n NAO EH TRIANGULO >:( \n\n");
            if (arestas[1] >= arestas[2] + arestas[0])
                Console.WriteLine("NAO EH TRIANGULO >:(");
            if (arestas[2] >= arestas[1] + arestas[0])
                Console.WriteLine("NAO EH TRIANGULO >:(");

            int ladosIguais = 0;

            if (IsEqual(arestas[0], arestas[1]))
                ladosIguais++;
            if (IsEqual(arestas[1], arestas[2]))
                ladosIguais++;
            if (IsEqual(arestas[0], arestas[2]))
                ladosIguais++;

            tipo = (TipoTriangulo)ladosIguais;

            Console.WriteLine(tipo.ToString());
        }

        public void Tranlate(Vector2 toTranslate)
        {
            pivo.x += toTranslate.x;
            pivo.y += toTranslate.y;


            pontos[0].x += toTranslate.x;
            pontos[1].x += toTranslate.x;
            pontos[2].x += toTranslate.x;

            pontos[0].y += toTranslate.y;
            pontos[1].y += toTranslate.y;
            pontos[2].y += toTranslate.y;

            UpdatePivo();
        }

        public void Scale(Vector2 toScale)
        {
            pontos[0].x *= toScale.x;
            pontos[1].x *= toScale.x;
            pontos[2].x *= toScale.x;

            pontos[0].y *= toScale.y;
            pontos[1].y *= toScale.y;
            pontos[2].y *= toScale.y;

            UpdatePivo();

        }

        public void LocalScale(Vector2 toScale)
        {
            var toTranslate = new Vector2 { x = -pivo.x, y = -pivo.y };
            var toTranslate2 = new Vector2 { x = pivo.x, y = pivo.y };

            Tranlate(toTranslate);

            Scale(toScale);

            Tranlate(toTranslate2);

            UpdatePivo();
        }
        public void Rotate(double angle)
        {
            angle = angle * Math.PI / 180;

            for (int i = 0; i < 3; i++)
            {
                Vector2 pontoOriginal = pontos[i];

                pontos[i].x = (pontoOriginal.x * Math.Cos(angle)) - (pontoOriginal.y * Math.Sin(angle));

                pontos[i].y = (pontoOriginal.x * Math.Sin(angle)) + (pontoOriginal.y * Math.Cos(angle));

            }

            UpdatePivo();
        }

        public void SetPivo()
        {

            if(++currentPivo == pontos.Length)
                currentPivo = 0;
            
            UpdatePivo();
        }

        void UpdatePivo()
        {
            pivo = pontos[currentPivo];
        }

        public void LocalRotate(double angle)
        {
            var toTranslate = new Vector2 { x = -pivo.x, y = -pivo.y };
            var toTranslate2 = new Vector2 { x = pivo.x, y = pivo.y };

            Tranlate(toTranslate);

            Rotate(angle);

            Tranlate(toTranslate2);
        }

        bool IsEqual(double a, double b)
        {
            return a == b;
        }

        double Distance(Vector2 a, Vector2 b)
        {
            double distance = 0;

            var conta = ((b.x - a.x) * (b.x - a.x)) + ((b.y - a.y) * (b.y - a.y));

            distance = Math.Sqrt(conta);

            return distance;
        }

        public void DrawTriangle()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // We start drawing by telling OpenGL what kind of shapes we will be using with the Begin() method.
            // Since we want to draw a rectangle with 4 points, we will use quads for simplicity.
            GL.Begin(BeginMode.Triangles);

            // Each of these 4 lines sets the color and position of a single point. By default, the OpenGL view extends from
            // -1 to 1 both from left to right and bottom to top, regardless of the window size, so by using -0.5 and 0.5, we
            // end up with a rectangle that is exactly 1/4th the size of the screen, and located directly in its center. We
            // don't care about the Z-coordinate since we are creating a rectangle that is perpendicular to our eye, so we
            // use 0.

            GL.Color3(0.5f, 0.5f, 0.5f); GL.Vertex3(pontos[0].x, pontos[0].y, 0);      // red, top left
            GL.Color3(0.5f, 0.5f, 0.5f); GL.Vertex3( pontos[1].x, pontos[1].y, 0);     // green, bottom left
            GL.Color3(0.5f, 0.5f, 0.5f); GL.Vertex3(pontos[2].x, pontos[2].y, 0);      // blue, bottom right
                                                                                           //GL.Color3(1f, 0f, 1f); GL.Vertex3(0.5f, 0.5f, 0);       // purple, top right

            // We end the drawing operation by calling End().
            GL.End();
        }
        
        }
    }
