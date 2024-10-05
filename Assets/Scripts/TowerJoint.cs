using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerJoint : MonoBehaviour
{
    public List<JoinDefinition> joins = new();

    public float breakingForce;
    public float breakingTorque;
        
    private Rigidbody2D _rb;
    private JointRef _ref;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ref = GetComponent<JointRef>();

        if (!_ref)
            _ref = transform.AddComponent<JointRef>();
    }

    private void Start()
    {
        foreach (var join in joins)
        {
            if (!join.connectedBody && !join.fixAnchor)
                continue;
            
            var joint = transform.AddComponent<FixedJoint2D>();
                
            joint.breakForce = breakingForce;
            joint.breakTorque = breakingTorque;
            joint.connectedBody = join.connectedBody;
            joint.breakAction = JointBreakAction2D.Destroy;
            joint.anchor = LocalToRbSpace(join.anchorLocalSpace);
            joint.autoConfigureConnectedAnchor = true;
                
            var jointRef = join.connectedBody.GetComponent<JointRef>();

            if (!jointRef)
                jointRef = join.connectedBody.transform.AddComponent<JointRef>();
                
            jointRef.Joints.Add(joint);
            _ref.Joints.Add(joint);
        }
    }

    private void OnDrawGizmos()
    {
        //if (!_rb)
        //    return;
            
        foreach (var jd in joins)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.TransformPoint(jd.anchorLocalSpace), 0.1f);
            //Gizmos.DrawSphere(_rb.GetRelativePoint(LocalToRbSpace(jd.anchorLocalSpace)), 0.1f);
        }
    }

    private Vector2 LocalToRbSpace(Vector2 localPosition)
    {
        return new Vector2(localPosition.x * transform.localScale.x, localPosition.y * transform.localScale.y);
    }

    private void OnJointBreak2D(Joint2D brokenJoint)
    {
    }

    [Serializable]
    public class JoinDefinition
    {
        public Rigidbody2D connectedBody;
        public Vector2 anchorLocalSpace;
        
        /// <summary>
        /// Connects the anchor to a fixed point in space instead of a rigidbody
        /// </summary>
        public bool fixAnchor;
    }
}