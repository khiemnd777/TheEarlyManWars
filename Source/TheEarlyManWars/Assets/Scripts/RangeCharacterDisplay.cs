using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeCharacterDisplay : CharacterDisplay
{
    public ProjectileObject projectileObjectPrefab;
    [SerializeField]
    Transform _projectilePoint;
    TechnologyManager _technologyManager;

    public override void Start ()
    {
        base.Start ();
        _technologyManager = FindObjectOfType<TechnologyManager> ();
    }

    // Hit function is used for being flag to determine launching time.
    public void Launch ()
    {

    }

    protected override IEnumerator AnimateAttack (IEnumerable<ObjectDisplay> enemies)
    {
        if (!enemies.Any ()) yield break;
        if (AnimationAttackIsNotNull ())
        {
            yield return StartCoroutine (PrepareAnimateLaunch ());
        }
        var projectileIns = Instantiate<ProjectileObject> (projectileObjectPrefab, _projectilePoint.position, Quaternion.identity);
        projectileIns.direction = direction;
        if (currentEnemy == null || currentEnemy is Object && currentEnemy.Equals (null))
        {
            currentEnemy = enemies.FirstOrDefault ();
        }
        // t = h / (uB-uA)
        if (currentEnemy != null && currentEnemy is Object && !currentEnemy.Equals (null))
        {
            var h = currentEnemy.transform.position.x - projectileIns.transform.position.x;
            var targetVel = isStopMove ? 0 : currentEnemy.speed.GetValue ();
            var u = (projectileIns.initialVelocity + targetVel) * settings.deltaSpeed * settings.deltaMoveStep * settings.deltaProjectileMoveStep;
            var predictedTime = h / u;
            Destroy (projectileIns.gameObject, predictedTime);
            yield return new WaitForSeconds (predictedTime);
            if (currentEnemy != null && currentEnemy is Object && !currentEnemy.Equals (null))
            {
                var atkPwrVal = attackPower.GetValue () * (1 + _technologyManager.rangeDamageRate);
                currentEnemy.TakeDamage (atkPwrVal, this);
            }
        }
    }

    protected override IEnumerator AnimateAttack (TowerDisplay tower)
    {
        if (tower == null || tower is Object && tower.Equals (null)) yield break;
        if (AnimationAttackIsNotNull ())
        {
            yield return StartCoroutine (PrepareAnimateLaunch ());
        }
        var projectileIns = Instantiate<ProjectileObject> (projectileObjectPrefab, _projectilePoint.position, Quaternion.identity);
        projectileIns.direction = direction;
        // t = h / (uB-uA)
        var h = tower.transform.position.x - projectileIns.transform.position.x;
        var u = projectileIns.initialVelocity * settings.deltaSpeed * settings.deltaMoveStep * settings.deltaProjectileMoveStep;
        var predictedTime = h / u;
        Destroy (projectileIns.gameObject, predictedTime);
        yield return new WaitForSeconds (predictedTime);
        if (tower != null && tower is Object && !tower.Equals (null))
        {
            var atkPwrVal = attackPower.GetValue () * (1 + _technologyManager.rangeDamageRate);
            tower.TakeDamage (atkPwrVal, this);
        }
    }

    IEnumerator PrepareAnimateLaunch ()
    {
        if (!AnimationAttackIsNotNull ()) yield break;
        animator.Play (animationAttack.name, 0);
        var launchFn = animationAttack.events.FirstOrDefault (x => x.functionName == "Launch");
        if (launchFn != null)
        {
            yield return new WaitForSeconds (launchFn.time);
        }
    }

    Vector3 ComputeCenterPoint (Vector3[] points)
    {
        var center = Vector3.zero;
        for (var i = 0; i < points.Length; i++)
        {
            center += points[i];
        }
        return center / points.Length;
    }
}
