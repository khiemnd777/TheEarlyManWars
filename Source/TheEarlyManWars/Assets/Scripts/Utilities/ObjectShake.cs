using System.Collections;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    public float duration;
    public float magnitude;

    Settings _settings;

    void Start ()
    {
        _settings = FindObjectOfType<Settings> ();
    }

    public IEnumerator Shake (Transform owner)
    {
        var originalLocalPosition = owner.localPosition;
        var elapsed = 0f;
        while (elapsed <= 1f)
        {
            owner.localPosition = originalLocalPosition + Random.insideUnitSphere * magnitude;
            elapsed += Time.fixedDeltaTime / duration * _settings.deltaSpeed;
            yield return new WaitForFixedUpdate ();
        }
        owner.localPosition = originalLocalPosition;
    }
}
