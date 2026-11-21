using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUpgrade", menuName = "Upgrades/Speed")]
public class SpeedUpgrade : UpgradeData
{
    [SerializeField] private float _speedBonus = 1.5f;

    public override void Apply(PlayerUpgrades playerUpgrades)
    {
        playerUpgrades.Movement.IncreaseSpeed(_speedBonus);
    }
}
