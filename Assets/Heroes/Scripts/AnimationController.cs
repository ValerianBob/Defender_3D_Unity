using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private HeroController _hr;

    private void Start()
    {
        _hr = GetComponent<HeroController>();
    }

    private void Update()
    {
        if (_hr.GetAgentMagnitude() > 0)
        {
            _hr.SetBool("IsWalking", true);
        }
        else
        {
            _hr.SetBool("IsWalking", false);
        }
    }
}