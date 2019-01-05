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

    public override void Awake ()
    {
        direction = Direction.LeftToRight;
        base.Awake ();
        enemyTower = FindObjectOfType<MonsterTowerDisplay> ();
        allies = FindObjectOfType<CharacterDisplayList> ();
        enemies = FindObjectOfType<MonsterDisplayList> ();
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
        if (healthBar != null && !healthBar.Equals (null))
        {
            healthBar.fillAmount = (float) hp / (float) maxHP;
        }
    }

    public override void OnDeath ()
    {
        StartCoroutine (InstantiateOnDeathEffect ());
    }

    IEnumerator InstantiateOnDeathEffect ()
    {
        var angle = Quaternion.Euler (-10f, -90f, 0f);
        var onDeathPosition = _onDeathPoint.Equals(null) ? transform.position : _onDeathPoint.position;
        var ins = Instantiate<ParticleSystem> (_onDeathEffectPrefab, onDeathPosition, angle);
        Destroy (ins.gameObject, ins.main.startLifetime.constant);
        yield break;
    }

    protected override IEnumerator AnimateAttack (IEnumerable<ObjectDisplay> enemies)
    {
        yield return StartCoroutine (AnimateAttack ());
        var atkPwrVal = attackPower.GetValue ();
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
                currentEnemy = enemyArray.ElementAtOrDefault(Random.Range(0, enemyArray.Count()));
            }
            if (currentEnemy != null && currentEnemy is Object && !currentEnemy.Equals (null))
            {
                currentEnemy.TakeDamage (atkPwrVal, this);
            }
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
