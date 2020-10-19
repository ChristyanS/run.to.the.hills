using System;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float velocidade;

    private Vector3 _posicaoInicial;

    public float _posicaoXFinal;

    // Start is called before the first frame update
    void Start()
    {
        velocidade=Math.Abs(velocidade);
        _posicaoInicial = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(-Fase.Instance.AjustaVelocidade(velocidade)* Time.deltaTime *
                            new Vector3(1, 0, 0));
        if (transform.position.x < _posicaoXFinal)
        {
            transform.position = _posicaoInicial;
        }
    }
}