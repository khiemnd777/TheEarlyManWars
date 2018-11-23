using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectDisplay : MonoBehaviour
{
    public BaseObject baseObject;
    public ObjectDisplayList allies;
    public ObjectDisplayList enemies;
    public Settings settings;
    public Stat speed;
    public Stat attackSpeed;
    public Stat attackPower;
    public Stat rangeAttack;
    public int hp;
    public int maxHP;
    public Direction direction;
    float _attackTime = 0f;

    public virtual void Awake ()
    {
        settings = FindObjectOfType<Settings> ();
        name = baseObject.name;
        speed.baseValue = baseObject.speed;
        attackSpeed.baseValue = baseObject.attackSpeed;
        attackPower.baseValue = baseObject.attackPower;
        rangeAttack.baseValue = baseObject.rangeAttack;
        maxHP = hp = baseObject.hp;
    }

    public virtual void Update ()
    {
        Move ();
    }

    public virtual void FixedUpdate ()
    {

    }

    public abstract void Attack (IEnumerable<ObjectDisplay> enemies);

    void PrepareAttack (IEnumerable<ObjectDisplay> enemies)
    {
        if (Time.time < _attackTime) return;
        var atkSpdVal = attackSpeed.GetValue ();
        _attackTime = Time.time + settings.deltaAttackTime / (atkSpdVal * settings.deltaSpeed);
        Attack(enemies);
    }

    public virtual void Move ()
    {
        var detectedList = DetectEnemies ();
        if (detectedList.Any ())
        {
            PrepareAttack (detectedList);
            return;
        }
        var spdVal = speed.GetValue ();
        transform.position += Vector3.right * (int) direction * spdVal * settings.deltaSpeed * Time.fixedDeltaTime;
    }

    public virtual void TakeDamage (int damage)
    {
        if (hp <= 0)
        {
            Debug.Log (name + " being killed!");
            allies.Remove (this);
            Destroy (gameObject);
            return;
        }
        hp -= damage;
    }

    public virtual IEnumerable<ObjectDisplay> DetectEnemies ()
    {
        var atkRangeVal = rangeAttack.GetValue ();
        if (settings.debug)
        {
            Debug.DrawRay (transform.position, Vector3.right * (int) direction * atkRangeVal, Color.yellow);
        }
        var currentX = transform.position.x;
        var rangeX = currentX + atkRangeVal * (int) direction;
        switch (direction)
        {
            case Direction.LeftToRight:
                return enemies.list.Where (e => e.transform.position.x > currentX && e.transform.position.x <= rangeX);
            case Direction.RightToLeft:
                return enemies.list.Where (e => e.transform.position.x >= rangeX && e.transform.position.x < currentX);
            default:
                return new List<ObjectDisplay> ();
        }
    }
}
