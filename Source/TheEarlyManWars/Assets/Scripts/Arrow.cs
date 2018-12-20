using System.Collections;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class Arrow : MonoBehaviour
{
    public int damage = 5;
    public float height = 3f;
    Settings _settings;
    Rigidbody2D _rb;
    Vector3 _currentVel;

    void Awake ()
    {
        _rb = GetComponent<Rigidbody2D> ();
        _settings = FindObjectOfType<Settings> ();
    }

    public void Launch (Vector3 targetPosition, float deltaSpeed = 1, System.Action reachedTargetAction = null)
    {
        StartCoroutine (Lauching (targetPosition, deltaSpeed, reachedTargetAction));
    }

    IEnumerator Lauching (Vector3 targetPosition, float deltaSpeed = 1, System.Action reachedTargetAction = null)
    {
        _rb.gravityScale *= deltaSpeed;
        var gravity = JumpVelocityCalculator.GetGravity2D (_rb);
        var jumpVel = JumpVelocityCalculator.Calculate (transform.position, targetPosition, gravity, height, true);
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
