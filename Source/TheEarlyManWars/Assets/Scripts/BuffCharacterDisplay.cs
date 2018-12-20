using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffCharacterDisplay : CharacterDisplay
{
    protected override IEnumerator AnimateAttack (IEnumerable<ObjectDisplay> enemies)
    {
        yield return StartCoroutine (AnimateAttack ());
        var atkPwrVal = attackPower.GetValue ();
        var allyArray = enemies.Where (x => x.GetInstanceID () != GetInstanceID ()).Select (x => (CharacterDisplay) x).ToArray ();
        var minRateHp = 1f;
        var minHpChar = allyArray.FirstOrDefault();
        foreach (var c in allyArray)
        {
            var rateHp = c.hp / c.maxHP;
            if(rateHp < minRateHp){
                minRateHp = rateHp;
                minHpChar = c;
            }
        }
        if (minHpChar.hp == currentEnemy.maxHP) yield break;
        minHpChar.hp += atkPwrVal;
    }

    public override IEnumerable<ObjectDisplay> DetectEnemies ()
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
}
