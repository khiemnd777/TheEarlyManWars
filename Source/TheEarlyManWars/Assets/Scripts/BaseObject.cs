using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : ScriptableObject
{
	public new string name;
	public Animator animator;
	public SpeedEnum speed;
	public AttackSpeedEnum attackSpeed;
	public RangeAttackEnum rangeAttack;
	public float attackPower;
	public float hp;
	public MoveType moveType;
	public AnimationClip animationAttack;
	[Space]
	public bool canKnockBack;
    public float knockBackProbability;
	public float knockBackRange;
}
