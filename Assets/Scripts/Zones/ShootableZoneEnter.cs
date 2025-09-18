using UnityEngine;

public class ShootableZoneEnter : MonoBehaviour
{
    [SerializeField] private ZPosition zPosition = ZPosition.Forward;
    [SerializeField] private XPosition xPosition = XPosition.Left;

    private ShootableZoneManager _shootableZoneManager;

    void Start()
    {
        _shootableZoneManager = ShootableZoneManager.Instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Zombie)) _shootableZoneManager.AddZombieInZone(new ZombieInZone(other.transform, zPosition, xPosition));
    }
}
