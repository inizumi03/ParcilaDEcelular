using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderActivator : MonoBehaviour
{
    public Animator playerAnimator;
    public string leftAnimName = "SwipeLeft";
    public string rightAnimName = "SwipeRight";
    public string kickAnimName = "Kick"; // Nueva animación

    public Collider leftCollider;
    public Collider rightCollider;
    public Collider kickCollider; // Nuevo collider

    private void Update()
    {
        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(leftAnimName))
        {
            EnableLeftCollider();
        }
        else if (stateInfo.IsName(rightAnimName))
        {
            EnableRightCollider();
        }
        else if (stateInfo.IsName(kickAnimName))
        {
            EnableKickCollider();
        }
        else
        {
            DisableColliders();
        }
    }

    void EnableLeftCollider()
    {
        if (leftCollider != null) leftCollider.enabled = true;
        if (rightCollider != null) rightCollider.enabled = false;
        if (kickCollider != null) kickCollider.enabled = false;
    }

    void EnableRightCollider()
    {
        if (rightCollider != null) rightCollider.enabled = true;
        if (leftCollider != null) leftCollider.enabled = false;
        if (kickCollider != null) kickCollider.enabled = false;
    }

    void EnableKickCollider()
    {
        if (kickCollider != null) kickCollider.enabled = true;
        if (leftCollider != null) leftCollider.enabled = false;
        if (rightCollider != null) rightCollider.enabled = false;
    }

    void DisableColliders()
    {
        if (leftCollider != null) leftCollider.enabled = false;
        if (rightCollider != null) rightCollider.enabled = false;
        if (kickCollider != null) kickCollider.enabled = false;
    }
}
