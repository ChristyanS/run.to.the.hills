using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public string botaoPulo;
    public string botaoDesliza;
    public float forcaPulo;
    public Transform verificaChaoTransform;
    public float raioDoVerificador;
    public LayerMask layerMaskChao;
    public AudioClip soundJump;
    public AudioClip soundSlide;
    public float tempoMaximoDeslizando;
    public Vector2 offSetDeslizando;
    public Vector2 sizeDeslizando;

    private static readonly int Pulo = Animator.StringToHash("pulo");
    private static readonly int Desliza = Animator.StringToHash("desliza");
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private AudioSource _audioSource;
    private BoxCollider2D _boxCollider2D;
    private float _contadorTempo;
    private Vector2 _offSetInicial;
    private Vector2 _sizeInicial;
    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;

    private void TouchCheck()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            _startTouchPosition = Input.GetTouch(0).position;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            _endTouchPosition = Input.GetTouch(0).position;
    }

    public bool TouchParaCima()
    {
        return _endTouchPosition.y > _startTouchPosition.y;
    }

    public bool TouchParaBaixo()
    {
        return _endTouchPosition.y < _startTouchPosition.y;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _sizeInicial = _boxCollider2D.size;
        _offSetInicial = _boxCollider2D.offset;
    }

    private void FixedUpdate()
    {
        TouchCheck();
        Pular();
        Deslizar();
        Animation();
    }

    /// <summary>
    /// Controla as animações do jogador
    /// </summary>
    private void Animation()
    {
        _animator.SetBool(Pulo, !EstaNoChao());
        _animator.SetBool(Desliza, EstaDeslizando());
    }

    /// <summary>
    /// Responsável por fazer o Player pular, O jogador só poderá pular se estiver no chão
    /// </summary>
    public void Pular()
    {
        if (!PodePular()) return;

        _rigidbody2D.AddForce(transform.up * forcaPulo);
        _audioSource.PlayOneShot(soundJump);
    }

    /// <summary>
    /// Responsável por fazer o Player deslizar, O jogador só poderá deslizar se estiver no chão
    /// </summary>
    public void Deslizar()
    {
        if (EstaDeslizando())
        {
            _contadorTempo += Time.deltaTime;
            if (_contadorTempo >= tempoMaximoDeslizando)
            {
                _contadorTempo = 0;
                _boxCollider2D.offset = _offSetInicial;
                _boxCollider2D.size = _sizeInicial;
            }
        }

        if (!PodeDeslizar()) return;

        _boxCollider2D.offset = offSetDeslizando;
        _boxCollider2D.size = sizeDeslizando;
        _audioSource.PlayOneShot(soundSlide);
        _contadorTempo += Time.deltaTime;
    }

    /// <summary>
    /// verifica se o player está deslizando
    /// </summary>
    /// <returns>se está deslizando retorna true e false caso o contrario</returns>
    public bool EstaDeslizando()
    {
        return _contadorTempo > 0;
    }

    /// <summary>
    /// Verifica se o Player pode deslizar
    /// </summary>
    /// <returns></returns>
    public bool PodeDeslizar()
    {
        return (Input.GetKeyDown(botaoDesliza) || TouchParaBaixo()) && EstaNoChao() && !EstaDeslizando();
    }

    /// <summary>
    /// Verifica se o Player tem condições para pular
    /// </summary>
    /// <returns></returns>
    public bool PodePular()
    {
        return (Input.GetKeyDown(botaoPulo) || TouchParaCima()) && EstaNoChao() && !EstaDeslizando();
    }

    /// <summary>
    /// Verifica se o jogador está tocando no chão
    /// </summary>
    /// <returns>true se está no chão e false caso contrário</returns>
    public bool EstaNoChao()
    {
        return Physics2D.OverlapCircle(verificaChaoTransform.position, raioDoVerificador, layerMaskChao);
    }

    /// <summary>
    /// Desenha Gizmos Para a Unity
    /// </summary>
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(verificaChaoTransform.position, raioDoVerificador);
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.collider.CompareTag(TagConstantes.INIMIGO))
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}