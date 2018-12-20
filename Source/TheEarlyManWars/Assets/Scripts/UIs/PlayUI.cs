using UnityEngine;

public class PlayUI : MonoBehaviour
{
    Settings _settings;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
    }

    public void Speedx2 ()
    {
        _settings.deltaSpeed = 2;
    }

    public void Speedx3 ()
    {
        _settings.deltaSpeed = 3;
    }

    public void Play ()
    {
        _settings.deltaSpeed = 1;
    }

    public void Pause ()
    {
        _settings.deltaSpeed = 0;
    }
}
