using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectDisplay : MonoBehaviour
{
    public BaseObject baseObject;
    public ObjectDisplayList allies;
    public ObjectDisplayList enemies;
    public Tower enemyTower;
    [System.NonSerialized]
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
    }

    public virtual void Start ()
    {
        name = baseObject.name;
        speed.baseValue = baseObject.speed;
        attackSpeed.baseValue = baseObject.attackSpeed;
        attackPower.baseValue = baseObject.attackPower;
        rangeAttack.baseValue = baseObject.rangeAttack;
        maxHP = hp = baseObject.hp;
    }

    public virtual void Update ()
    {
        var detectedList = DetectEnemies ();
        if (detectedList.Any ())
        {
            if (PrepareAttack ())
            {
                Attack (detectedList);
            }
        }
        else
        {
            var detectedEnemyTower = DetectEnemyTower ();
            if (detectedEnemyTower != null)
            {
                if (PrepareAttack ())
                {
                    Attack (detectedEnemyTower);
                }
            }
            else
            {
                Move ();
            }
        }
    }

    public virtual void FixedUpdate ()
    {

    }

    public abstract void Attack (IEnumerable<ObjectDisplay> enemies);

    public virtual void Attack (Tower tower)
    {
        var atkPwrVal = attackPower.GetValue ();
        tower.TakeDamage (atkPwrVal);
    }

    bool PrepareAttack ()
    {
        if(settings.deltaSpeed <= 0) return false;
        if (Time.time < _attackTime) return false;
        var atkSpdVal = attackSpeed.GetValue ();
        _attackTime = Time.time + settings.deltaAttackTime / (atkSpdVal * settings.deltaSpeed);
        return true;
    }

    public virtual void Move ()
    {
        var spdVal = speed.GetValue ();
        transform.position += Vector3.right * (int) direction * spdVal * settings.deltaSpeed * settings.deltaMoveStep * Time.fixedDeltaTime;
    }

    public virtual void TakeDamage (int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Debug.Log (name + " being killed!");
            allies.Remove (this);
            Destroy (gameObject);
        }
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

    public virtual Tower DetectEnemyTower ()
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
                return enemyTower.transform.position.x > currentX && enemyTower.transform.position.x <= rangeX ? enemyTower : null;
            case Direction.RightToLeft:
                return enemyTower.transform.position.x >= rangeX && enemyTower.transform.position.x < currentX ? enemyTower : null;
            default:
                return null;
        }
    }
}
