using UnityEngine;

public class CommonProjectileObject : ProjectileObject
{
    Settings _settings;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
    }

    void FixedUpdate ()
    {
        transform.position += Vector3.right * (int) direction * initialVelocity * _settings.deltaSpeed * _settings.deltaMoveStep * _settings.deltaProjectileMoveStep * Time.fixedDeltaTime;
    }
}
