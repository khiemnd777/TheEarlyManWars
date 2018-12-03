using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeCharacterDisplay : CharacterDisplay
{
    [SerializeField]
    ProjectileObject _projectileObjectPrefab; 

    protected override IEnumerator AnimateAttack (IEnumerable<ObjectDisplay> enemies)
    {
        if (!enemies.Any ()) yield break;
        var projectileIns = Instantiate<ProjectileObject> (_projectileObjectPrefab, transform.position, Quaternion.identity);
        projectileIns.direction = direction;

        var points = enemies.Select (x => x.transform.position).ToArray ();
        if (currentEnemy == null || currentEnemy is Object && currentEnemy.Equals (null))
        {
            currentEnemy = enemies.ElementAt (Random.Range (0, enemies.Count ()));
        }
        // t = h / (uB-uA)
        var h = currentEnemy.transform.position.x - projectileIns.transform.position.x;
        var u = (projectileIns.initialVelocity + currentEnemy.speed.GetValue ()) * settings.deltaSpeed * settings.deltaMoveStep;
        var predictedTime = h / u;
        Destroy(projectileIns.gameObject, predictedTime);
        yield return new WaitForSeconds (predictedTime);
        if(currentEnemy is Object && !currentEnemy.Equals(null))
            currentEnemy.TakeDamage (attackPower.GetValue (), this);
    }

    protected override IEnumerator AnimateAttack (Tower tower)
    {
        if (tower == null || tower is Object && tower.Equals (null)) yield break;
        var projectileIns = Instantiate<ProjectileObject> (_projectileObjectPrefab, transform.position, Quaternion.identity);
        projectileIns.direction = direction;
        // t = h / (uB-uA)
        var h = tower.transform.position.x - projectileIns.transform.position.x;
        var u = projectileIns.initialVelocity * settings.deltaSpeed * settings.deltaMoveStep;
        var predictedTime = h / u;
        yield return new WaitForSeconds (predictedTime);
        tower.TakeDamage (attackPower.GetValue ());
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
