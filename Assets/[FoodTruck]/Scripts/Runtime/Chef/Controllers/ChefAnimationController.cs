using UnityEngine;

public class ChefAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _chefAnimator;

    public string IDLE_ANIM = "Idle";
    public string RUNNING_ANIM = "Run";
    public string ORDER_ANIM = "Order";
    public string CARRYING_ANIM = "Carrying";
    public string IDLECARRYING_ANIM = "IdleCarry";

    public void ChangeAnimation(string animationName)
    {
        _chefAnimator.CrossFade(animationName, 0.1f);
    }
}
