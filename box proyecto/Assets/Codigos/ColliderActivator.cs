using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderActivator : MonoBehaviour
{
    public Animator playerAnimator;
    public string leftAnimName = "SwipeLeft";
    public string rightAnimName = "SwipeRight";

    public Collider leftCollider;
    public Collider rightCollider;

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
        else
        {
            DisableColliders();
        }
    }

    void EnableLeftCollider()
    {
        if (leftCollider != null) leftCollider.enabled = true;
        if (rightCollider != null) rightCollider.enabled = false;
    }

    void EnableRightCollider()
    {
        if (rightCollider != null) rightCollider.enabled = true;
        if (leftCollider != null) leftCollider.enabled = false;
    }

    void DisableColliders()
    {
        if (leftCollider != null) leftCollider.enabled = false;
        if (rightCollider != null) rightCollider.enabled = false;
    }
}
