public enum RangeAttackEnum
{
    Melee = 1,
    MediumRange,
    LongRange
}
public static class RangeAttackEnumExtensions
{
    public static int GetValue (this RangeAttackEnum rawValue)
    {
        switch (rawValue)
        {
            default:
                case RangeAttackEnum.Melee:
                return 2;
            case RangeAttackEnum.MediumRange:
                    return 5;
            case RangeAttackEnum.LongRange:
                    return 8;
        }
    }
}
