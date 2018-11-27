using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterDisplay : ObjectDisplay
{
    public MonsterAttackType attackType;
    ObjectDisplay _currentEnemy;

    public override void Awake ()
    {
        direction = Direction.RightToLeft;
        base.Awake ();
        enemyTower = FindObjectOfType<PlayerTower> ();
        attackType = ((BaseMonster) baseObject).attackType;
        allies = FindObjectOfType<MonsterDisplayList> ();
        enemies = FindObjectOfType<CharacterDisplayList> ();
    }

    public override void Attack (IEnumerable<ObjectDisplay> enemies)
    {
        var atkPwrVal = attackPower.GetValue ();
        if (_currentEnemy == null || _currentEnemy is Object && _currentEnemy.Equals (null))
        {
            _currentEnemy = enemies.ElementAt(Random.Range(0, enemies.Count()));
        }
        _currentEnemy.TakeDamage (atkPwrVal);
    }
}
