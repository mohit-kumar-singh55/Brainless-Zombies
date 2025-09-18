using System.Collections.Generic;
using UnityEngine;

public struct ZombieInZone
{
    public Transform Transform;
    public ZPosition ZPosition;
    public XPosition XPosition;
    // public ZombieController zombie;

    public ZombieInZone(Transform transform, ZPosition zPosition, XPosition xPosition)
    {
        Transform = transform;
        ZPosition = zPosition;
        XPosition = xPosition;
    }
}

public class ShootableZoneManager : MonoBehaviour
{
    public static ShootableZoneManager Instance { get; private set; }

    private List<ZombieInZone> _zombiesInZone = new();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddZombieInZone(ZombieInZone zombieInZone) => _zombiesInZone.Add(zombieInZone);

    public void FindZombieToShoot(ZPosition zPosition, XPosition xPosition)
    {
        ZombieInZone zombieInZone = _zombiesInZone.Find(z => z.ZPosition == zPosition && z.XPosition == xPosition);     // supposing it should be the first zombie

        if (zombieInZone.Transform != null && zombieInZone.Transform.position != Vector3.zero)
        {
            // shoot brain
            Debug.Log("Shooting brain " + zombieInZone.Transform.position);
            _zombiesInZone.Remove(zombieInZone);
            Destroy(zombieInZone.Transform.gameObject);
            // check if zombie is of same color as of brain, if so remove zombie from here
            // _zombiesInZone.Remove(zombieInZone);
        }
    }
}
