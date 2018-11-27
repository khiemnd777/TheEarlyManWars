using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Button))]
public class AddCharacterButton : MonoBehaviour
{
    public BaseCharacter baseCharacter;
    CharacterSpawner _spawner;
    Button _button;

    void Awake ()
    {
        _button = GetComponent<Button> ();
        _spawner = FindObjectOfType<CharacterSpawner>();
    }

    void Start ()
    {
        _button.onClick.AddListener (() => OnClick ());
    }

    void OnClick ()
    {
        _spawner.Spawn(baseCharacter);
    }
}
