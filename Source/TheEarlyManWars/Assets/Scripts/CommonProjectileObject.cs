using System.Collections;
using UnityEngine;

public class CommonProjectileObject : ProjectileObject
{
    public float height;
    Vector3 _currentVel;
    Rigidbody2D _rb;

    void Awake ()
    {
        _rb = GetComponent<Rigidbody2D> ();
    }

    public override void Launch (Vector3 targetPosition, Vector3 deltaDistance, Vector3 stopPosition, float deltaSpeed = 1, System.Action reachedTargetAction = null)
    {
        StartCoroutine (Lauching (targetPosition, deltaDistance, stopPosition, deltaSpeed, reachedTargetAction));
    }

    IEnumerator Lauching (Vector3 targetPosition, Vector3 deltaDistance, Vector3 stopPosition, float deltaSpeed = 1, System.Action reachedTargetAction = null)
    {
        _rb.gravityScale *= deltaSpeed * initialVelocity;
        var gravity = JumpVelocityCalculator.GetGravity2D (_rb);
        var jumpVel = JumpVelocityCalculator.Calculate (transform.position, targetPosition, deltaDistance, stopPosition, gravity, height, true);
        _currentVel = jumpVel.velocity;
        _rb.velocity = _currentVel;
        yield return new WaitForSeconds (jumpVel.simulatedTime);
        if (reachedTargetAction != null)
        {
            reachedTargetAction ();
        }
        Destroy (gameObject);
    }
}
