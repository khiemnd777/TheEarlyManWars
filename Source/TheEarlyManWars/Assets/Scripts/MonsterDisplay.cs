using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterDisplay : ObjectDisplay
{
    [System.NonSerialized]
    public MonsterAttackType attackType;
    MeatSystem _meatSystem;
    int _gainedMeat;
    ObjectDisplay _currentEnemy;

    public override void Awake ()
    {
        direction = Direction.RightToLeft;
        base.Awake ();
        enemyTower = FindObjectOfType<PlayerTowerDisplay> ();
        allies = FindObjectOfType<MonsterDisplayList> ();
        enemies = FindObjectOfType<CharacterDisplayList> ();
        _meatSystem = FindObjectOfType<MeatSystem> ();
    }

    public override void Start ()
    {
        base.Start ();
        var baseMonster = (BaseMonster) baseObject;
        attackType = baseMonster.attackType;
        _gainedMeat = baseMonster.gainedMeat;
    }

    protected override IEnumerator AnimateAttack (IEnumerable<ObjectDisplay> enemies)
    {
        if(!enemies.Any()) yield break;
        yield return StartCoroutine(AnimateAttack());
        var atkPwrVal = attackPower.GetValue ();
        if (_currentEnemy == null || _currentEnemy is Object && _currentEnemy.Equals (null))
        {
            _currentEnemy = enemies.FirstOrDefault();
        }
        if(_currentEnemy != null && _currentEnemy is Object && !_currentEnemy.Equals (null)){
            _currentEnemy.TakeDamage (atkPwrVal, this);
        }
        yield break;
    }

    public override void OnDeath (TowerDisplay damagedBy)
    {
        OnDeath();
    }

    public override void OnDeath (ObjectDisplay damagedBy)
    {
        OnDeath();
    }

    public override void OnDeath ()
    {
        _meatSystem.Gain (_gainedMeat);
    }
}
