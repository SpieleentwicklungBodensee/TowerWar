using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public event Action OnRemoveFromTower;
    public float        minForce     = 50;
    public float        forceToBreak = 1000;
    public GameObject   explosionPrefab;
    public Action<Transform> stopTrackingStone;

    private float _damagePoints;

    private void Start()
    {
        _damagePoints = forceToBreak;
    }

    [NonSerialized] public Action<Brick> OnClick;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (stopTrackingStone != null)
        {
            //Debug.Log("Exit3");
            stopTrackingStone(transform);
            stopTrackingStone = null;
        }
        
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
            
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySound("hit2");

            Destroy(gameObject);
        }
        else
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySound("hit1", Math.Min(force / forceToBreak + 0.3f, 1.0f));
        }
    }

    private void OnMouseDown()
    {
        OnClick?.Invoke(this);
    }

    void Update()
    {
        if (stopTrackingStone != null && transform.position is { x: > 20 } or { x: < -32 })
        {
            //Debug.Log("Exit1");
            stopTrackingStone(transform);
            stopTrackingStone = null;
        }
        
        if (transform.position.y < -14)
        {
            if (stopTrackingStone != null)
            {
                //Debug.Log("Exit2");
                stopTrackingStone(transform);
                stopTrackingStone = null;
            }
            
            OnRemoveFromTower?.Invoke();
            OnRemoveFromTower = null;
        }
    }
}