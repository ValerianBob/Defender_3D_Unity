using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    private HeroController _heroController;

    [SerializeField] private GameObject Marker;

    //public LayerMask Layer_Mask;

    private void Start()
    {
        _heroController = GetComponent<HeroController>();
    }

    private void Update()
    {
        //if (InputReader.Instance.MouseRightClick)
        //{ 
        //    if (Physics.Raycast(_heroController.GetRayOrigin(), _heroController.GetRayDirection(), out RaycastHit hit, 100f))
        //    {
        //        if (hit.collider.gameObject.CompareTag("Environment"))
        //        {
        //            Debug.Log("Can't go in Environment");
                    
        //            return;
        //        }
        //        else if (hit.collider.gameObject.CompareTag("Enemy"))
        //        {
        //            Debug.Log("Trying to attack enemy");

        //            _heroController.SetAgentDestination(hit.point);

        //            return;
        //        }

        //        Instantiate(Marker, hit.point, Marker.transform.rotation);

        //        _heroController.IsAttackingEnemy = false;
        //        _heroController.ChangeAgentRotationManualy(_heroController.IsAttackingEnemy);

        //        _heroController.SetAgentDestination(hit.point);
        //    }
        //}
    }

    public void MoveToPosition(Vector3 position)
    {
        _heroController.SetAgentDestination(position);

        Instantiate(Marker, position, Marker.transform.rotation);
    }

    public void RotateCharacterTowardEnemy(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation, 999f * Time.deltaTime);
        }
    }
}
