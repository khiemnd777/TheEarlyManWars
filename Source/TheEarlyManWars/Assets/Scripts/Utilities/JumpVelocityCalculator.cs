using UnityEngine;

public class JumpVelocityCalculator
{
    public static JumpVelocityData Calculate (Vector3 ownerPosition, Vector3 targetPosition, Vector3 deltaDistance, Vector3 stopPosition, float gravity, float height, bool updateHeight)
    {
        var offsetY = targetPosition.y - ownerPosition.y;
        var updatedHeight = offsetY - height > 0 ? offsetY + height : height;
        var time = (Mathf.Sqrt ((float) (-2 * updatedHeight / gravity)) + Mathf.Sqrt ((float) (2 * (offsetY - updatedHeight) / gravity)));
        var velocityY = Vector3.up * Mathf.Sqrt ((float) (-2 * gravity * updatedHeight));
        var targetDeltaDistance = time * deltaDistance;
        var realTargetPosition = targetPosition - targetDeltaDistance;
        if (realTargetPosition.x < stopPosition.x)
        {
            realTargetPosition = new Vector3 (stopPosition.x, realTargetPosition.y, realTargetPosition.z);
        }
        var offsetXZ = new Vector3 (realTargetPosition.x - ownerPosition.x, 0, realTargetPosition.z - ownerPosition.z);
        var velocityXZ = offsetXZ / time;
        var velocity = velocityXZ + velocityY * -Mathf.Sign ((float) gravity);
        return new JumpVelocityData (velocity, time);
    }

    public static void DrawPath (Vector3 ownerPosition, Vector3 targetPosition, Vector3 deltaDistance, Vector3 stopPosition, float gravity, float height, bool updateHeight)
    {
        var calculatedJump = Calculate (ownerPosition, targetPosition, deltaDistance, stopPosition, gravity, height, updateHeight);
        var previousDrawPoint = ownerPosition;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            var simulationTime = i / (float) resolution * calculatedJump.simulatedTime;
            var displacement = calculatedJump.velocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            var drawPoint = ownerPosition + displacement;
            Debug.DrawLine (previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    public static float GetGravity2D (Rigidbody2D rb)
    {
        return GetGravity2D (rb.gravityScale);
    }

    public static float GetGravity2D (float gravityScale)
    {
        return -gravityScale * Physics2D.gravity.magnitude;
    }

    public struct JumpVelocityData
    {
        public readonly Vector3 velocity;
        public readonly float simulatedTime;

        public JumpVelocityData (Vector3 velocity, float simulatedTime)
        {
            this.velocity = velocity;
            this.simulatedTime = simulatedTime;
        }
    }
}
