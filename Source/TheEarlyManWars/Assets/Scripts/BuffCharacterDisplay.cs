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
        var instanceId = GetInstanceID ();
        var allyArray = enemies.Where (x => x.GetInstanceID () != instanceId)
            .Select (x => (CharacterDisplay) x)
            .ToArray ();
        var minRateHp = 1f;
        var minHpChar = allyArray.FirstOrDefault ();
        if (minHpChar == null || minHpChar is Object && minHpChar.Equals (null)) yield break;
        foreach (var c in allyArray)
        {
            var rateHp = (float) c.hp / (float) c.maxHP;
            if (rateHp < minRateHp)
            {
                minRateHp = rateHp;
                minHpChar = c;
            }
        }
        if (minHpChar.hp >= minHpChar.maxHP)
        {
            minHpChar.hp = minHpChar.maxHP;
            yield break;
        }
        minHpChar.hp += atkPwrVal;
    }

    protected override IEnumerator Go ()
    {
        while (gameObject != null && !gameObject.Equals (null))
        {
            if (detectedEnemies.Any ())
            {
                if (detectedEnemies.Any (x => x.hp < x.maxHP))
                {
                    yield return StartCoroutine (PrepareAttack ());
                    isStopMove = true;
                    yield return StartCoroutine (AnimateAttack (detectedEnemies));
                }
                else
                {
                    // Do something...
                    var currentX = transform.position.x;
                    var frontAllies = allies.list
                        .Where (x => detectedEnemies.Any (x1 => x1.GetInstanceID () != x.GetInstanceID ()))
                        .Where (x => x.transform.position.x > currentX && x.hp < x.maxHP).ToList ();
                    if (frontAllies.Any ())
                    {
                        isStopMove = true;
                        Move ();
                        yield return new WaitForFixedUpdate ();
                    }
                    else
                    {
                        isStopMove = false;
                        yield return null;
                    }
                }
            }
            else
            {
                var currentX = transform.position.x;
                var frontAllies = allies.list.Where (e => e.transform.position.x > currentX).ToList ();
                if (frontAllies.Any ())
                {
                    isStopMove = true;
                    Move ();
                    yield return new WaitForFixedUpdate ();
                }
                else
                {
                    isStopMove = false;
                    yield return null;
                }
            }
        }
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
            case Direction.LeftToRight:
                return allies.list.Where (e => e.transform.position.x > currentX && e.transform.position.x <= rangeX);
            case Direction.RightToLeft:
                return allies.list.Where (e => e.transform.position.x >= rangeX && e.transform.position.x < currentX);
            default:
                return new List<ObjectDisplay> ();
        }
    }
}
