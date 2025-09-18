using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerFaceDirection { Forward = 0, Backward = 180 }

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;

    private float _curYRotation = 0;        // 0 ⇌ 180
    private PlayerFaceDirection _curFaceDirection;

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
