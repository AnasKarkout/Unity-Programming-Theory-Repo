using System.Collections;
using UnityEngine;

public enum PowerupType { None, Healing, Shielding, Strength }

public class Powerup : MonoBehaviour
{
    // [field: SerializeField] allows serializing auto-properties for read-only external access.
    [field: SerializeField] public PowerupType powerupType { get; private set; }

    private float lifespanOnGround = 7.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(GroundLifecycle());
        }
    }

    IEnumerator GroundLifecycle()
    {
        yield return new WaitForSeconds(lifespanOnGround);
        gameObject.SetActive(false);
    }
}
