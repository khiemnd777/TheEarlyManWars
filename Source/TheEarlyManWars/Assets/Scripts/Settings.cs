using UnityEngine;

public class Settings : MonoBehaviour
{
    public float deltaMoveStep = .5f;
    public float deltaProjectileMoveStep = 2f;
    // Delta attack time should be one
    public float deltaAttackTime = 1f;
    public bool debug = false;
    public float objectShakeDuration = .2f;
    public float objectShakeMagnitude = .08f;
    [Header("Screen settings")]
    public ScreenOrientation screenOrientation = ScreenOrientation.Landscape;
    [System.NonSerialized]
    public float defaultScreenSize = 7.44f;
    public float screenSizeMin = 5f;
    public float screenSizeMax = 7.44f;

    void Awake ()
    {
        Screen.orientation = screenOrientation;
    }
}
