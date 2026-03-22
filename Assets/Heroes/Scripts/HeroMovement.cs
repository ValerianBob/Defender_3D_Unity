using UnityEngine;
using UnityEngine.Timeline;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] private GameObject Marker;

    private HeroController _hr;

    private void Start()
    {
        _hr = GetComponent<HeroController>();
    }

    private void Update()
    {
        if (InputReader.Instance.MouseRightClick)
        { 
            if (Physics.Raycast(_hr.GetRayOrigin(), _hr.GetRayDestination(), out RaycastHit hit, 100f))
            {
                Instantiate(Marker, hit.point, Marker.transform.rotation);

                _hr.SetAgentDestination(hit.point);
            }
        }
    }
}
