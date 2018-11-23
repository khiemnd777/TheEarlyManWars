using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterDisplay : ObjectDisplay
{
    public AttackType attackType;
    float _attackTime = 0f;

    public override void Awake ()
    {
        direction = Direction.LeftToRight;
        base.Awake ();
        attackType = ((BaseCharacter) baseObject).attackType;
        allies = FindObjectOfType<CharacterDisplayList> ();
        enemies = FindObjectOfType<MonsterDisplayList> ();
    }

    public override void Attack (IEnumerable<ObjectDisplay> enemies)
    {
        var atkPwrVal = attackPower.GetValue();
        if (attackType == AttackType.AOEMelee)
        {
            foreach (var enemy in enemies)
            {
                enemy.TakeDamage(atkPwrVal);
            }
        }
        else
        {
            var enemy = enemies.First();
            enemy.TakeDamage(atkPwrVal);
        }
    }
}
