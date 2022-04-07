using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerBehavior : MonoBehaviour
{
    /// <summary>
    /// The object that will be hit with a ray.
    /// </summary>
    private RaycastHit _hit;

    /// <summary>
    /// The list of all units selected by the player.
    /// </summary>
    [SerializeField]
    private List<Transform> _selectedUnits = new List<Transform>();

    /// <summary>
    /// The list of all units selected by the player.
    /// </summary>
    public List<Transform> SelectedUnits
    {
        get { return _selectedUnits; }
    }

    /// <summary>
    /// What to do when a unit is selected by the player.
    /// </summary>
    /// <param name="selectedObject"> The unit being selected. </param>
    private void SelectUnit(Transform selectedObject)
    {
        // The unit is added to the _selectedUnits list.
        _selectedUnits.Add(selectedObject);

        // Attempt to find a PlayerUnitMovementBehavior on the selected unit.
        UnitSelectionBehavior unit = selectedObject.GetComponent<UnitSelectionBehavior>();

        if (unit)
            unit.OnSelection();
            
    }

    /// <summary>
    /// What to do when the player clicks away from any unit.
    /// </summary>
    private void DeselectUnits()
    {
        for (int i = 0; i < _selectedUnits.Count; i++)
        {
            UnitSelectionBehavior unit = _selectedUnits[i].GetComponent<UnitSelectionBehavior>();

            if (unit)
                unit.OnDeselection();
        }

        _selectedUnits.Clear();
    }

    private void Update()
    {
        // If the player left clicks...
        if(Input.GetMouseButtonDown(0))
        {
            // ...create a ray to check if something gets hit.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Check if something was hit, if it was...
            if(Physics.Raycast(ray, out _hit))
            {
                // ...check the tag of whatever was hit. If it was a unit...
                if(_hit.transform.gameObject.CompareTag("Units"))
                {
                    // ...run the SelectUnit method, giving it the transform of what was hit.
                    SelectUnit(_hit.transform);
                }
                // If it was not a unit...
                else
                {
                    // ...run the DeselectUnits method.
                    DeselectUnits();
                }
            }
        }
    }
}
