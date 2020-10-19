public class Fase
{
    private static Fase instance;

    private Fase()
    {
    }

    public float AjustaVelocidade(float value)
    {
        return value + PorcentagemDeAceleracao(value);
    }

    public float PorcentagemDeAceleracao(float value)
    {
        if (Pontuacao.Instance.pontos * 2 / 2 >= 300f)
        {
            return value * 300f / 100;
        }

        return value * (Pontuacao.Instance.pontos * 2 / 2) / 100f;
    }

    public float AjustaVelocidadeDiminui(float value)
    {
        return value - PorcentagemDeDesaceleracao(value);
    }

    public float PorcentagemDeDesaceleracao(float value)
    {
        if (Pontuacao.Instance.pontos * 2 / 4 >= 60)
        {
            return value * 60f / 100;
        }

        return value * (Pontuacao.Instance.pontos * 2 / 4) / 100f;
    }

    /// <summary>
    /// Singleton para Fase
    /// </summary>
    public static Fase Instance
    {
        get
        {
            if (instance == null)
                instance = new Fase();
            return instance;
        }
    }
}