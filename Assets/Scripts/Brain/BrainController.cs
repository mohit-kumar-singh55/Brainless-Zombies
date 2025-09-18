using UnityEngine;

public class BrainController : MonoBehaviour
{
    [SerializeField] Color brainColor = Color.Yellow;     // yellow by default

    private Vector3 _moveDirection;
    private float _moveSpeed = 0.1f;        // 0.1 by default if not set

    public Color BrainColor => brainColor;

    public delegate void BrainHit(BrainController brain);
    public event BrainHit OnBrainHit;

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
        MoveBrain();
    }

    private void MoveBrain()
    {
        transform.position += _moveSpeed * Time.deltaTime * new Vector3(0f, 0f, _moveDirection.z);
    }

    public void SetMoveDirection(Vector3 moveDir) => _moveDirection = moveDir;

    public void SetMoveSpeed(float speed) => _moveSpeed = speed;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            OnBrainHit?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
