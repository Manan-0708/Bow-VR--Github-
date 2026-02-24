using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;

public class SceneStateExporter
{
    [MenuItem("Tools/Export Scene State")]
    public static void ExportSceneState()
    {
        StringBuilder sb = new StringBuilder();

        Scene scene = SceneManager.GetActiveScene();
        sb.AppendLine("===== UNITY SCENE STATE EXPORT =====");
        sb.AppendLine("Scene Name: " + scene.name);
        sb.AppendLine("Scene Path: " + scene.path);
        sb.AppendLine("------------------------------------");

        GameObject[] rootObjects = scene.GetRootGameObjects();

        foreach (GameObject root in rootObjects)
        {
            ExportGameObject(root, sb, 0);
        }

        string path = Application.dataPath + "/SceneStateExport.txt";
        File.WriteAllText(path, sb.ToString());

        Debug.Log("Scene state exported to: " + path);
    }

    static void ExportGameObject(GameObject obj, StringBuilder sb, int indent)
    {
        string indentStr = new string(' ', indent * 4);

        sb.AppendLine(indentStr + "GameObject: " + obj.name);
        sb.AppendLine(indentStr + "  Active: " + obj.activeSelf);

        Transform t = obj.transform;
        sb.AppendLine(indentStr + "  Position: " + t.localPosition);
        sb.AppendLine(indentStr + "  Rotation: " + t.localEulerAngles);
        sb.AppendLine(indentStr + "  Scale: " + t.localScale);

        Component[] components = obj.GetComponents<Component>();
        sb.AppendLine(indentStr + "  Components:");

        foreach (Component comp in components)
        {
            if (comp != null)
                sb.AppendLine(indentStr + "    - " + comp.GetType().Name);
        }

        sb.AppendLine("");

        foreach (Transform child in obj.transform)
        {
            ExportGameObject(child.gameObject, sb, indent + 1);
        }
    }
}