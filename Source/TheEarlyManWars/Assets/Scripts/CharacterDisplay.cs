using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : ObjectDisplay
{
    public AttackType attackType;
    protected ObjectDisplay currentEnemy;
    public Image healthBar;
    public Text nameText;
    [SerializeField]
    Transform _onDeathPoint;
    [SerializeField]
    ParticleSystem _onDeathEffectPrefab;
    Rigidbody2D _rb;

    public override void Awake ()
    {
        direction = Direction.LeftToRight;
        base.Awake ();
        enemyTower = FindObjectOfType<MonsterTowerDisplay> ();
        allies = FindObjectOfType<CharacterDisplayList> ();
        enemies = FindObjectOfType<MonsterDisplayList> ();
        _rb = GetComponent<Rigidbody2D> ();
    }

    public override void Start ()
    {
        base.Start ();
        attackType = ((BaseCharacter) baseObject).attackType;
        if (nameText != null && !nameText.Equals (null))
        {
            nameText.text = baseObject.name;
        }
    }

    public override void Update ()
    {
        base.Update ();
        if (healthBar != null && !healthBar.Equals (null))
        {
            healthBar.fillAmount = (float) hp / (float) maxHP;
        }
    }

    public override void OnDeath ()
    {
        if (AnimationHurtIsNotNull ())
        {
            animator.Play (animationHurt.name, 0);
        }
        // StartCoroutine (InstantiateOnDeathEffect ());
        StartCoroutine (NavigateDeathAction ());

    }

    IEnumerator InstantiateOnDeathEffect ()
    {
        var angle = Quaternion.Euler (-10f, -90f, 0f);
        var onDeathPosition = _onDeathPoint.Equals (null) ? transform.position : _onDeathPoint.position;
        var ins = Instantiate<ParticleSystem> (_onDeathEffectPrefab, onDeathPosition, angle);
        Destroy (ins.gameObject, ins.main.startLifetime.constant);
        yield break;
    }

    IEnumerator NavigateDeathAction ()
    {
        shadow.gameObject.SetActive (false);
        yield return StartCoroutine (JumpForDeath (-2f, 1f, 1f));
        yield return StartCoroutine (JumpForDeath (-1.15f, .5f, 1f));
        yield return StartCoroutine (Vanishing ());
        Destroy (gameObject);
    }

    IEnumerator JumpForDeath (float distance, float height, float deltaGravityScale)
    {
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rb.gravityScale *= settings.deltaSpeed * deltaGravityScale;
        var spriteTransform = spriteRenderer.transform;
        var positionAtDeath = new Vector3 (spriteTransform.position.x + distance, spriteTransform.position.y, spriteTransform.position.z);
        var gravity = JumpVelocityCalculator.GetGravity2D (_rb);
        var jumpVel = JumpVelocityCalculator.Calculate (spriteTransform.position, positionAtDeath, Vector3.zero, positionAtDeath, gravity, height, true);
        var _currentVel = jumpVel.velocity;
        _rb.velocity = _currentVel;
        yield return new WaitForSeconds (jumpVel.simulatedTime);
        _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    IEnumerator Vanishing ()
    {
        var t = 0f;
        var originColor = spriteRenderer.color;
        while (t <= 1f)
        {
            t += Time.deltaTime / .5f;

            originColor.a = Mathf.Lerp (1f, 0f, t);
            spriteRenderer.color = originColor;
            yield return null;
        }
    }

    protected override IEnumerator AnimateAttack (IEnumerable<ObjectDisplay> enemies)
    {
        yield return StartCoroutine (AnimateAttack ());
        var atkPwrVal = attackPower.GetValue ();
        atkPwrVal = atkPwrVal * (1 + technologyManager.meleeDamageRate);
        if (attackType == AttackType.AOEMelee)
        {
            var enemyArray = GetMonstersByAttackType (enemies);
            foreach (var enemy in enemyArray)
            {
                if (enemy == null || enemy is Object && enemy.Equals (null)) continue;
                if (enemy.attackType == MonsterAttackType.Air) continue;
                enemy.TakeDamage (atkPwrVal, this);
            }
        }
        else
        {
            var enemyArray = GetMonstersByAttackType (enemies);
            if (currentEnemy == null || currentEnemy is Object && currentEnemy.Equals (null))
            {
                currentEnemy = enemyArray.ElementAtOrDefault (Random.Range (0, enemyArray.Count ()));
            }
            if (currentEnemy != null && currentEnemy is Object && !currentEnemy.Equals (null))
            {
                currentEnemy.TakeDamage (atkPwrVal, this);
            }
        }
        if (AnimationIdleIsNotNull () && !dead)
        {
            var hitFn = animationAttack.events.FirstOrDefault (x => x.functionName == "Hit");
            if (hitFn != null)
            {
                yield return new WaitForSeconds (animationAttack.length - hitFn.time);
            }
            animator.Play (animationIdle.name, 0);
        }
        yield break;
    }

    IEnumerable<MonsterDisplay> GetMonstersByAttackType (IEnumerable<ObjectDisplay> monsters)
    {
        if (attackType == AttackType.Range)
        {
            return monsters.Select (x => (MonsterDisplay) x).ToArray ();
        }
        return monsters.Select (x => (MonsterDisplay) x).Where (x => x.attackType != MonsterAttackType.Air).ToArray ();
    }
}
