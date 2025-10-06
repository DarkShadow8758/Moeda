using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Animator animator;
    
    public void Animate(string trigger)
    {
        animator.SetTrigger(trigger);
    }
}
