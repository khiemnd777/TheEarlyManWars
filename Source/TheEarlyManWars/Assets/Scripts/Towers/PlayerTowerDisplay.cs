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

    ObjectDisplay _currentEnemy;

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
        var arrowIns = Instantiate<Arrow> (arrowPrefab, arrowLaunchPoint.position, Quaternion.identity);
        var enemyArray = enemies.ToArray ();
        if (_currentEnemy == null || _currentEnemy is Object && _currentEnemy.Equals (null))
        {
            _currentEnemy = enemyArray.FirstOrDefault ();
        }
        if (_currentEnemy != null && _currentEnemy is Object && !_currentEnemy.Equals (null))
        {
            var deltaDistance = Vector3.right * (int) direction * _currentEnemy.speed.GetValue () * settings.deltaSpeed * settings.deltaMoveStep;
            var stopPos = new Vector3 (transform.position.x + _currentEnemy.rangeAttack.GetValue (), transform.position.y, transform.position.z);
            arrowIns.Launch (_currentEnemy.transform.position, deltaDistance, stopPos, settings.deltaSpeed, () =>
            {
                if (_currentEnemy != null && !_currentEnemy.Equals (null))
                {
                    _currentEnemy.TakeDamage (arrowIns.damage, this);
                }
            });
        }
        yield break;
    }
}
