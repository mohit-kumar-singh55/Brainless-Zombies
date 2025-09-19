using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    private CinemachineImpulseSource cineImpulseSouce;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        cineImpulseSouce = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeCamera() => cineImpulseSouce.GenerateImpulse();
}
