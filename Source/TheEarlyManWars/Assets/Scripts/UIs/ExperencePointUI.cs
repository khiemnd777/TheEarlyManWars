using UnityEngine;
using UnityEngine.UI;

public class ExperencePointUI : MonoBehaviour
{
    Text _text;
    [SerializeField]
    Currency _currency;

    void Awake()
    {
        _text = GetComponent<Text>();
    }

    void Update ()
    {
        _text.text = Mathf.Ceil(_currency.experencePoint).ToString();
    }
}
