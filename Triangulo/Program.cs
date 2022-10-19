using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;


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

if (!defaultValue)
{
    vector1.x = x1;
    vector1.y = y1;
    vector2.x = x2;
    vector2.y = y2;
    vector3.x = x3;
    vector3.y = y3;
}

var curScale = new Vector2() { x = 1f, y = 1f };

var triangulo = new Triangulo(vector1, vector2, vector3);

Console.WriteLine("É um triângulo válido? " + (triangulo.EhTriangulo() ? "Sim" : "Não"));

if (!triangulo.EhTriangulo())
{
    Console.WriteLine("Não é possível efetuar as operações com um triângulo inválido");
    return;
}
else
{
    Console.WriteLine("Tipo do Triângulo: " + triangulo.GetTipoTriangulo().ToString());
}

triangulo.Scale(curScale);

Console.WriteLine("Tab => Muda o Pivot");
Console.WriteLine("E => Rotaciona para a esquerda no Pivot");
Console.WriteLine("Q => Rotaciona para a direita no Pivot");
Console.WriteLine("A => Move para esquerda");
Console.WriteLine("D => Move para direita");
Console.WriteLine("W => Move para cima");
Console.WriteLine("S => Move para baixo");
Console.WriteLine("Up Arrow => Aumenta a escala no eixo y");
Console.WriteLine("Down Arrow => Diminui a escala no eixo y");
Console.WriteLine("Right Arrow => Aumenta a escala no eixo x");
Console.WriteLine("Left Arrow => Diminui a escala no eixo x");

var nativeWindowSettings = new NativeWindowSettings()
{
    Size = new Vector2i(800, 600),
    Title = "Trabalho matemática e física",
    // This is needed to run on macos
    Flags = ContextFlags.ForwardCompatible,
};

using TrianguloView tutorial = new TrianguloView(triangulo, GameWindowSettings.Default, nativeWindowSettings);
tutorial.Run();

