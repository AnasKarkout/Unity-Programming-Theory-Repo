using UnityEngine;

public class PowerupBehaviour : MonoBehaviour
{
    // A PowerupBehavior is an attribute applied to the Player when the Powerup Object is collected
    // Different Powerups will have different behaviors applied to the player via AddComponent<PowerupB_/powerup/]>()

    public PowerupType PowerupType { get; private set; }

    void Awake()
    {
        // TODO set the PowerupType through each child class then call this Awake as base.Awake();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
