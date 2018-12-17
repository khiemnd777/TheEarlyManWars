public class AttackSpeedUtility
{
    public static float GetHitValuePerSecond (AttackSpeedEnum rawValue)
    {
        switch (rawValue)
        {
            case AttackSpeedEnum.One:
                return 0.5f;
            case AttackSpeedEnum.Two:
                return 1f;
            case AttackSpeedEnum.Three:
                return 1.5f;
            case AttackSpeedEnum.Four:
                return 2f;
            default:
                return 0;
        }
    }
}
