using System.Linq;
using UnityEngine;

public abstract class ObjectDisplay : MonoBehaviour
{
    public BaseObject baseObject;
    public ObjectDisplayList allies;
    public ObjectDisplayList enemies;
    public Settings settings;
    public Stat speed;
    public Stat attackSpeed;
    public Stat attackPower;
    public Stat rangeAttack;
    public Stat hp;
    public int maxHP;
    public Direction direction;

    public virtual void Awake ()
    {
        settings = FindObjectOfType<Settings> ();
        name = baseObject.name;
        speed.baseValue = baseObject.speed;
        attackSpeed.baseValue = baseObject.attackSpeed;
        attackPower.baseValue = baseObject.attackPower;
        rangeAttack.baseValue = baseObject.rangeAttack;
        maxHP = hp.baseValue = baseObject.hp;
    }

    public virtual void Update ()
    {
        Move ();
    }

    public virtual void FixedUpdate ()
    {

    }

    public virtual void Move ()
    {
        var detected = DetectEnemy ();
        if (detected != null)
        {
            return;
        }
        var spdVal = speed.GetValue ();
        transform.position += Vector3.right * (int) direction * spdVal * settings.deltaSpeed * Time.fixedDeltaTime;
    }

    public virtual ObjectDisplay DetectEnemy ()
    {
        var atkRangeVal = rangeAttack.GetValue ();
        Debug.DrawRay (transform.position, Vector3.right * (int) direction * atkRangeVal, Color.yellow);
        var currentX = transform.position.x;
        var rangeX = currentX + atkRangeVal * (int) direction;
        ObjectDisplay detectedObj;
        switch (direction)
        {
            default:
            case Direction.LeftToRight:
            {
                detectedObj =  enemies.list.FirstOrDefault (e => e.transform.position.x > currentX && e.transform.position.x <= rangeX);
                break;
            }
            case Direction.RightToLeft:
            {
                detectedObj =  enemies.list.FirstOrDefault (e => e.transform.position.x >= rangeX && e.transform.position.x < currentX);
                break;
            }
        }
        return detectedObj;
    }
}
