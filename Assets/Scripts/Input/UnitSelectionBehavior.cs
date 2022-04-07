using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionBehavior : MonoBehaviour
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
    }

    /// <summary>
    /// To be called when the unit is selected.
    /// </summary>
    public virtual void OnSelection()
    {
        _isSelected = true;

        // Sets its selection icon to be active.
        _selectionIcon.gameObject.SetActive(true);
    }
    
    /// <summary>
    /// To be called when the unit is deselected.
    /// </summary>
    public virtual void OnDeselection()
    {
        _isSelected = false;

        // Sets its selection icon to be inactive.
        _selectionIcon.gameObject.SetActive(false);
    }
}
