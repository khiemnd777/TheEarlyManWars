using UnityEngine;
using UnityEngine.UI;

public class DiamondUI : MonoBehaviour
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
        _text.text = Mathf.Ceil(_currency.diamond).ToString();
    }
}
