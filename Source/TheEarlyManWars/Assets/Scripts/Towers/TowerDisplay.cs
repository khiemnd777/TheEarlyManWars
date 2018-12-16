using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerDisplay : MonoBehaviour
{
    public TowerObject towerObject;
    public int hp;
    public Stat rangeAttack;
    public Stat attackSpeed;
    [System.NonSerialized]
    public Direction direction;
    [System.NonSerialized]
    public ObjectDisplayList enemies;
    protected Settings settings;
    IEnumerable<ObjectDisplay> _detectedEnemies;
    float _attackTime = 0f;

    public virtual void Awake ()
    {
        settings = FindObjectOfType<Settings> ();
    }

    public virtual void Start ()
    {
        rangeAttack.baseValue = towerObject.rangeAttack;
        attackSpeed.baseValue = towerObject.attackSpeed;
        hp = towerObject.hp;
        StartCoroutine (ScanEnemies ());
        StartCoroutine (OnAlert ());
    }

    public void TakeDamage (int damage, ObjectDisplay damagedBy)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Debug.Log (name + " being destroyed!");
            OnDeath (damagedBy);
            Destroy (gameObject);
        }
    }

    public virtual void OnDeath (ObjectDisplay damagedBy)
    {

    }

    protected virtual IEnumerator AnimateAttack (IEnumerable<ObjectDisplay> enemies)
    {
        yield break;
    }

    public virtual IEnumerable<ObjectDisplay> DetectEnemies ()
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
                return enemies.list.Where (e => e.transform.position.x > currentX && e.transform.position.x <= rangeX);
            case Direction.RightToLeft:
                return enemies.list.Where (e => e.transform.position.x >= rangeX && e.transform.position.x < currentX);
            default:
                return new List<ObjectDisplay> ();
        }
    }

    bool PrepareAttack ()
    {
        if (settings.deltaSpeed <= 0) return false;
        if (Time.time < _attackTime) return false;
        var atkSpdVal = attackSpeed.GetValue ();
        _attackTime = Time.time + settings.deltaAttackTime / (atkSpdVal * settings.deltaSpeed);
        return true;
    }

    IEnumerator ScanEnemies ()
    {
        while (gameObject != null && !gameObject.Equals (null))
        {
            _detectedEnemies = DetectEnemies ();
            yield return null;
        }
    }

    IEnumerator OnAlert ()
    {
        while (gameObject != null && !gameObject.Equals (null))
        {
            if (_detectedEnemies.Any ())
            {
                if (PrepareAttack ())
                {
                    yield return StartCoroutine (AnimateAttack (_detectedEnemies));
                }
            }
            yield return null;
        }
    }
}
