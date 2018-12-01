using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterDisplay : ObjectDisplay
{
    MeatSystem _meatSystem;
    MonsterAttackType _attackType;
    int _gainedMeat;
    ObjectDisplay _currentEnemy;

    public override void Awake ()
    {
        direction = Direction.RightToLeft;
        base.Awake ();
        enemyTower = FindObjectOfType<PlayerTower> ();
        allies = FindObjectOfType<MonsterDisplayList> ();
        enemies = FindObjectOfType<CharacterDisplayList> ();
        _meatSystem = FindObjectOfType<MeatSystem> ();
    }

    public override void Start ()
    {
        base.Start ();
        var baseMonster = (BaseMonster) baseObject;
        _attackType = baseMonster.attackType;
        _gainedMeat = baseMonster.gainedMeat;
    }

    public override void Attack (IEnumerable<ObjectDisplay> enemies)
    {
        var atkPwrVal = attackPower.GetValue ();
        if (_currentEnemy == null || _currentEnemy is Object && _currentEnemy.Equals (null))
        {
            _currentEnemy = enemies.ElementAt (Random.Range (0, enemies.Count ()));
        }
        _currentEnemy.TakeDamage (atkPwrVal, this);
    }

    public override void OnDeath (ObjectDisplay damagedBy)
    {
        _meatSystem.Gain(_gainedMeat);
    }
}
