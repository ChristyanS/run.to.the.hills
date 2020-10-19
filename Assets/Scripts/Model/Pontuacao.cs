public class Pontuacao
{
    public int pontos { get; private set; }
    private static Pontuacao instance;

    private Pontuacao()
    {
    }

    /// <summary>
    /// Adiciona pontos para o jogador 
    /// </summary>
    public void AdicionaPontos()
    {
        pontos++;
    }

    public void ResetaPontos()
    {
        pontos = 0;
    }

    /// <summary>
    /// Singleton para Pontuacao
    /// </summary>
    public static Pontuacao Instance
    {
        get
        {
            if (instance == null)
                instance = new Pontuacao();
            return instance;
        }
    }
}