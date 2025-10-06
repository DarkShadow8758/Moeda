using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void AnimateByTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }
    public void CharacterLike()
    {
        AnimateByTrigger("Like");
    }
    public void CharacterDamage()
    {
        AnimateByTrigger("Damage");
    }
}
