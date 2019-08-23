using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public Transform spawningPoint;
    CharacterDisplayList _displayList;
    [SerializeField]
    CharacterAvailableList _availableList;

    void Awake ()
    {
        _displayList = FindObjectOfType<CharacterDisplayList> ();
    }

    public void Spawn (BaseCharacter baseCharacter)
    {
        var ins = Instance (baseCharacter);
        if (ins == null) return;
        _displayList.Add (ins);
    }

    CharacterDisplay Instance (BaseCharacter baseCharacter)
    {
        if (!_availableList.Exists (baseCharacter)) return null;
        var posZ = baseCharacter.lane.GetPosition();
        var realPosZ = posZ + Random.Range (-.2f, .2f);
        var spawningPosition = spawningPoint.position + Vector3.forward * realPosZ;
        var instance = Instantiate<CharacterDisplay> (baseCharacter.displayPrefab, spawningPosition, Quaternion.identity);
        instance.baseObject = baseCharacter;
        return instance;
    }
}
