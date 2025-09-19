using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BrainController : MonoBehaviour
{
    [SerializeField] private Color brainColor = Color.Yellow;     // yellow by default

    private Vector3 _moveDirection;
    private float _moveSpeed = 0.1f;        // 0.1 by default if not set
    private Rigidbody _rb;

    public Color BrainColor => brainColor;

    public delegate void BrainHit(BrainController brain);
    public event BrainHit OnBrainHit;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
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
        if (_rb.isKinematic) MoveBrain();
    }

    private void MoveBrain()
    {
        transform.position += _moveSpeed * Time.deltaTime * new Vector3(0f, 0f, _moveDirection.z);
    }

    public void SetMoveDirection(Vector3 moveDir) => _moveDirection = moveDir;

    public void SetMoveSpeed(float speed) => _moveSpeed = speed;

    public void ShootItSelf(Vector3 zombiePos)
    {
        if (_rb == null) return;
        _rb.isKinematic = false;
        _rb.AddForce((zombiePos - transform.position).normalized * 50, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player)) OnBrainHit?.Invoke(this);
        else if (other.CompareTag(Tags.Zombie))
        {
            if (other.TryGetComponent(out ZombieController zombieController))
            {
                if (zombieController.ZombieColor == brainColor)
                {
                    zombieController.PushBackItSelf(transform.position, other.ClosestPoint(transform.position));
                    CameraController.Instance.ShakeCamera();
                }
            }
        }

        // destroy brain if hit anything
        if (!other.CompareTag(Tags.GameOverLine)) Destroy(gameObject);
    }
}
