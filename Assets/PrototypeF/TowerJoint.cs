using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PrototypeF
{
    public class TowerJoint : MonoBehaviour
    {
        public List<JoinDefinition> joins;

        public float breakingForce;
        public float breakingTorque;
        
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            foreach (var join in joins)
            {
                var joint = transform.AddComponent<FixedJoint2D>();
                joint.breakForce = breakingForce;
                joint.breakTorque = breakingTorque;
                joint.connectedBody = join.connectedBody;
                joint.breakAction = JointBreakAction2D.Destroy;
                joint.anchor = LocalToRbSpace(join.anchorLocalSpace);
                joint.autoConfigureConnectedAnchor = true;
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

        [Serializable]
        public class JoinDefinition
        {
            public Rigidbody2D connectedBody;
            public Vector2 anchorLocalSpace;
        }
    }
}
