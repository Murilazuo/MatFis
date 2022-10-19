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

    /// <summary>
    /// Construtor do Triângulo
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    public Triangulo(Vector2 a, Vector2 b, Vector2 c)
    { //contrutor
        SetPontos(a, b, c);
        SetArestas(a, b, c);
        SetTipo();

        currentPivo = 0;
        pivo = a;
    }

    /// <summary>
    /// Atribui as Aretas
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    private void SetArestas(Vector2 a, Vector2 b, Vector2 c)
    {
        arestas = new double[3];

        arestas[0] = Distance(a, b);
        arestas[1] = Distance(b, c);
        arestas[2] = Distance(c, a);
    }

    /// <summary>
    /// Atribui os pontos do Triângulo
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    private void SetPontos(Vector2 a, Vector2 b, Vector2 c)
    {
        pontos = new Vector2[3];

        pontos[0] = a;
        pontos[1] = b;
        pontos[2] = c;
    }

    /// <summary>
    /// Atribui o tipo do triângulo
    /// </summary>
    private void SetTipo()
    {
        int ladosIguais = 0;

        if (IsEqual(arestas[0], arestas[1]))
            ladosIguais++;
        if (IsEqual(arestas[1], arestas[2]))
            ladosIguais++;
        if (IsEqual(arestas[0], arestas[2]))
            ladosIguais++;

        tipo = (TipoTriangulo)ladosIguais;
    }

    /// <summary>
    /// Retorna o tipo do Triângulo
    /// </summary>
    /// <returns></returns>
    public TipoTriangulo GetTipoTriangulo()
    {
        return tipo;
    }

    /// <summary>
    /// Verifica se é um triângulo válido
    /// </summary>
    /// <returns></returns>
    public bool EhTriangulo()
    {
        if (arestas[0] >= arestas[1] + arestas[2])
        {
            return false;
        }
        else if (arestas[1] >= arestas[2] + arestas[0])
        {
            return false;
        }
        else if (arestas[2] >= arestas[1] + arestas[0])
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Efetua a Translação do Triângulo
    /// </summary>
    /// <param name="toTranslate"></param>
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

    /// <summary>
    /// Altera a escala do Triângulo
    /// </summary>
    /// <param name="toScale"></param>
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

    /// <summary>
    /// Altera a escala no pivot pelo valor informado
    /// </summary>
    /// <param name="toScale"></param>
    public void LocalScale(Vector2 toScale)
    {
        var toTranslate = new Vector2 { x = -pivo.x, y = -pivo.y };
        var toTranslate2 = new Vector2 { x = pivo.x, y = pivo.y };

        Tranlate(toTranslate);

        Scale(toScale);

        Tranlate(toTranslate2);

        UpdatePivo();
    }

    /// <summary>
    /// Rotaciona o triângulo
    /// </summary>
    /// <param name="angle"></param>
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

    /// <summary>
    /// Altera o pivot
    /// </summary>
    public void ChangePivot()
    {
        if (currentPivo + 1 == pontos.Length)
        {
            currentPivo = 0;
        }
        else
        {
            currentPivo++;
        }

        UpdatePivo();
    }

    /// <summary>
    /// Atualiza o pivot
    /// </summary>
    public void UpdatePivo()
    {
        pivo = pontos[currentPivo];
    }

    /// <summary>
    /// Rotaciona num ângulo pelo pivot
    /// </summary>
    /// <param name="angle"></param>
    public void LocalRotate(double angle)
    {
        var toTranslate = new Vector2 { x = -pivo.x, y = -pivo.y };
        var toTranslate2 = new Vector2 { x = pivo.x, y = pivo.y };

        Tranlate(toTranslate);

        Rotate(angle);

        Tranlate(toTranslate2);
    }

    /// <summary>
    /// Verifica se os dois valores são iguais
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public bool IsEqual(double a, double b)
    {
        return a == b;
    }

    /// <summary>
    /// Calcula a distância entre dois pontos
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public double Distance(Vector2 a, Vector2 b)
    {
        double distance = 0;

        var conta = ((b.x - a.x) * (b.x - a.x)) + ((b.y - a.y) * (b.y - a.y));

        distance = Math.Sqrt(conta);

        return distance;
    }

    public float[] DrawTriangle()
    {
        var points = new float[]{
                (float) pontos[0].x, (float) pontos[0].y, 0f,
                (float) pontos[1].x, (float) pontos[1].y, 0f,
                (float) pontos[2].x, (float) pontos[2].y, 0f,
            };

        return points;
    }
}

