using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq; // Add this line for LINQ support
using System.Reflection;
using System.IO;

public class CustomComponentCreator : EditorWindow
{
    private GameObject selectedObject;
    private List<Type> componentTypes = new List<Type>();
    private int selectedComponentIndex = 0;
    private List<Component> componentList = new List<Component>();
    private string newComponentName = "";

    private string componentFolderPath = "Assets/Scripts/GameObjects/GridObjects/Components/CompTypes";

    [MenuItem("Tools/Create Custom Component")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CustomComponentCreator));
    }

    void OnEnable()
    {
        // Load all available component types from the specified namespace
        LoadComponentTypes("GridObjects.Components.CompTypes");
    }

    void OnGUI()
    {
        GUILayout.Label("Custom Component Creator", EditorStyles.boldLabel);

        selectedObject = EditorGUILayout.ObjectField("Selected Object", selectedObject, typeof(GameObject), true) as GameObject;

        // Display dropdown menu to select component type
        selectedComponentIndex = EditorGUILayout.Popup("Component Type", selectedComponentIndex, GetComponentTypeNames());

        if (GUILayout.Button("Add Component"))
        {
            AddComponent();
        }

        GUILayout.Space(10);
        GUILayout.Label("Component List", EditorStyles.boldLabel);
        DisplayComponentList();

        GUILayout.Space(10);
        GUILayout.Label("Add New Component", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        newComponentName = EditorGUILayout.TextField("Component Name", newComponentName);
        if (GUILayout.Button("+", GUILayout.Width(20)))
        {
            AddNewComponent();
        }
        GUILayout.EndHorizontal();
    }

    void AddComponent()
    {
        if (selectedObject != null)
        {
            Type selectedType = componentTypes[selectedComponentIndex];
            Component newComponent = selectedObject.AddComponent(selectedType);
            componentList.Add(newComponent);
            Debug.Log("Added component " + selectedType.Name + " to " + selectedObject.name);
        }
        else
        {
            Debug.LogWarning("Please select a GameObject to add the component to.");
        }
    }

    void DisplayComponentList()
    {
        foreach (Component component in componentList)
        {
            GUILayout.Label(component.GetType().Name);
        }
    }

    void LoadComponentTypes(string namespaceName)
    {
        componentTypes.Clear();

        // Get all loaded assemblies
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (Assembly assembly in assemblies)
        {
            // Filter assemblies by namespace
            Type[] types = assembly.GetTypes().Where(t => t.Namespace == namespaceName).ToArray();
            componentTypes.AddRange(types);
        }
    }

    string[] GetComponentTypeNames()
    {
        List<string> typeNames = new List<string>();

        foreach (Type type in componentTypes)
        {
            // Get only the class name without namespace
            string className = type.Name;
            typeNames.Add(className);
        }

        return typeNames.ToArray();
    }

    void AddNewComponent()
    {
         if (!string.IsNullOrEmpty(newComponentName))
        {
            string newComponentPath = Path.Combine(componentFolderPath, newComponentName + ".cs");

            // Check if the component file already exists
            if (!File.Exists(newComponentPath))
            {
                // Create the new component file
                File.WriteAllText(newComponentPath, GenerateComponentTemplate(newComponentName));
                Debug.Log("Added new component: " + newComponentName);

                AssetDatabase.Refresh();
            }
            else
            {
                Debug.LogWarning("Component file already exists.");
            }
        }
        else
        {
            Debug.LogWarning("Please enter a valid component name.");
        }
    }

    string GenerateComponentTemplate(string componentName)
        {
            // Generate the content for the new component file
            string template = 
$@"using UnityEngine;

namespace GridObjects.Components.CompTypes
{{
    public class {componentName} : MonoBehaviour
    {{
        // Add your custom component code here
    }}
}}";

            return template;
        }
}