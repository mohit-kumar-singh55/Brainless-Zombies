using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;

    private float _curYRotation = 0;        // 0 ⇌ 180
    private PlayerFaceDirection _curFaceDirection;

    public delegate void BrainShoot(ZPosition zPosition, XPosition xPosition);
    public static event BrainShoot OnBrainShoot;

    public PlayerFaceDirection CurFaceDirection => _curFaceDirection;

    void Awake()
    {
        _curFaceDirection = PlayerFaceDirection.Forward;       // start facing forward
    }

    // being called internally by the input system
    private void OnSwitchCamRotation(InputValue val)
    {
        _curYRotation = _curYRotation == 0 ? 180 : 0;
        _curFaceDirection = _curYRotation == 0 ? PlayerFaceDirection.Forward : PlayerFaceDirection.Backward;

        StartCoroutine(RotatePlayer());
    }

    // being called internally by the input system
    private void OnShootBrain(InputValue val)
    {
        Vector2 value = val.Get<Vector2>();

        if (value == Vector2.zero) return;

        // find and shoot brain at zombie
        OnBrainShoot?.Invoke(_curFaceDirection == PlayerFaceDirection.Forward ? ZPosition.Forward : ZPosition.Backward, value.x < 0 ? XPosition.Left : XPosition.Right);
    }

    IEnumerator RotatePlayer()
    {
        float t = 0;
        float startRatation = transform.rotation.eulerAngles.y;

        while (t < 1)
        {
            t += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Euler(0, Mathf.Lerp(startRatation, _curYRotation, t), 0);
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, _curYRotation, 0);
    }
}
