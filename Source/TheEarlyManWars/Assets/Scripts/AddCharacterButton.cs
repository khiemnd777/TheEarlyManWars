using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Button))]
public class AddCharacterButton : MonoBehaviour
{
    public BaseCharacter baseCharacter;
    public int meat;
    MeatSystem _meatSystem;
    CharacterSpawner _spawner;
    Button _button;

    void Awake ()
    {
        _button = GetComponent<Button> ();
        _spawner = FindObjectOfType<CharacterSpawner> ();
        _meatSystem = FindObjectOfType<MeatSystem> ();
    }

    void Start ()
    {
        meat = baseCharacter.meat;
        _button.onClick.AddListener (() => OnClick ());
    }

    void OnClick ()
    {
        _meatSystem.Purchase (meat, () => _spawner.Spawn (baseCharacter));
    }
}
