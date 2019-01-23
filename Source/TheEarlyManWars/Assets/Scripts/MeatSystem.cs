using UnityEngine;

public class MeatSystem : MonoBehaviour
{
    public int meat { get { return Mathf.FloorToInt (_meat); } }
    public float deltaCounter = 1f;
    Settings _settings;
    [SerializeField]
    float _meat;
    TechnologyManager _technologyManager;

    void Start ()
    {
        _settings = FindObjectOfType<Settings> ();
        _technologyManager = FindObjectOfType<TechnologyManager>();
    }

    public void Gain (float meat)
    {
        this._meat += meat;
    }

    public void Gain (float[] meats)
    {
        foreach (var meat in meats)
        {
            Gain (meat);
        }
    }

    public void Purchase (float meat, System.Action then)
    {
        if (meat > this._meat) return;
        this._meat -= meat;
        if (then != null) then ();
    }

    void Update ()
    {
        var amount = Time.deltaTime * deltaCounter * _settings.deltaSpeed;
        amount *= (1 + _technologyManager.meatRate);
        _meat += amount;
    }
}
