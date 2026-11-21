using UnityEngine;

[CreateAssetMenu(fileName = "FireRateUpgrade", menuName = "Upgrades/FireRate")]
public class FireRateUpgrade : UpgradeData
{
    [SerializeField, Range(0.5f, 0.95f)] private float _fireRateMultiplier = 0.85f;

    public override void Apply(PlayerUpgrades playerUpgrades)
    {
        playerUpgrades.Shooting.IncreaseFireRate(_fireRateMultiplier);
    }
}
