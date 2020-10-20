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
    private Vector2 _startTouchPositionUp;
    private Vector2 _endTouchPositionUp;
    private Vector2 _startTouchPositionDown;
    private Vector2 _endTouchPositionDown;

    private bool _estaDeslizando;


    public bool TouchParaCima()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            _startTouchPositionUp = Input.GetTouch(0).position;
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _endTouchPositionUp = Input.GetTouch(0).position;
            return _endTouchPositionUp.y > _startTouchPositionUp.y;
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            _startTouchPositionUp = Vector2.zero;
            _endTouchPositionUp = Vector2.zero;
        }

        return false;
    }

    public bool TouchParaBaixo()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            _startTouchPositionDown = Input.GetTouch(0).position;
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _endTouchPositionDown = Input.GetTouch(0).position;
            return _endTouchPositionDown.y < _startTouchPositionDown.y;
        }else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            _startTouchPositionDown = Vector2.zero;
            _endTouchPositionDown = Vector2.zero;
        }

        return false;
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
        _animator.SetBool(Desliza, _estaDeslizando);
    }

    /// <summary>
    /// Responsável por fazer o Player pular, O jogador só poderá pular se estiver no chão
    /// </summary>
    public void Pular()
    {
        if (!PodePular()) return;

        _rigidbody2D.AddForce(transform.up * forcaPulo);
        _audioSource.PlayOneShot(soundJump);
        _contadorTempo = 0;
        _boxCollider2D.offset = _offSetInicial;
        _boxCollider2D.size = _sizeInicial;
        _estaDeslizando = false;
    }

    /// <summary>
    /// Responsável por fazer o Player deslizar, O jogador só poderá deslizar se estiver no chão
    /// </summary>
    public void Deslizar()
    {
        if (_estaDeslizando)
        {
            _contadorTempo += Time.deltaTime;
            if (_contadorTempo >= tempoMaximoDeslizando)
            {
                _contadorTempo = 0;
                _boxCollider2D.offset = _offSetInicial;
                _boxCollider2D.size = _sizeInicial;
                _estaDeslizando = false;
            }
        }

        if (!PodeDeslizar()) return;

        _rigidbody2D.AddForce(transform.up * -forcaPulo);
        _boxCollider2D.offset = offSetDeslizando;
        _boxCollider2D.size = sizeDeslizando;
        _audioSource.PlayOneShot(soundSlide);
        _estaDeslizando = true;
    }

    /// <summary>
    /// Verifica se o Player pode deslizar
    /// </summary>
    /// <returns></returns>
    public bool PodeDeslizar()
    {
        return (Input.GetKey(botaoDesliza) || TouchParaBaixo()) && !_estaDeslizando;
    }

    /// <summary>
    /// Verifica se o Player tem condições para pular
    /// </summary>
    /// <returns></returns>
    public bool PodePular()
    {
        return EstaNoChao() && (Input.GetKey(botaoPulo) || TouchParaCima());
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