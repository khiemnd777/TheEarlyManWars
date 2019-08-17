using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage = 5;
    public float height = 3f;
    Rigidbody2D _rb;
    Vector3 _currentVel;

    void Awake ()
    {
        _rb = GetComponent<Rigidbody2D> ();
    }

    public void Launch (Vector3 targetPosition, Vector3 deltaDistance, Vector3 stopPosition, System.Action reachedTargetAction = null)
    {
        StartCoroutine (Lauching (targetPosition, deltaDistance, stopPosition, reachedTargetAction));
    }

    IEnumerator Lauching (Vector3 targetPosition, Vector3 deltaDistance, Vector3 stopPosition, System.Action reachedTargetAction = null)
    {
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
