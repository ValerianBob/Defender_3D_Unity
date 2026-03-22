using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Animator Animator;

    private NavMeshAgent _agent;

    private Ray Ray;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Ray = MainCamera.ScreenPointToRay(InputReader.Instance.MousePosition);
    }
    
    public float GetAgentMagnitude()
    {
        return _agent.velocity.magnitude;
    }

    public void SetAgentDestination(Vector3 point)
    {
        _agent.SetDestination(point);
    }

    public void Play(string animationName)
    {
        Animator.Play(animationName);
    }

    public void SetBool(string paramName, bool value)
    {
        Animator.SetBool(paramName, value);
    }

    public void SetFloat(string paramName, float value)
    {
        Animator.SetFloat(paramName, value);
    }

    public void SetTrigger(string paramName)
    {
        Animator.SetTrigger(paramName);
    }

    public Vector3 GetRayOrigin()
    {
        return Ray.origin;
    }

    public Vector3 GetRayDestination()
    {
        return Ray.direction;
    }
}
