using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu (fileName = "Character Available List", menuName = "Create Character Available List")]
public class CharacterAvailableList : ScriptableObject
{
    public List<BaseCharacter> list;

    public void Add (BaseCharacter baseCharacter)
    {
        list.Add (baseCharacter);
    }

    public bool Exists (BaseCharacter baseCharacter)
    {
        return list.Any(x => baseCharacter.Equals(x));
    }
}
