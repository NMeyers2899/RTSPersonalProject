using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBehavior : MonoBehaviour
{
    /// <summary>
    /// The cost of moving onto the node.
    /// </summary>
    [SerializeField]
    private float _cost;

    /// <summary>
    /// Determines whether or not the node can be walked on.
    /// </summary>
    [SerializeField]
    private bool _isWalkable;

    /// <summary>
    /// The cost of moving onto the node.
    /// </summary>
    public float Cost
    {
        get { return _cost; }
    }

    /// <summary>
    /// Determines whether or not the node can be walked on.
    /// </summary>
    public bool IsWalkable
    {
        get { return _isWalkable; }
    }
}
