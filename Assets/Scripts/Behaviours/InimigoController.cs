using UnityEngine;

public class InimigoController : MonoBehaviour
{
    public float velocidade;

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(velocidade, _rigidbody2D.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag(TagConstantes.DESTRUIDOR))
        {
            Destroy(transform.gameObject);
        }
        
        if (collider2D.CompareTag(TagConstantes.PONTUADOR))
        {
            Pontuacao.Instance.AdicionaPontos();
        }
    }
}