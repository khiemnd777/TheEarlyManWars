﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCharacterDisplay : CharacterDisplay
{
    [SerializeField]
    ProjectileObject _projectileObjectPrefab;

    protected override IEnumerator AnimateAttack ()
    {
        yield break;
    }
}
