using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTowerDisplay : TowerDisplay
{
    public Arrow arrowPrefab;
    public Transform arrowLaunchPoint;
    public Image healthBar;

    public override void Awake ()
    {
        direction = Direction.LeftToRight;
        enemies = FindObjectOfType<MonsterDisplayList> (); 
        base.Awake ();
    }

    public override void Update ()
    {
        if (healthBar != null && !healthBar.Equals (null))
        {
            healthBar.fillAmount = (float) hp / (float) maxHP;
        }
    }


    protected override IEnumerator AnimateAttack (IEnumerable<ObjectDisplay> enemies)
    {
        var arrowIns = Instantiate<Arrow>(arrowPrefab, arrowLaunchPoint.position, Quaternion.identity);
        var enemy = enemies.ElementAt(Random.Range(0, enemies.Count()));
        if(enemy != null && !enemy.Equals(null)){
            arrowIns.Launch(enemy.transform.position, settings.deltaSpeed, () => {
                if(enemy != null && !enemy.Equals(null)){
                    enemy.TakeDamage(arrowIns.damage, this);
                }
            });
        }
        yield break;
    }
}
