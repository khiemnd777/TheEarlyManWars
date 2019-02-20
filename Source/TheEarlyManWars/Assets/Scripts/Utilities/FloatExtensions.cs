using UnityEngine;

public static class FloatExtensions
{
    public static float ToCeil (this float value)
    {
        return Mathf.Ceil (value);
    }

    public static float ToRoundToInt (this float value)
    {
        return Mathf.RoundToInt (value);
    }
}
