using UnityEngine;

public abstract class ProjectileObject : MonoBehaviour
{
    public float initialVelocity;
    [System.NonSerialized]
    public Direction direction;

    public virtual void Launch (Vector3 targetPosition, Vector3 deltaDistance, Vector3 stopPosition, float deltaSpeed = 1, System.Action reachedTargetAction = null)
    {

    }
}
