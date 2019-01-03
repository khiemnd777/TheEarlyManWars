using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : ScriptableObject
{
	public new string name;
	public SpeedEnum speed;
	public AttackSpeedEnum attackSpeed;
	public RangeAttackEnum rangeAttack;
	public int attackPower;
	public int hp;
	public MoveType moveType;
	public Animator animator;
	public AnimationClip animationAttack;
	[Space]
	public bool canKnockBack;
    public float knockBackProbability;
	public float knockBackRange;
}
