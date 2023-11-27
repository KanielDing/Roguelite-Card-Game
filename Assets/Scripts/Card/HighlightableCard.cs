using UnityEngine;

public class HighlightableCard : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnMouseEnter()
    {
        if (animator != null) animator.SetTrigger("onHoverEnter");
    }

    private void OnMouseExit()
    {
        if (animator != null) animator.SetTrigger("onHoverExit");
    }
}
