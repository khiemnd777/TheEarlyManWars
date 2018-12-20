using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterDisplay : ObjectDisplay
{
    public AttackType attackType;
    protected ObjectDisplay currentEnemy;

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
        else if (attackType == AttackType.Range)
        {
            var enemyArray = GetMonstersByAttackType (enemies);
            if (currentEnemy == null || currentEnemy is Object && currentEnemy.Equals (null))
            {
                currentEnemy = enemyArray.FirstOrDefault ();
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
