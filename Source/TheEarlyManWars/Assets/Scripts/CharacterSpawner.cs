using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public Transform spawningPoint;
    CharacterDisplayList _displayList;
    [SerializeField]
    CharacterAvailableList _availableList;
    [SerializeField]
    CharacterDisplay _displayPrefab;
    Settings _settings;

    void Awake ()
    {
        _settings = FindObjectOfType<Settings> ();
        _displayList = FindObjectOfType<CharacterDisplayList> ();
    }

    public void Spawn (BaseCharacter baseCharacter)
    {
        
    }
}
