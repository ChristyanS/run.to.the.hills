using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text recordText;
    public Text pontuacaoText;

    private void Start()
    {
        ConfiguraNovoRecord();
        ConfiguraTexto();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("Fase");
            Pontuacao.Instance.ResetaPontos();
        }
    }

    /// <summary>
    /// Verifica se o record foi batido
    /// </summary>
    /// <returns>retorna true se o record foi batido e false caso contrario</returns>
    private bool BateuRecord()
    {
        return Pontuacao.Instance.pontos > PlayerPrefs.GetInt(ValoresPersistidosEnum.RECORD.ToString());
    }

    /// <summary>
    /// Responsável por setar o record caso tenha ocorrido
    /// </summary>
    private void ConfiguraNovoRecord()
    {
        if (BateuRecord())
        {
            PlayerPrefs.SetInt(ValoresPersistidosEnum.RECORD.ToString(), Pontuacao.Instance.pontos);
        }
    }

    /// <summary>
    /// Configura os texto de pontuação
    /// </summary>
    private void ConfiguraTexto()
    {
        recordText.text = PlayerPrefs.GetInt(ValoresPersistidosEnum.RECORD.ToString()).ToString();
        pontuacaoText.text = Pontuacao.Instance.pontos.ToString();
    }
}