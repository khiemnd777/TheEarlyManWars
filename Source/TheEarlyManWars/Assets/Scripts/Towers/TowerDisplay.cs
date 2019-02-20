using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerDisplay : Display
{
    public TowerObject towerObject;
    public float hp;
    public float maxHP;
    public Stat rangeAttack;
    public float attackSpeed;
    [System.NonSerialized]
    public Direction direction;
    [System.NonSerialized]
    public ObjectDisplayList enemies;
    protected Settings settings;
    IEnumerable<ObjectDisplay> _detectedEnemies;
    float _attackTime = 0f;
    [System.NonSerialized]
    public bool hasDestroyed;

    public virtual void Awake ()
    {
        settings = FindObjectOfType<Settings> ();
    }

    public virtual void Start ()
    {
        rangeAttack.baseValue = towerObject.rangeAttack;
        attackSpeed = towerObject.attackSpeed;
        maxHP = hp = towerObject.hp;
        // StartCoroutine (ScanEnemies ());
        StartCoroutine (OnAlert ());
    }

    public virtual void Update ()
    {

    }

    public void TakeDamage (float damage, ObjectDisplay damagedBy)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Debug.Log (name + " being destroyed!");
            hasDestroyed = true;
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

    IEnumerator PrepareAttack ()
    {
        if (settings.deltaSpeed <= 0) yield break;
        if (Time.time < _attackTime) yield break;
        var atkSpdVal = attackSpeed;
        while (_attackTime <= 1f)
        {
            _attackTime += Time.deltaTime * (atkSpdVal * settings.deltaAttackTime) * settings.deltaSpeed;
            yield return null;
        }
        _attackTime = 0f;
    }

    // IEnumerator ScanEnemies ()
    // {
    //     while (gameObject != null && !gameObject.Equals (null))
    //     {
    //         _detectedEnemies = DetectEnemies ();
    //         yield return null;
    //     }
    // }

    IEnumerator OnAlert ()
    {
        while (gameObject != null && !gameObject.Equals (null))
        {
            _detectedEnemies = DetectEnemies ();
            if (_detectedEnemies.Any ())
            {
                yield return StartCoroutine (AnimateAttack (_detectedEnemies));
                yield return StartCoroutine (PrepareAttack ());
            }
            yield return null;
        }
    }
}
