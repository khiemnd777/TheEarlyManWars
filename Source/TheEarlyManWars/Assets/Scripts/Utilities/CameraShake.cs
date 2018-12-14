using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        var originalLocalPosition = transform.localPosition;
        var elapsed = 0f;
        while (elapsed <= 1f)
        {
            transform.localPosition = originalLocalPosition + Random.insideUnitSphere * magnitude;
            elapsed += Time.deltaTime / duration;
            yield return null;
        }
        transform.localPosition = originalLocalPosition;
    }
}
