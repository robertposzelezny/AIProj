using UnityEngine;

public class TeleportOnCollision : MonoBehaviour
{
    public GameObject teleportDestination;

    private CharacterController characterController;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    /// <summary>Solid colliders (non-trigger) the CharacterController walks into.</summary>
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider != null && hit.collider.CompareTag("Exit"))
            TryTeleport();
    }

    /// <summary>Trigger volumes — OnControllerColliderHit does not fire for triggers.</summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit"))
            TryTeleport();
    }

    void TryTeleport()
    {
        if (teleportDestination == null)
            return;

        if (characterController != null)
            characterController.enabled = false;

        transform.position = teleportDestination.transform.position;
        transform.rotation = teleportDestination.transform.rotation;

        if (characterController != null)
            characterController.enabled = true;
    }
}
