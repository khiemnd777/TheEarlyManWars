using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Button))]
public class AddCharacterButton : MonoBehaviour
{
    public BaseCharacter baseCharacter;
    int _meat;
    float _cooldownPurchase;
    Settings _settings;
    MeatSystem _meatSystem;
    CharacterSpawner _spawner;
    Button _button;
    [SerializeField]
    Text _nameText;
    [SerializeField]
    Text _meatText;
    bool _inCooldownProgress;
    float _cooldownCounter;

    void Awake ()
    {
        _button = GetComponent<Button> ();
    }

    void Start ()
    {
        _spawner = FindObjectOfType<CharacterSpawner> ();
        _meatSystem = FindObjectOfType<MeatSystem> ();
        _settings = FindObjectOfType<Settings> ();
        _meat = baseCharacter.meat;
        _cooldownPurchase = baseCharacter.cooldownPurchase;
        _nameText.text = baseCharacter.name;
        _meatText.text = _meat.ToString ();
        _button.onClick.AddListener (() => OnClick ());
    }

    void Update ()
    {
        CalculateCooldownProgress();
    }

    void OnClick ()
    {
        if (_inCooldownProgress) return;
        _meatSystem.Purchase (_meat, () =>
        {
            _spawner.Spawn (baseCharacter);
            _inCooldownProgress = true;
        });
    }

    void CalculateCooldownProgress ()
    {
        if (_settings.deltaSpeed <= 0) return;
        if (!_inCooldownProgress) return;
        _button.interactable = false;
        if (_cooldownCounter < 1f)
        {
            _cooldownCounter += Time.deltaTime / _cooldownPurchase * _settings.deltaSpeed;
            return;
        }
        _inCooldownProgress = false;
        _button.interactable = true;
        _cooldownCounter = 0f;
    }
}
