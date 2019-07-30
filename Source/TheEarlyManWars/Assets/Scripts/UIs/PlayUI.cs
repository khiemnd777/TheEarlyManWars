using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayUI : MonoBehaviour
{
    [SerializeField]
    Text _youWinText;

    Settings _settings;
    bool _stopped;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
    }

    public void Speedx2 ()
    {
        if (_stopped) return;
        Time.timeScale = 2;
        // _settings.deltaSpeed = 2;
    }

    public void Speedx3 ()
    {
        if (_stopped) return;
        Time.timeScale = 3;
        // _settings.deltaSpeed = 3;
    }

    public void Play ()
    {
        if (_stopped) return;
        Time.timeScale = 1;
        // _settings.deltaSpeed = 1;
    }

    public void Pause ()
    {
        Time.timeScale = 0;
        // _settings.deltaSpeed = 0;
    }

    public void Stop ()
    {
        Pause ();
        _stopped = true;
    }

    public void ShowYouWinText ()
    {
        _youWinText.gameObject.SetActive (true);
    }

    public void Win ()
    {
        Stop ();
        ShowYouWinText ();
    }

    public void ResetScene ()
    {
        SceneManager.LoadScene("Gameplay/Prototype 1");
    }
}
