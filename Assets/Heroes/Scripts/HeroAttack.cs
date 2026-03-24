using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    private HeroController _heroController;
    private HeroMovement _heroMovement;

    RaycastHit hit;

    private void Start()
    {
        _heroController = GetComponent<HeroController>();
        _heroMovement = GetComponent<HeroMovement>();
    }

    private void Update()
    { 
        if (InputReader.Instance.MouseRightClick)
        {
            if (Physics.Raycast(_heroController.GetRayOrigin(), _heroController.GetRayDirection(), out hit, 100f))
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                { 
                    Debug.Log("Enemy was hited");

                    _heroController.IsAttackingEnemy = true;
                    _heroController.ChangeAgentRotationManualy(_heroController.IsAttackingEnemy);
                }
            }
        }

        if (_heroController.IsAttackingEnemy)
        {
            _heroMovement.RotateCharacterTowardEnemy(hit.collider.gameObject.transform.position);
        }
    }
}
