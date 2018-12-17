public enum SpeedEnum
{
    Default = 1,
    Slow,
    Medium,
    Fast
}

public static class SpeedEnumExtensions
{
    public static int GetValue (this SpeedEnum s)
    {
        switch(s){
            default:
            case SpeedEnum.Slow:
                return 2;
            case SpeedEnum.Medium:
                return 5;
            case SpeedEnum.Fast:
                return 8;
        }
    }
}
