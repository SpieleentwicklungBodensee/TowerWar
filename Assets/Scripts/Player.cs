using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator  animator;
    public Transform shootPoint;
    
    public event Action OnDeath; 

    public void Shoot(Vector2 direction, GameObject bullet)
    {
        bullet.transform.position = shootPoint.position;
        bullet.GetComponent<Rigidbody2D>().AddForce(direction);
        animator.SetTrigger("Shot");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            OnDeath?.Invoke();
    }
}