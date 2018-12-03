using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCharacterDisplay : CharacterDisplay
{
    [SerializeField]
    Transform _flyableObject;

    protected override IEnumerator AnimateAttack ()
    {
        yield break;
    }
}
