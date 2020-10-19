using UnityEngine;
using UnityEngine.UI;

public class PontuacaoView : MonoBehaviour
{
    private Text pontuacaoText;

    private void Start()
    {
        pontuacaoText = GetComponent<Text>();
    }

    private void Update()
    {
        pontuacaoText.text = Pontuacao.Instance.pontos.ToString();
    }
}