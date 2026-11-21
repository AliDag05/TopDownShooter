using UnityEngine;

public abstract class UpgradeData : ScriptableObject
{
    [SerializeField] private string _title;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _maxLevel;

    public string Title => _title;
    public string Description => _description;
    public Sprite Icon => _icon;
    public int MaxLevel => _maxLevel;

    public abstract void Apply(PlayerUpgrades playerUpgrades);
}
