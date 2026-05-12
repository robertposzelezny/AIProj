using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportOnCollision : MonoBehaviour
{
    public GameObject teleportDestination;

    [Tooltip("When teleportDestination is null, touching Exit loads the next scene in Build Settings. After the last level, returns to the main menu (build index 0).")]
    public bool loadNextSceneIfNoDestination = true;

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
        if (teleportDestination != null)
        {
            if (characterController != null)
                characterController.enabled = false;

            transform.position = teleportDestination.transform.position;
            transform.rotation = teleportDestination.transform.rotation;

            if (characterController != null)
                characterController.enabled = true;
            return;
        }

        if (!loadNextSceneIfNoDestination)
            return;

        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next >= SceneManager.sceneCountInBuildSettings)
            next = 0;
        SceneManager.LoadScene(next);
    }
}
