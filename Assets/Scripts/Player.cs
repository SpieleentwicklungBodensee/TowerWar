using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    
    public event Action OnDeath; 

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            Shoot();
    }

    void Shoot()
    {
        animator.SetTrigger("Shot");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            OnDeath?.Invoke();
    }
}