using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Button))]
public class AddCharacterButton : MonoBehaviour
{
    public BaseCharacter baseCharacter;
    int _meat;
    MeatSystem _meatSystem;
    CharacterSpawner _spawner;
    Button _button;
    [SerializeField]
    Text _nameText;
    [SerializeField]
    Text _meatText;

    void Awake ()
    {
        _button = GetComponent<Button> ();
        _spawner = FindObjectOfType<CharacterSpawner> ();
        _meatSystem = FindObjectOfType<MeatSystem> ();
    }

    void Start ()
    {
        _meat = baseCharacter.meat;
        _nameText.text = baseCharacter.name;
        _meatText.text = _meat.ToString();
        _button.onClick.AddListener (() => OnClick ());
    }

    void OnClick ()
    {
        _meatSystem.Purchase (_meat, () => _spawner.Spawn (baseCharacter));
    }
}
