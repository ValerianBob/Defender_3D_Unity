using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private HeroController _heroController;

    private void Start()
    {
        _heroController = GetComponent<HeroController>();
    }

    private void Update()
    {
        if (_heroController.GetAgentMagnitude() > 0)
        {
            _heroController.SetBool("IsWalking", true);
        }
        else
        {
            _heroController.SetBool("IsWalking", false);
        }
    }
}