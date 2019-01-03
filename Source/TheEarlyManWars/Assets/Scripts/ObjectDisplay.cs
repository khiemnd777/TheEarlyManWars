using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
    public TowerDisplay enemyTower;
    // Settings
    [System.NonSerialized]
    public Settings settings;
    // Stats
    public SpeedEnum speed;
    public AttackSpeedEnum attackSpeed;
    public RangeAttackEnum rangeAttack;
    public Stat attackPower;
    public int hp;
    public int maxHP;
    // Knock back
    public bool canKnockBack
    {
        get;
        private set;
    }
    public float knockBackProbability
    {
        get;
        private set;
    }
    // Others
    [System.NonSerialized]
    public Direction direction;
    protected IEnumerable<ObjectDisplay> detectedEnemies;
    protected IEnumerable<ObjectDisplay> detectedAllies;
    TowerDisplay _detectedTower;
    protected bool isStopMove;
    float attackSpeedSecond;

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
        speed = baseObject.speed;
        attackSpeed = baseObject.attackSpeed;
        rangeAttack = baseObject.rangeAttack;
        attackPower.baseValue = baseObject.attackPower;
        maxHP = hp = baseObject.hp;
        canKnockBack = baseObject.canKnockBack;
        knockBackProbability = baseObject.knockBackProbability;
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

    public virtual void Attack (TowerDisplay tower)
    {

    }

    protected virtual IEnumerator PrepareAttack ()
    {
        if (settings.deltaSpeed <= 0) yield break;
        var atkSpdVal = attackSpeed.GetValue ();
        if (atkSpdVal == 0) yield break;
        var percent = 0f;
        while (percent <= 1f)
        {
            percent += Time.deltaTime * (atkSpdVal * settings.deltaAttackTime) * settings.deltaSpeed;
            yield return null;
        }
    }

    public virtual void Move ()
    {
        if (_stopMove) return;
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

    public virtual void TakeDamage (int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Debug.Log (name + " being killed!");
            OnDeath ();
            allies.Remove (this);
            Destroy (gameObject);
        }
    }

    public virtual void TakeDamage (int damage, ObjectDisplay damagedBy)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Debug.Log (name + " being killed!");
            OnDeath (damagedBy);
            allies.Remove (this);
            Destroy (gameObject);
        }
    }

    public virtual void TakeDamage (int damage, TowerDisplay damagedBy)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Debug.Log (name + " being killed!");
            OnDeath (damagedBy);
            allies.Remove (this);
            Destroy (gameObject);
        }
    }

    public virtual void OnDeath (ObjectDisplay damagedBy)
    {
        OnDeath ();
    }

    public virtual void OnDeath (TowerDisplay damagedBy)
    {
        OnDeath ();
    }

    public virtual void OnDeath ()
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

    public virtual IEnumerable<ObjectDisplay> DetectAllies ()
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
            case Direction.RightToLeft:
                return allies.list.Where (e => e.transform.position.x > currentX && e.transform.position.x <= rangeX);
            case Direction.LeftToRight:
                return allies.list.Where (e => e.transform.position.x >= rangeX && e.transform.position.x < currentX);
            default:
                return new List<ObjectDisplay> ();
        }
    }

    public virtual TowerDisplay DetectEnemyTower ()
    {
        if (enemyTower == null || enemyTower is Object && enemyTower.Equals (null)) return null;
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

    bool _stopMove;
    public void StopMove ()
    {
        _stopMove = true;
    }

    public void CanMove ()
    {
        _stopMove = false;
    }

    protected virtual IEnumerator Go ()
    {
        while (gameObject != null && !gameObject.Equals (null))
        {
            if (detectedEnemies.Any ())
            {
                yield return StartCoroutine (PrepareAttack ());
                isStopMove = true;
                yield return StartCoroutine (AnimateAttack (detectedEnemies));
            }
            else
            {
                if (_detectedTower != null)
                {
                    yield return StartCoroutine (PrepareAttack ());
                    isStopMove = true;
                    yield return StartCoroutine (AnimateAttack (_detectedTower));
                }
                else
                {
                    isStopMove = false;
                    Move ();
                    yield return new WaitForFixedUpdate ();
                }
            }
        }
    }

    public bool AnimationAttackIsNotNull ()
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
    }

    protected virtual IEnumerator AnimateAttack (TowerDisplay tower)
    {
        if (tower == null || tower is Object && tower.Equals (null)) yield break;
        yield return StartCoroutine (AnimateAttack ());
        if (tower == null || tower is Object && tower.Equals (null)) yield break;
        var atkPwrVal = attackPower.GetValue ();
        tower.TakeDamage (atkPwrVal, this);
    }

    IEnumerator ScanEnemies ()
    {
        while (gameObject != null && !gameObject.Equals (null))
        {
            detectedEnemies = DetectEnemies ();
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
