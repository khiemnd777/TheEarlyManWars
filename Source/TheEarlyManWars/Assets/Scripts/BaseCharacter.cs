using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Character", menuName = "Create Character")]
public class BaseCharacter : BaseObject {
	public int meat;
	public AttackType attackType;
	public int cooldownPurchase;
}