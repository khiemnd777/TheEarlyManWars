public enum LaneEnum
{
    One,
    Two,
    Three,
    Four,
    Five
}
public static class LaneEnumExtensions
{
    // -----    .Five.    -------------------
    // -----    .Four.    -------------------
    // -----    .Three.   -------------------
    // -----    .Two.     -------------------
    // -----    .One.     -------------------
    public static float GetPosition (this LaneEnum rawValue)
    {
        switch (rawValue)
        {
            case LaneEnum.One:
                return -2.5f;
            case LaneEnum.Two:
                return -1.5f;
            case LaneEnum.Three:
                return 0;
            case LaneEnum.Four:
                return 1.5f;
            case LaneEnum.Five:
                return 2.5f;
            default:
                return -1.5f;
        }
    }
}
