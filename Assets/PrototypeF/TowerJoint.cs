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

        private void Start()
        {
            foreach (var join in joins)
            {
                var joint = transform.AddComponent<FixedJoint2D>();
                joint.breakForce = breakingForce;
                joint.breakTorque = breakingTorque;
                joint.connectedBody = join.connectedBody;
                joint.breakAction = JointBreakAction2D.Destroy;
                joint.anchor = join.anchorLocalSpace;
                joint.autoConfigureConnectedAnchor = true;
            }
        }

        [Serializable]
        public class JoinDefinition
        {
            public Rigidbody2D connectedBody;
            public Vector2 anchorLocalSpace;
        }
    }
}
