using UnityEngine;

public class MeatSystem : MonoBehaviour
{
    public int meat { get { return Mathf.FloorToInt (_meat); } }
    public float deltaCounter = 1f;
    Settings _settings;
    [SerializeField]
    float _meat;

    void Start ()
    {
        _settings = FindObjectOfType<Settings> ();
    }

    public void Purchase (int meat, System.Action then)
    {
        if (meat > this._meat) return;
        this._meat -= meat;
        if (then != null) then ();
    }

    void Update ()
    {
        _meat += Time.deltaTime * deltaCounter * 15 * _settings.deltaSpeed;
    }
}
