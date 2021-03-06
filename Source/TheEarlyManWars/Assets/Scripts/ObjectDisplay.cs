using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class ObjectDisplay : Display
{
    public BaseObject baseObject;
    // Animation & Animator
    [Header ("Animation & Animator")]
    public Animator animator;
    public AnimationClip animationAttack;
    public AnimationClip animationHurt;
    public AnimationClip animationWalk;
    public AnimationClip animationIdle;
    [Header ("Renderer")]
    public SpriteRenderer bodySpriteRenderer;
    public Transform shadow;
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
    [System.NonSerialized]
    public TechnologyManager technologyManager;
    [Space]
    // Stats
    public SpeedEnum speed;
    public AttackSpeedEnum attackSpeed;
    public RangeAttackEnum rangeAttack;
    public Stat attackPower;
    public float hp;
    public float maxHP;
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
    public float knockBackRange
    {
        get;
        private set;
    }
    // Others
    [System.NonSerialized]
    public bool dead;
    [System.NonSerialized]
    public Direction direction;
    protected IEnumerable<ObjectDisplay> detectedEnemies;
    protected IEnumerable<ObjectDisplay> detectedAllies;
    [System.NonSerialized]
    public bool isStopMove;
    TowerDisplay _detectedTower;
    // Shake
    [SerializeField]
    ObjectShake _shake;
    float _attackSpeedSecond;
    bool _firstAttack = true;
    bool _stopMove;

    public virtual void Awake ()
    {
        settings = FindObjectOfType<Settings> ();
        technologyManager = FindObjectOfType<TechnologyManager> ();
        _shake = GetComponentInChildren<ObjectShake> ();
    }

    public virtual void Start ()
    {
        name = baseObject.name;
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
        knockBackRange = baseObject.knockBackRange;
        // StartCoroutine (ScanEnemies ());
        // StartCoroutine (ScanTower ());
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

    protected virtual IEnumerator PreAttack ()
    {
        if (!AnimationAttackIsNotNull ()) yield break;
        animator.Play (animationAttack.name, 0, 0);
        var hitFn = animationAttack.events.FirstOrDefault (x => x.functionName == "Hit");
        if (hitFn != null)
        {
            var hitTime = hitFn.time;
            // var atkSpdVal = attackSpeed.GetValue ();
            // var animTime = animationAttack.length;
            // if (atkSpdVal > 0f)
            // {
            //     var atkTime = 1 / atkSpdVal;
            //     var realTimeRate = animTime / atkTime;
            //     animator.speed = realTimeRate;
            //     animator.Play (animationAttack.name, 0, 0);
            //     var realStartToAttackTime = hitTime / realTimeRate;
            //     yield return new WaitForSeconds (realStartToAttackTime);
            // }
            // else
            // {
            //     yield return new WaitForSeconds (hitTime);
            // }
            yield return new WaitForSeconds (hitTime);
        }
    }

    protected virtual IEnumerator PostAttack ()
    {
        if (_firstAttack)
        {
            _firstAttack = false;
        }
        var atkSpdVal = attackSpeed.GetValue ();
        var atkTime = 1 / atkSpdVal;
        yield return new WaitForSeconds (atkTime);
        // if (AnimationAttackIsNotNull ())
        // {
        //     var hitFn = animationAttack.events.FirstOrDefault (x => x.functionName == "Hit");
        //     if (hitFn != null)
        //     {
        //         var animTime = animationAttack.length;
        //         var hitTime = hitFn.time;
        //         if (atkSpdVal > 0f)
        //         {
        //             var realTimeRate = animTime / atkTime;
        //             animator.speed = realTimeRate;
        //             var realAttackToEndTime = (animTime - hitTime) / realTimeRate;
        //             yield return new WaitForSeconds (realAttackToEndTime);
        //         }
        //         else
        //         {
        //             yield return new WaitForSeconds (animTime - hitTime);
        //         }
        //     }
        //     else
        //     {
        //         yield return new WaitForSeconds (atkTime);
        //     }
        // }
        // else
        // {
        //     yield return new WaitForSeconds (atkTime);
        // }
        if (animator)
        {
            animator.speed = 1;
        }
    }

    public virtual void Move ()
    {
        if (_stopMove) return;
        var spdVal = speed.GetValue ();
        switch (baseObject.moveType)
        {
            case MoveType.OnGround:
                transform.position += Vector3.right * (int) direction * spdVal * settings.deltaMoveStep * Time.fixedDeltaTime;
                break;
            case MoveType.InAir:
                // Temp
                transform.position += Vector3.right * (int) direction * spdVal * settings.deltaMoveStep * Time.fixedDeltaTime;
                break;
            default:
                break;
        }
    }

    public virtual void TakeDamage (float damage)
    {
        if (dead) return;
        hp -= damage;
        StartCoroutine (_shake.Shake ());
        if (hp <= 0)
        {
            dead = true;
            Debug.Log (name + " being killed!");
            OnDeath ();
            allies.Remove (this);
        }
    }

    public virtual void TakeDamage (float damage, ObjectDisplay damagedBy)
    {
        if (dead) return;
        hp -= damage;
        StartCoroutine (_shake.Shake ());
        if (damagedBy.canKnockBack)
        {
            var prob = damagedBy.knockBackProbability * 100f;
            var procKnockBacks = Probability.Initialize<bool> (new [] { true, false }, new [] { prob, 100f - prob });
            var procKnockBack = Probability.GetValueInProbability (procKnockBacks);
            if (procKnockBack)
            {
                StartCoroutine (KnockBack (damagedBy));
            }
        }
        if (hp <= 0)
        {
            dead = true;
            Debug.Log (name + " being killed!");
            OnDeath (damagedBy);
            allies.Remove (this);
        }
    }

    public virtual void TakeDamage (float damage, TowerDisplay damagedBy)
    {
        if (dead) return;
        hp -= damage;
        StartCoroutine (_shake.Shake ());
        if (hp <= 0)
        {
            dead = true;
            Debug.Log (name + " being killed!");
            OnDeath (damagedBy);
            allies.Remove (this);
        }
    }

    IEnumerator KnockBack (ObjectDisplay damageBy)
    {
        StopMove ();
        animator.Play (animationHurt.name, 0, 0);
        var percent = 0f;
        var originPos = transform.position;
        var targetPos = new Vector2 (transform.position.x - damageBy.knockBackRange, transform.position.y);
        while (percent <= 1f)
        {
            var step = Time.deltaTime * settings.deltaMoveStep * 12f;
            percent += step;
            transform.position = Vector2.Lerp (originPos, targetPos, percent);
            yield return null;
        }
        animator.Play (animationIdle.name, 0, 0);
        CanMove ();
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
        Destroy (gameObject);
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
                return enemies.list.Where (e => !e.dead && e.transform.position.x > currentX && e.transform.position.x <= rangeX);
            case Direction.RightToLeft:
                return enemies.list.Where (e => !e.dead && e.transform.position.x >= rangeX && e.transform.position.x < currentX);
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
        while (gameObject != null && !gameObject.Equals (null) && !dead)
        {
            detectedEnemies = DetectEnemies ();
            if (detectedEnemies.Any ())
            {
                if (AnimationIdleIsNotNull () && !dead)
                {
                    animator.Play (animationIdle.name, 0);
                }
                isStopMove = true;
                yield return StartCoroutine (PreAttack ());
                yield return StartCoroutine (AnimateAttack (detectedEnemies));
                yield return StartCoroutine (PostAttack ());
            }
            else
            {
                _detectedTower = DetectEnemyTower ();
                if (_detectedTower != null)
                {
                    if (AnimationIdleIsNotNull () && !dead)
                    {
                        animator.Play (animationIdle.name, 0);
                    }
                    isStopMove = true;
                    yield return StartCoroutine (PreAttack ());
                    yield return StartCoroutine (AnimateAttack (_detectedTower));
                    yield return StartCoroutine (PostAttack ());
                }
                else
                {
                    if (AnimationWalkIsNotNull () && !dead)
                    {
                        animator.Play (animationWalk.name, 0);
                    }
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

    public bool AnimationWalkIsNotNull ()
    {
        return animator != null && !animator.Equals (null) && animationWalk != null && !animationWalk.Equals (null);
    }

    public bool AnimationHurtIsNotNull ()
    {
        return animator != null && !animator.Equals (null) && animationHurt != null && !animationHurt.Equals (null);
    }

    public bool AnimationIdleIsNotNull ()
    {
        return animator != null && !animator.Equals (null) && animationIdle != null && !animationIdle.Equals (null);
    }

    protected virtual IEnumerator AnimateAttack ()
    {
        yield break;
        // if (!AnimationAttackIsNotNull ()) yield break;
        // animator.Play (animationAttack.name, 0);
        // var hitFn = animationAttack.events.FirstOrDefault (x => x.functionName == "Hit");
        // if (hitFn != null)
        // {
        //     var atkSpdVal = attackSpeed.GetValue ();
        //     if (atkSpdVal > 0f)
        //     {
        //         var animTime = animationAttack.length;
        //         var hitTime = hitFn.time;
        //         var atkTime = 1 / atkSpdVal;
        //         var realTimeRate = animTime / atkTime;
        //         animator.speed = realTimeRate;
        //         var realStartToAttackTime = hitTime / realTimeRate;
        //         yield return new WaitForSeconds (realStartToAttackTime);
        //         animator.speed = 1;
        //     }
        //     else
        //     {
        //         yield return new WaitForSeconds (hitFn.time);
        //     }
        // }
        // else
        // {
        //     yield return new WaitForSeconds (animationAttack.length);
        // }
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
        var atkPwrVal = attackPower.GetValue () * (1 + technologyManager.meleeDamageRate);
        tower.TakeDamage (atkPwrVal, this);
        if (AnimationIdleIsNotNull () && !dead)
        {
            var hitFn = animationAttack.events.FirstOrDefault (x => x.functionName == "Hit");
            if (hitFn != null)
            {
                yield return new WaitForSeconds (animationAttack.length - hitFn.time);
            }
            animator.Play (animationIdle.name, 0);
        }
    }
}
