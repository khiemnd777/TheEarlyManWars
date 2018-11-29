using UnityEngine;

public class MeatSystem : MonoBehaviour
{
    public int meat;
    public float deltaCounter = 1f;
    Settings _settings;
    float _meat;

    void Start ()
    {
        _settings = FindObjectOfType<Settings> ();
    }

    public void Purchase (int meat, System.Action then)
    {
        if (meat < this.meat) return;
        this.meat -= meat;
        if (then != null)
        {
            then ();
        }
    }

    void Update ()
    {
        _meat += Time.deltaTime * deltaCounter * _settings.deltaSpeed;
        meat = Mathf.FloorToInt (_meat);
    }
}
