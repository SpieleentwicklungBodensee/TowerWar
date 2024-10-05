using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator  animator;
    public Transform shootPoint;
    public GameObject tower;
    private Brick selectedBrick;
    private GameObject shotBullets;

    public event Action OnDeath;

    public void Start()
    {
        shotBullets = new GameObject("shotBullets");
    }

    public void Activate(bool activate)
    {
        if(activate)
        {
            float maxY = -Mathf.Infinity;
            foreach(Brick block in tower.GetComponentsInChildren<Brick>())
            {
                var y = block.transform.position.y;
                if(y > maxY)
                {
                    maxY = y;
                    selectedBrick = block;
                }
            }

            selectedBrick.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    public void ChangeBlockSelection(Vector2 direction)
    {

    }


    public void Shoot(Vector2 direction)
    {
        var bullet = selectedBrick.gameObject;
        bullet.GetComponent<SpriteRenderer>().color = Color.white;
        bullet.transform.position = shootPoint.position;
        bullet.transform.parent = shotBullets.transform;
        bullet.GetComponent<Rigidbody2D>().AddForce(direction);
        animator.SetTrigger("Shot");
        selectedBrick = null;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            OnDeath?.Invoke();
    }
}