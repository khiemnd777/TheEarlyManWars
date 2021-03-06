using UnityEngine;

public class TowerObject : ScriptableObject
{
    public int level;
    public Animator animator;
    public float damage;
    public float hp;
    public float rangeAttack;
    public float attackSpeed;
    public TowerDisplay displayPrefab;
}
