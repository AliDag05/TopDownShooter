using UnityEngine;

[CreateAssetMenu(fileName = "DamageUpgrade", menuName = "Upgrades/Damage")]
public class DamageUpgrade : UpgradeData
{
    [SerializeField] private int _damageBonus = 1;

    public override void Apply(PlayerUpgrades playerUpgrades)
    {
        playerUpgrades.Shooting.IncreaseDamage(_damageBonus);
    }
}
