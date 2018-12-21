using UnityEngine;
using UnityEngine.UI;

public class MonsterTowerDisplay : TowerDisplay
{
    public Image healthBar;

    public override void Update ()
    {
        if (healthBar != null && !healthBar.Equals (null))
        {
            healthBar.fillAmount = (float) hp / (float) maxHP;
        }
    }

    public override void Awake ()
    {
        direction = Direction.RightToLeft;
        enemies = FindObjectOfType<CharacterDisplayList> ();
        base.Awake();
    }
}
