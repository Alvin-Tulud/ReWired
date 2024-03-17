using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
[CustomEditor(typeof(ObjectColorChanger))]
public class SceneViewDragHandler : Editor
{
    static GameObject draggedObject;
    static ObjectColorChanger objectColorChanger = null;

    static SceneViewDragHandler()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    // Handles the scene GUI events
    static void OnSceneGUI(SceneView sceneView)
    {
        Event currentEvent = Event.current;

        switch (currentEvent.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                HandleDragEvent(sceneView, currentEvent);
                break;
        }
    }

    // Handles drag events
    static void HandleDragEvent(SceneView sceneView, Event currentEvent)
    {
        Vector2 mousePosition = currentEvent.mousePosition;
        mousePosition.y = sceneView.camera.pixelHeight - mousePosition.y;
        Vector2 worldPosition = sceneView.camera.ScreenToWorldPoint(mousePosition);
        Vector2 direction = Vector2.zero;
        RaycastHit2D hitInfo = Physics2D.Raycast(worldPosition, direction);

        if (hitInfo.collider != null && hitInfo.collider.gameObject.tag == "Floor")
        {
            HandleFloorHit(hitInfo.collider.gameObject);
        }
        else
        {
            HandleNoHit();
        }

        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

        if (currentEvent.type == EventType.DragPerform)
        {
            HandleDragPerform(currentEvent, worldPosition);
        }

        currentEvent.Use();
    }

    // Handles hits on floor objects
    static void HandleFloorHit(GameObject hitObject)
    {
        if (hitObject.GetComponent<ObjectColorChanger>() != null)
        {
            HandleObjectColorChanger(hitObject);
        }
        else
        {
            RestoreColor();
        }
    }

    // Handles no hit situation
    static void HandleNoHit()
    {
        RestoreColor();
    }

    // Handles the case when the hit object is an ObjectColorChanger
    static void HandleObjectColorChanger(GameObject hitObject)
    {
        if (objectColorChanger != null)
        {
            objectColorChanger.RestoreColor();
        }
        objectColorChanger = hitObject.GetComponent<ObjectColorChanger>();
        objectColorChanger.ChangeColor(Color.green);
    }

    // Restores color to the object
    static void RestoreColor()
    {
        if (objectColorChanger != null)
        {
            objectColorChanger.RestoreColor();
        }
    }

    // Handles the drag perform event
    static void HandleDragPerform(Event currentEvent, Vector2 worldPosition)
    {
        DragAndDrop.AcceptDrag();

        if (DragAndDrop.objectReferences.Length > 0)
        {
            draggedObject = DragAndDrop.objectReferences[0] as GameObject;
            HandleDraggedObject(worldPosition);
        }

        currentEvent.Use();
    }

    // Handles the dragged object
    static void HandleDraggedObject(Vector2 worldPosition)
    {
        GameObject instantiatedObject = PrefabUtility.InstantiatePrefab(draggedObject) as GameObject;
        Undo.RegisterCreatedObjectUndo(instantiatedObject, instantiatedObject.name);

        if (instantiatedObject != null && objectColorChanger != null)
        {
            instantiatedObject.transform.parent = objectColorChanger.transform;
            instantiatedObject.transform.localPosition = Vector3.zero;
        }
        else if (instantiatedObject != null)
        {
            instantiatedObject.transform.position = worldPosition;
        }

        RestoreColor();
    }
}