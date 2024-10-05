using System;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public float minForce = 50;
    public float forceToBreak = 1000;

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
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        OnClick?.Invoke(this);
    }
}