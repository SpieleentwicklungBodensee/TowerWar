using System;
using System.Collections.Generic;
using UnityEngine;

public class JointRef : MonoBehaviour
{
    [NonSerialized] public readonly List<Joint2D> Joints = new();
}
