using UnityEngine;

public class SandWindFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void LateUpdate()
    {
        Vector3 position = target.position;

        // Keep sand centered on player
        position.y = transform.position.y;

        transform.position = position;
    }
}
