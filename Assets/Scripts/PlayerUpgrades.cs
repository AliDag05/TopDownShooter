using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerShooting _playerShooting;
    
    public PlayerMovement Movement => _playerMovement;
    public PlayerShooting Shooting => _playerShooting;
}
