using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public event Action OnRemoveFromTower;
    public float        minForce     = 50;
    public float        forceToBreak = 1000;
    public GameObject   explosionPrefab;

    private float _damagePoints;

    private void Start()
    {
        _damagePoints = forceToBreak;
    }

    [NonSerialized] public Action<Brick> OnClick;

    private void OnCollisionEnter2D(Collision2D col)
    {
        var force = col.relativeVelocity.magnitude * col.rigidbody.mass;

        if (force < minForce)
            return;

        _damagePoints -= force;

        if (_damagePoints < 0)
        {
            if (explosionPrefab)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
            
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        OnClick?.Invoke(this);
    }

    void Update()
    {
        if (transform.position.y < -14)
        {
            OnRemoveFromTower?.Invoke();
            OnRemoveFromTower = null;
        }
    }
}