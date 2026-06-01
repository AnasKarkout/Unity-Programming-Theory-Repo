using UnityEngine;

public enum PowerupType { None, Healing, Shielding, Strength }

public class Powerup : MonoBehaviour
{
    // [field: SerializeField] allows serializing auto-properties for read-only external access.
    [field: SerializeField] public PowerupType powerupType { get; private set; } 

    // TODO: powerup object lifecycle handling
}
