using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator  animator;
    public Transform shootPoint;
    public GameObject tower;
    private Brick selectedBrick;
    private GameObject shotBullets;
    private List<ReactivateCollision> _reactivateCollisions = new();
    public float waterLevel = -15.25f;

    public event Action OnDeath;

    public void Start()
    {
        shotBullets = new GameObject("shotBullets");
    }

    private void Update()
    {
        var removeList = new List<ReactivateCollision>();

        foreach (var reactivateCollision in _reactivateCollisions)
        {
            reactivateCollision.remainingTime -= Time.deltaTime;
            if (reactivateCollision.remainingTime <= 0)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), reactivateCollision.collider, false);
                removeList.Add(reactivateCollision);
            }
        }

        foreach (var remove in removeList)
        {
            _reactivateCollisions.Remove(remove);
        }
    }

    public void Activate(bool activate)
    {
        if (activate)
        {
            float maxY = -Mathf.Infinity;
            foreach (Brick block in tower.GetComponentsInChildren<Brick>())
            {
                var y = block.transform.position.y;
                if (y > maxY)
                {
                    maxY          = y;
                    selectedBrick = block;
                }
            }

            selectedBrick.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    public void ChangeBlockSelection(Vector2 direction)
    {
        var size = selectedBrick.GetComponent<Collider2D>().bounds.size;
        var position = selectedBrick.transform.position;
        position.x += size.x / 2 * direction.x;
        position.y += size.y / 2 * direction.y;

        float closest = Mathf.Infinity;
        Brick newBrick = null;
        Vector3 direction3 = new Vector3(direction.x, direction.y, 0);
        Vector3 directionRotated3 = new Vector3(-1 * direction.y, direction.x, 0);

        foreach(Brick block in tower.GetComponentsInChildren<Brick>())
        {
            if(GameObject.ReferenceEquals(block.gameObject, selectedBrick.gameObject))
                continue;


            var dist = block.transform.position - position;
            var distDir1 = Vector3.Dot(dist, direction3);
            if(distDir1 < 0)
                continue;

            var distDir2 = Vector3.Dot(dist, directionRotated3);
            var fullDist = new Vector2(distDir1, distDir2).sqrMagnitude;
            if(fullDist < closest)
            {
                closest = fullDist;
                newBrick = block;
            }
        }

        if(newBrick != null)
        {
            if(selectedBrick != null)
                selectedBrick.GetComponent<SpriteRenderer>().color = Color.white;

            selectedBrick = newBrick;
            if(selectedBrick != null)
                selectedBrick.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }


    public void Shoot(Vector2 direction)
    {
        if(selectedBrick == null)
            return;

        var bullet = selectedBrick.gameObject;
        bullet.GetComponent<SpriteRenderer>().color = Color.white;
        bullet.transform.position                   = shootPoint.position;
        bullet.transform.parent                     = shotBullets.transform;
        bullet.GetComponent<Rigidbody2D>().AddForce(direction);
        animator.SetTrigger("Shot");
        selectedBrick = null;

        var joints = bullet.GetComponent<JointRef>();
        if(joints != null)
            foreach(var joint in joints.Joints)
                if(joint != null)
                    Destroy(joint);

        var bulletCol = bullet.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), bulletCol);
        _reactivateCollisions.Add(new ReactivateCollision
        {
            collider      = bulletCol,
            remainingTime = 1f
        });
    }

    private void Update()
    {
        if (transform.position is { x: > 50 or < -50 } or { y: < -50 } || transform.position.y < waterLevel)
            OnDeath?.Invoke();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            OnDeath?.Invoke();
    }

    private class ReactivateCollision
    {
        public Collider2D collider;
        public float      remainingTime = 1;
    }
}