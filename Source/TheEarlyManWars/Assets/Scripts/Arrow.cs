using System.Collections;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class Arrow : MonoBehaviour
{
    public int damage = 5;

    Rigidbody2D _rb;

    void Awake ()
    {
        _rb = GetComponent<Rigidbody2D> ();
    }

    public void Launch (Vector3 targetPosition, System.Action reachedTargetAction = null)
    {
        StartCoroutine(Lauching(targetPosition, reachedTargetAction));
    }

    IEnumerator Lauching (Vector3 targetPosition, System.Action reachedTargetAction = null)
    {
        var gravity = JumpVelocityCalculator.GetGravity2D (_rb);
        var jumpVel = JumpVelocityCalculator.Calculate (transform.position, targetPosition, gravity, 3, true);
        _rb.velocity = jumpVel.velocity;
        yield return new WaitForSeconds(jumpVel.simulatedTime);
        if(reachedTargetAction != null){
            reachedTargetAction();
        }
        Destroy(gameObject);
    }
}
