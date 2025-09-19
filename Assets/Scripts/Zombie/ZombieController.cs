using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(Collider))]
public class ZombieController : MonoBehaviour
{
    [SerializeField] private Color zombieColor = Color.Yellow;     // yellow by default

    private Vector3 _moveDirection;
    private float _moveSpeed = 0.1f;        // 0.1 by default if not set
    private Rigidbody _rb;
    private Animator animator;
    private Collider col;

    public Color ZombieColor => zombieColor;

    // public delegate void ZombieHit(ZombieController zombie);
    // public event ZombieHit OnZombieHit;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider>();
    }

    void Start()
    {
        if (_moveDirection == null)
        {
            Debug.Log("No Move Direction is provided");
            gameObject.SetActive(false);
            return;
        }
    }

    void Update()
    {
        if (_rb.isKinematic) MoveZombie();
    }

    private void MoveZombie()
    {
        transform.position += _moveSpeed * Time.deltaTime * new Vector3(0f, 0f, _moveDirection.z);
    }

    public void SetMoveDirection(Vector3 moveDir) => _moveDirection = moveDir;

    public void SetMoveSpeed(float speed) => _moveSpeed = speed;

    public void PushBackItSelf(Vector3 brainPos, Vector3 forcePos)
    {
        animator.enabled = false;
        col.isTrigger = false;
        _rb.isKinematic = false;
        _rb.useGravity = true;
        _rb.AddForceAtPosition((brainPos - transform.position).normalized * 50, forcePos, ForceMode.Impulse);
        _rb.AddTorque(Vector3.up * 50, ForceMode.Impulse);

        Destroy(gameObject, 1.5f);
    }
}
