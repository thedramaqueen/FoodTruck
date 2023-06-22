using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _customerAnimator;

    public string IDLE_ANIM = "Idle";
    public string RUNNING_ANIM = "Run";
    public string ORDER_ANIM = "Order";
    public string CARRYING_ANIM = "Carrying";

    public void ChangeAnimation(string animationName)
    {
        _customerAnimator.CrossFade(animationName, 0.1f);
    }
}
