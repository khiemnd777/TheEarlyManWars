using System.Collections;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    public float duration;
    public float magnitude;
    Vector3 _originalLocalPosition;
    Settings _settings;

    void Start ()
    {
        _settings = FindObjectOfType<Settings> ();
        _originalLocalPosition = transform.localPosition;
        duration = _settings.objectShakeDuration;
        magnitude = _settings.objectShakeMagnitude;
    }

    public IEnumerator Shake ()
    {
        var elapsed = 0f;
        while (elapsed <= 1f)
        {
            transform.localPosition = _originalLocalPosition + Random.insideUnitSphere * magnitude;
            elapsed += Time.fixedDeltaTime / duration;
            yield return new WaitForFixedUpdate ();
        }
        transform.localPosition = _originalLocalPosition;
    }
}
