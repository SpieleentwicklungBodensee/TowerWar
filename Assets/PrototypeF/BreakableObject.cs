using System;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public float minForce = 50;
    public float forceToBreak = 1000;

    private float _damagePoints;
    
    private void Start()
    {
        _damagePoints = forceToBreak;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var force = other.relativeVelocity.magnitude * 10;

        if (force < minForce)
            return;
        
        _damagePoints -= force;

        if (_damagePoints < 0)
        {
            Destroy(gameObject);
        }
    }
}
