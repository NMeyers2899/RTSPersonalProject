using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitMovementBehavior : MonoBehaviour
{
    /// <summary>
    /// Will determine whether or not the player has this unit selected.
    /// </summary>
    private bool _isSelected = false;

    [SerializeField]
    private Transform _selectionIcon;

    /// <summary>
    /// Will determine whether or not the player has this unit selected.
    /// </summary>
    public bool IsSelected
    {
        get { return _isSelected; }
        set { _isSelected = value; }
    }

    private void OnMouseDown()
    {
        // Sets _isSelected to true and calls the OnSelection function.
        _isSelected = true;
        OnSelection();
    }

    private void OnSelection()
    {
        _selectionIcon.gameObject.SetActive(true);
    }
}
