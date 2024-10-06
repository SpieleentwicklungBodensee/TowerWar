using UnityEngine;

public class StickyUI : MonoBehaviour
{
    public Transform anchor;

    private Vector3 _anchorOffset;

    private void Start()
    {
        _anchorOffset = transform.position - anchor.position;
    }
    
    private void Update()
    {
        transform.position = anchor.position + _anchorOffset;
    }
}
