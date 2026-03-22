using UnityEngine;

public class ClearObject : MonoBehaviour
{
    public float DelayToDestroy;

    private void Start()
    {
        Invoke("DestroyObject", DelayToDestroy);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
