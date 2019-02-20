using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
	[Header ("Upgrade")]
	public float upgradedAttackRate;
	public float upgradedHpRate;
	public Cost upgradedCost;
	public UpgradedAnimationLevel[] upgradedAnimationLevels;

	public void AssignUpgradedDisplay ()
	{
		if (upgradedAnimationLevels.All (x => x.level != level)) return;
		var mUpgradedAnimationLevel = upgradedAnimationLevels.FirstOrDefault (x => x.level == level);
		displayPrefab = (CharacterDisplay) mUpgradedAnimationLevel.characterDisplay;
	}
}
