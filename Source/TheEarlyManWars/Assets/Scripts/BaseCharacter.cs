using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Character", menuName = "Create Character")]
public class BaseCharacter : BaseObject
{
	public CharacterLevel characterLevel;
	public int level;
	public int meat;
	public AttackType attackType;
	public float cooldownPurchase;
	public CharacterDisplay displayPrefab;
	[Header("Upgrade")]
    public float upgradedAttackRate;
    public float upgradedHpRate;
    public Cost upgradedCost;
}
