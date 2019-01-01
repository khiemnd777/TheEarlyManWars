using UnityEngine;
using UnityEngine.UI;

public class MonsterTowerDisplay : TowerDisplay
{
    public Image healthBar;
    PlayUI _playUI;

    public override void Start ()
    {
        _playUI = FindObjectOfType<PlayUI> ();
        base.Start ();
    }

    public override void Update ()
    {
        if (healthBar != null && !healthBar.Equals (null))
        {
            healthBar.fillAmount = (float) hp / (float) maxHP;
        }
        base.Update ();
    }

    public override void Awake ()
    {
        direction = Direction.RightToLeft;
        enemies = FindObjectOfType<CharacterDisplayList> ();
        base.Awake ();
    }

    public override void OnDeath (ObjectDisplay damagedBy)
    {
        _playUI.Win ();
    }
}
