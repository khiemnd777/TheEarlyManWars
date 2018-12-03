using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectDisplay : MonoBehaviour
{
    public BaseObject baseObject;
    // Animation & Animator
    [System.NonSerialized]
    public Animator animator;
    [System.NonSerialized]
    public AnimationClip animationAttack;
    // List of object  display
    [System.NonSerialized]
    public ObjectDisplayList allies;
    [System.NonSerialized]
    public ObjectDisplayList enemies;
    [System.NonSerialized]
    public Tower enemyTower;
    // Settings
    [System.NonSerialized]
    public Settings settings;
    // Stats
    public Stat speed;
    public Stat attackSpeed;
    public Stat attackPower;
    public Stat rangeAttack;
    public int hp;
    public int maxHP;
    [System.NonSerialized]
    public Direction direction;
    float _attackTime = 0f;
    IEnumerable<ObjectDisplay> _detectedEnemies;
    Tower _detectedTower;

    public virtual void Awake ()
    {
        settings = FindObjectOfType<Settings> ();
    }

    public virtual void Start ()
    {
        name = baseObject.name;
        if (baseObject.animator != null)
        {
            animator = baseObject.animator;
        }
        if (baseObject.animationAttack != null)
        {
            animationAttack = baseObject.animationAttack;
        }
        speed.baseValue = baseObject.speed;
        attackSpeed.baseValue = baseObject.attackSpeed;
        attackPower.baseValue = baseObject.attackPower;
        rangeAttack.baseValue = baseObject.rangeAttack;
        maxHP = hp = baseObject.hp;
        StartCoroutine (ScanEnemies ());
        StartCoroutine (ScanTower ());
        StartCoroutine (Go ());
    }

    public virtual void Update ()
    {

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
        if (settings.deltaSpeed <= 0) return false;
        if (Time.time < _attackTime) return false;
        var atkSpdVal = attackSpeed.GetValue ();
        _attackTime = Time.time + settings.deltaAttackTime / (atkSpdVal * settings.deltaSpeed);
        return true;
    }

    public virtual void Move ()
    {
        var spdVal = speed.GetValue ();
        switch (baseObject.moveType)
        {
            case MoveType.OnGround:
                transform.position += Vector3.right * (int) direction * spdVal * settings.deltaSpeed * settings.deltaMoveStep * Time.fixedDeltaTime;
                break;
            case MoveType.InAir:
                // Temp
                transform.position += Vector3.right * (int) direction * spdVal * settings.deltaSpeed * settings.deltaMoveStep * Time.fixedDeltaTime;
                break;
            default:
                break;
        }
    }

    public virtual void TakeDamage (int damage, ObjectDisplay damgedBy)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Debug.Log (name + " being killed!");
            OnDeath (damgedBy);
            allies.Remove (this);
            Destroy (gameObject);
        }
    }

    public virtual void OnDeath (ObjectDisplay damagedBy)
    {

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

    // Hit function is used for being flag to determine the fired off of get-hit-time.
    public void Hit ()
    {

    }

    IEnumerator Go ()
    {
        while (gameObject != null && !gameObject.Equals (null))
        {
            if (_detectedEnemies.Any ())
            {
                if (PrepareAttack ())
                {
                    yield return StartCoroutine (AnimateAttack (_detectedEnemies));
                    // if (AnimationAttackIsNotNull ())
                    // {
                    //     yield return StartCoroutine (AnimateAttack (_detectedEnemies));
                    // }
                    // else
                    // {
                    //     Attack (_detectedEnemies);
                    // }
                }
                else
                {
                    yield return null;
                }
            }
            else
            {
                if (_detectedTower != null)
                {
                    if (PrepareAttack ())
                    {
                        yield return StartCoroutine (AnimateAttack (_detectedTower));
                        // if (AnimationAttackIsNotNull ())
                        // {
                        //     yield return StartCoroutine (AnimateAttack (_detectedTower));
                        // }
                        // else
                        // {
                        //     Attack (_detectedTower);
                        // }
                    }
                    else
                    {
                        yield return null;
                    }
                }
                else
                {
                    Move ();
                    yield return new WaitForFixedUpdate ();
                }
            }
        }
    }

    bool AnimationAttackIsNotNull ()
    {
        return animator != null && !animator.Equals (null) && animationAttack != null && !animationAttack.Equals (null);
    }

    protected virtual IEnumerator AnimateAttack ()
    {
        if (!AnimationAttackIsNotNull ()) yield break;
        animator.Play (animationAttack.name, 0);
        var hitFn = animationAttack.events.FirstOrDefault (x => x.functionName == "Hit");
        if (hitFn != null)
        {
            yield return new WaitForSeconds (hitFn.time);
        }
    }

    protected virtual IEnumerator AnimateAttack (IEnumerable<ObjectDisplay> enemies)
    {
        if (!enemies.Any ()) yield break;
        yield return StartCoroutine (AnimateAttack ());
        Attack (enemies);
    }

    protected virtual IEnumerator AnimateAttack (Tower tower)
    {
        if (tower == null || tower is Object && tower.Equals (null)) yield break;
        yield return StartCoroutine (AnimateAttack ());
        Attack (tower);
    }

    IEnumerator ScanEnemies ()
    {
        while (gameObject != null && !gameObject.Equals (null))
        {
            _detectedEnemies = DetectEnemies ();
            yield return null;
        }
    }

    IEnumerator ScanTower ()
    {
        while (gameObject != null && !gameObject.Equals (null))
        {
            _detectedTower = DetectEnemyTower ();
            yield return null;
        }
    }
}
