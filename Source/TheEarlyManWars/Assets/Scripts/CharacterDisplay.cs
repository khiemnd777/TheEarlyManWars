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
        enemyTower = FindObjectOfType<MonsterTower> ();
        allies = FindObjectOfType<CharacterDisplayList> ();
        enemies = FindObjectOfType<MonsterDisplayList> ();
    }

    public override void Start ()
    {
        base.Start ();
        attackType = ((BaseCharacter) baseObject).attackType;
    }

    public override void Attack (IEnumerable<ObjectDisplay> enemies)
    {
        var atkPwrVal = attackPower.GetValue ();
        if (attackType == AttackType.AOEMelee)
        {
            foreach (var enemy in enemies)
            {
                enemy.TakeDamage (atkPwrVal, this);
            }
        }
        else
        {
            if (currentEnemy == null || currentEnemy is Object && currentEnemy.Equals (null))
            {
                currentEnemy = enemies.First();
            }
            if(currentEnemy is Object && !currentEnemy.Equals(null))
                currentEnemy.TakeDamage (atkPwrVal, this);
        }
    }
}
