using UnityEngine;

public abstract class ProjectileObject : MonoBehaviour
{
    public float initialVelocity;
    [System.NonSerialized]
    public Direction direction;
}
