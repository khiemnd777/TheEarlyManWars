using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Character Available List", menuName = "Create Character Available List")]
public class CharacterAvailableList : ScriptableObject
{
    public List<BaseCharacter> list;
    [SerializeField]
    CharacterDisplay _displayPrefab;

    public void Add (BaseCharacter baseCharacter)
    {
        list.Add (baseCharacter);
    }

    public CharacterDisplay Instance (BaseCharacter baseCharacter, Vector3 spawningPoint)
    {
        var z = Random.Range (-1f, 1f);
        var spawningPosition = spawningPoint + Vector3.forward * z;
        var instance = Instantiate<CharacterDisplay> (_displayPrefab, spawningPosition, Quaternion.identity);
        instance.baseObject = baseCharacter;
        return instance;
    }
}
