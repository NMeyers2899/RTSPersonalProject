using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerSelectionBehavior : MonoBehaviour
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
    /// The texture that will be drawn to the screen when the player left clicks and drags in the window.
    /// </summary>
    private Texture2D _selectionTexture;

    /// <summary>
    /// Will let the rest of the program know if the player is currently able to select something.
    /// </summary>
    private static bool _canSelect = true;

    private bool _isDragging = false;

    private Vector3 _mousePosition;

    /// <summary>
    /// The list of all units selected by the player.
    /// </summary>
    public List<Transform> SelectedUnits
    {
        get { return _selectedUnits; }
    }

    public Texture2D SelectionTexture
    {
        get
        {
            if(!_selectionTexture)
            {
                _selectionTexture = new Texture2D(1, 1);
                _selectionTexture.SetPixel(0, 0, Color.white);
                _selectionTexture.Apply();
            }

            return _selectionTexture;
        }
    }

    public static bool CanSelect
    {
        get { return _canSelect; }
        set { _canSelect = value; }
    }

    /// <summary>
    /// What to do when a unit is selected by the player.
    /// </summary>
    /// <param name="selectedObject"> The unit being selected. </param>
    private void SelectUnit(Transform selectedObject, bool canMultiSelect = false)
    {
        // If the player cannot multi select...
        if (!canMultiSelect)
            // Deselect the current units that are selected.
            DeselectUnits();

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

    /// <summary>
    /// Draws the selection texture to the screen.
    /// </summary>
    /// <param name="rect"> The rectangle that will be drawn. </param>
    /// <param name="color"> The color of the rectangle being drawn. </param>
    private void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, SelectionTexture);
    }

    /// <summary>
    /// Draws the border of the rectangle to the screen.
    /// </summary>
    /// <param name="rect"> The rectangle being drawn. </param>
    /// <param name="thickness"> How thick the border will be. </param>
    /// <param name="color"> The color of the border. </param>
    private void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        // The top of the border.
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        // The bottom of the border.
        DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
        // The left side of the border.
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        // The right side of the border.
        DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
    }

    /// <summary>
    /// Creates a rectangle using the positions given as the end points.
    /// </summary>
    /// <param name="screenPos1"> The first point of the screen. </param>
    /// <param name="screenPos2"> The second point of the screen. </param>
    /// <returns></returns>
    private Rect GetScreenRect(Vector3 screenPos1, Vector3 screenPos2)
    {
        // Find the bottom right and the top left of the screen.
        screenPos1.y = Screen.height - screenPos1.y;
        screenPos2.y = Screen.height - screenPos2.y;

        Vector3 bottomRight = Vector3.Max(screenPos1, screenPos2);
        Vector3 topLeft = Vector3.Min(screenPos1, screenPos2);

        // Create the rectangle.
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

    private Bounds GetViewPortBounds(Camera cam, Vector3 screenPos1, Vector3 screenPos2)
    {
        Vector3 boundsPos1 = cam.ScreenToViewportPoint(screenPos1);
        Vector3 boundsPos2 = cam.ScreenToViewportPoint(screenPos2);

        Vector3 minimum = Vector3.Min(boundsPos1, boundsPos2);
        Vector3 maximum = Vector3.Max(boundsPos1, boundsPos2);

        minimum.z = cam.nearClipPlane;
        maximum.z = cam.farClipPlane;

        Bounds bounds = new Bounds();
        bounds.SetMinMax(minimum, maximum);

        return bounds;
    }

    private void OnGUI()
    {
        if(_isDragging)
        {
            Rect rect = GetScreenRect(_mousePosition, Input.mousePosition);
            DrawScreenRect(rect, new Color(0f, 0f, 0f, 0.25f));
            DrawScreenRectBorder(rect, 3, Color.green);
        }
    }

    private void Update()
    {
        // If the player left clicks...
        if(Input.GetMouseButtonDown(0))
        {
            // ...update the current mouse position, and...
            _mousePosition = Input.mousePosition;
            // ...create a ray to check if something gets hit.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Check if something was hit, if it was...
            if(Physics.Raycast(ray, out _hit))
            {
                // ...check the tag of whatever was hit. If it was a unit...
                if(_hit.transform.gameObject.CompareTag("Units"))
                {
                    // ...run the SelectUnit method, giving it the transform of what was hit.
                    SelectUnit(_hit.transform, Input.GetKey(KeyCode.LeftShift));
                }
                // If it was not a unit...
                else
                {
                    _isDragging = true;
                    // ...run the DeselectUnits method.
                    DeselectUnits();
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }
    }
}
