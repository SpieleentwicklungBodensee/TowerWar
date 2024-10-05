using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            Destroy(gameObject);
    }
}