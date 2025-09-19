using System.Collections.Generic;
using UnityEngine;

public struct ZombieInZone
{
    public Transform Transform;
    public ZPosition ZPosition;
    public XPosition XPosition;
    public Color Color;
    public Rigidbody rb;
    // public ZombieController zombie;

    public ZombieInZone(Transform transform, ZPosition zPosition, XPosition xPosition, Color color, Rigidbody rb)
    {
        Transform = transform;
        ZPosition = zPosition;
        XPosition = xPosition;
        Color = color;
        this.rb = rb;
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

    public ZombieInZone FindZombieToShoot(ZPosition zPosition, XPosition xPosition, Color brainColor)
    {
        ZombieInZone zombieInZone = _zombiesInZone.Find(z => z.ZPosition == zPosition && z.XPosition == xPosition);     // supposing it should be the first zombie

        if (zombieInZone.Transform != null && zombieInZone.Transform.position != Vector3.zero)
        {
            // check if zombie is of same color as of brain, if so remove zombie from here
            if (zombieInZone.Color == brainColor) _zombiesInZone.Remove(zombieInZone);

            // shoot brain
            // Debug.Log("Shooting brain " + zombieInZone.Transform.position);
            return zombieInZone;
        }

        // just null
        return new ZombieInZone(null, zPosition, xPosition, brainColor, null);
    }
}
