using UnityEngine;

public class TrigerAnimationEvent : MonoBehaviour
{
    public HeroAttack heroAttack;

    public void DealDamageEvent()
    {
        heroAttack.DealDamage();
    }
}
