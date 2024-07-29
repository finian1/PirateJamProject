using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject mainPlayer;
    [SerializeField] private float cameraFollowSpeed;
    [SerializeField] private Vector3 cameraOffset;

    private void LateUpdate()
    {
        if (mainPlayer != null)
        {
            Vector3 playerTarget = mainPlayer.transform.position + cameraOffset;
            transform.position = Vector3.Lerp(transform.position, playerTarget, cameraFollowSpeed * Time.deltaTime);
        }
    }
}
