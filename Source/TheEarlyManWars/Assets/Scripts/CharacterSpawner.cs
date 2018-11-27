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

    public CharacterDisplay Instance (BaseCharacter baseCharacter)
    {
        if (!_availableList.Exists (baseCharacter)) return null;
        var z = Random.Range (-1f, 1f);
        var spawningPosition = spawningPoint.position + Vector3.forward * z;
        var instance = Instantiate<CharacterDisplay> (_displayPrefab, spawningPosition, Quaternion.identity);
        instance.baseObject = baseCharacter;
        return instance;
    }

    public void Spawn (BaseCharacter baseCharacter)
    {
        var ins = Instance(baseCharacter);
        if(ins == null) return;
        _displayList.Add(ins);
    }
}
