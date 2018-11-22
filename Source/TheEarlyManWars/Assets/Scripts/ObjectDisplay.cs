using UnityEngine;

public abstract class ObjectDisplay : MonoBehaviour
{
    public BaseObject baseObject;
    public ObjectDisplayList allies;
    public ObjectDisplayList enemies;
    public Stat speed;
    public Stat attackSpeed;
    public Stat attackPower;
    public Stat rangeAttack;
    public Stat hp;
    public int maxHP;

    public virtual void Awake ()
    {
        name = baseObject.name;
        speed.baseValue = baseObject.speed;
        attackSpeed.baseValue = baseObject.attackSpeed;
        attackPower.baseValue = baseObject.attackPower;
        rangeAttack.baseValue = baseObject.rangeAttack;
        maxHP = hp.baseValue = baseObject.hp;
    }

    public virtual void Update ()
    {
        Move ();
    }

    public virtual void FixedUpdate ()
    {

    }

    public abstract void Move ();
}
