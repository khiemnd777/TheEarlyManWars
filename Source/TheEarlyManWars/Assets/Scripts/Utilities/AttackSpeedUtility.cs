public class AttackSpeedUtility
{
    public static float GetHitValuePerSecond (int rawValue)
    {
        switch (rawValue)
        {
            case 1:
                return 0.5f;
            case 2:
                return 1f;
            case 3:
                return 1.5f;
            case 4:
                return 2f;
            default:
                return 0;
        }
    }
}
