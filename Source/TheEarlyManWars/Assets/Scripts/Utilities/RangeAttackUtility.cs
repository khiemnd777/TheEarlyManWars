public class RangeAttackUtility
{
    public static int GetValue (RangeAttackEnum rawValue)
    {
        switch (rawValue)
        {
            default:
            case RangeAttackEnum.Melee:
                return 2;
            case RangeAttackEnum.MediumRange:
                return 6;
            case RangeAttackEnum.LongRange:
                return 8;
        }
    }
}