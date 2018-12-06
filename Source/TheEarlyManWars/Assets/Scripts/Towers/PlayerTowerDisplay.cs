using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTowerDisplay : TowerDisplay
{
    public Arrow arrowPrefab;
    public Transform arrowLaunchPoint;

    public override void Awake ()
    {
        direction = Direction.LeftToRight;
        enemies = FindObjectOfType<MonsterDisplayList> ();
        base.Awake ();
    }

    protected override IEnumerator AnimateAttack (IEnumerable<ObjectDisplay> enemies)
    {
        var arrowIns = Instantiate<Arrow>(arrowPrefab, arrowLaunchPoint.position, Quaternion.identity);
        var enemy = enemies.ElementAt(Random.Range(0, enemies.Count()));
        if(enemy != null && !enemy.Equals(null)){
            arrowIns.Launch(enemy.transform.position, () => {
                if(enemy != null && !enemy.Equals(null)){
                    enemy.TakeDamage(arrowIns.damage, this);
                }
            });
        }
        yield break;
    }
}
