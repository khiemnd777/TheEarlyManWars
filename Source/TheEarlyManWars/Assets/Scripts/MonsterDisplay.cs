using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterDisplay : ObjectDisplay
{
    public MonsterAttackType attackType;
    float _attackTime = 0f;

    public override void Awake ()
    {
        direction = Direction.RightToLeft;
        base.Awake ();
        attackType = ((BaseMonster) baseObject).attackType;
        allies = FindObjectOfType<MonsterDisplayList> ();
        enemies = FindObjectOfType<CharacterDisplayList> ();
    }

    public override void Attack (IEnumerable<ObjectDisplay> enemies)
    {
        var atkPwrVal = attackPower.GetValue();
        var enemy = enemies.First ();
        enemy.TakeDamage (atkPwrVal);
    }
}
