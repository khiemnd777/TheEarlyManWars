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
        var commonRange = 1;
        switch (rawValue)
        {
            default:
                case RangeAttackEnum.Melee:
                return 2 + commonRange;
            case RangeAttackEnum.MediumRange:
                    return 5 + commonRange;
            case RangeAttackEnum.LongRange:
                    return 8 + commonRange;
        }
    }
}
