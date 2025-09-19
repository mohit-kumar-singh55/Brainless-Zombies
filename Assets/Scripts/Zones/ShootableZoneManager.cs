using System.Collections.Generic;
using UnityEngine;

public struct ZombieInZone
{
    public Transform Transform;
    public ZPosition ZPosition;
    public XPosition XPosition;
    public Color Color;
    public Rigidbody rb;

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
        // remove any destroyed zombies
        _zombiesInZone.RemoveAll(zom => zom.Transform == null);

        ZombieInZone zombieInZone = _zombiesInZone.Find(zom => zom.ZPosition == zPosition && zom.XPosition == xPosition);

        if (zombieInZone.Transform == null)
            return new ZombieInZone(null, zPosition, xPosition, brainColor, null);

        // check match
        if (zombieInZone.Color == brainColor)
            _zombiesInZone.Remove(zombieInZone);

        return zombieInZone;
    }

}
