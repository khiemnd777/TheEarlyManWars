using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Monster", menuName = "Create Monster")]
public class BaseMonster : BaseObject
{
    public int gainedMeat;
    public MonsterAttackType attackType;
    public MonsterDisplay displayPrefab;
}
