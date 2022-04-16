using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitMovementBehavior : MonoBehaviour
{
    /// <summary>
    /// The node the unit is currently on.
    /// </summary>
    [SerializeField]
    private NodeBehavior _currentNode;

    /// <summary>
    /// The node that the unit will attempt to move towards.
    /// </summary>
    [SerializeField]
    private NodeBehavior _targetNode;

    public NodeBehavior CurrentNode
    {
        get { return _currentNode; }
    }

    public NodeBehavior TargetNode
    {
        get { return _targetNode; }
    }
}
