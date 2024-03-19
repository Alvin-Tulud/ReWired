using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class ObjectMoveHandler
{
    private static GameObject grabbedObject;
    private static Vector3 initialPosition;
    private static bool isDragging = false;

    static ObjectMoveHandler()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    // Handles the scene GUI events
    private static void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;
        Ray rayPosition = HandleUtility.GUIPointToWorldRay(e.mousePosition);
        Vector2 worldPosition = rayPosition.origin;

        switch (e.type)
        {
            case EventType.MouseDown:
                MouseDownEvent(worldPosition); // Process mouse down event
                break;
            case EventType.MouseDrag:
                MouseDragEvent(); // Process mouse drag event
                break;
            case EventType.MouseUp:
                MouseUpEvent(worldPosition); // Process mouse up event
                break;
        }
    }

    // Converts screen position to world position
    private static Vector2 GetWorldPosition(SceneView sceneView, Vector2 screenPosition)
    {
        screenPosition.y = sceneView.camera.pixelHeight - screenPosition.y;
        return sceneView.camera.ScreenToWorldPoint(screenPosition);
    }

    // Handles mouse down event
    private static void MouseDownEvent(Vector2 worldPosition)
    {
        if (Event.current.button != 0)
            return;

        worldPosition -= new Vector2(0.3f, 0.3f);
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, new Vector2(-1f, -1f), 2);

        if (IsValidGrab(hit))
        {
            grabbedObject = hit.collider.gameObject;
            initialPosition = grabbedObject.transform.position;
            isDragging = true;
        }
    }

    // Checks if the grab is valid
    private static bool IsValidGrab(RaycastHit2D hit)
    {
        return hit.collider != null && hit.collider.gameObject.tag != "Floor" && hit.collider.gameObject.tag != "Wall";
    }

    // Handles mouse drag event
    private static void MouseDragEvent()
    {
        if (isDragging && grabbedObject != null)
        {
            Undo.RecordObject(grabbedObject.transform, "Move Object");
        }
    }

    // Handles mouse up event
    private static void MouseUpEvent(Vector2 worldPosition)
    {
        if (isDragging && grabbedObject != null)
        {
            MoveObjectToFloor(worldPosition); // Move object to the floor if valid
            ResetDragState(); // Reset drag state
        }
    }

    // Moves the object to the floor if valid
    private static void MoveObjectToFloor(Vector2 worldPosition)
    {
        LayerMask layerMask = LayerMask.GetMask("Editor");
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, new Vector2(-1f, -1f), 2, layerMask);

        if (hit.collider != null && hit.collider.gameObject.tag == "Floor")
        {
            grabbedObject.transform.parent = hit.collider.gameObject.transform;
            grabbedObject.transform.localPosition = Vector3.zero;
        }
    }

    // Resets drag state
    private static void ResetDragState()
    {
        isDragging = false;
        grabbedObject = null;
    }
}