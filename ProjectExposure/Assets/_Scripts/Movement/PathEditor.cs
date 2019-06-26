using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Path))]
public class PathEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Path myScript = (Path)target;

        var style = new GUIStyle(GUI.skin.button);
        style.normal.textColor = Color.red;
        
        if (GUILayout.Button("Generate Points"))
        {
            if (myScript.transform.childCount != 0)
            {
                EditorUtility.DisplayDialog("Warning", "Please clear this path first", "Da");
            }
            else
            {
                myScript.GeneratePoints();
            }
        }
        if (GUILayout.Button("Add Point"))
        {
            myScript.AddPoint();
        }
        GUILayout.Space(30);
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Clear Points"))
        {
            if ( EditorUtility.DisplayDialog("","Are you sure you want to clear path points???", "yes", "NO"))
            {
                myScript.DestroyPoints();
            }
        }
    }
    public void OnSceneGUI()
    {
        return;
        int lineSteps = 15;

        Path myScript = (Path)target;
        myScript.points = Path.GetAllPoints(myScript.GetFirstPoint());

        for (int i = 0; i < myScript.points.Count-2; i++)
        {
            Transform handleTransform = myScript.transform;
            Quaternion handleRotation = handleTransform.rotation;
            Vector3 p0 = (myScript.points[i].transform.position);
            Vector3 p1 = (myScript.points[i+1].transform.position);

            Handles.color = Color.white;
            Handles.DrawLine(p0, p1);
            Vector3 lineStart = p0;
            for (int j = 1; j <= lineSteps-3; j++)
            {
                Vector3 lineEnd = Path.GetPoint(myScript.points[j].transform.position, myScript.points[j + 1].transform.position,
                    myScript.points[j + 2].transform.position, lineSteps);

                Handles.DrawLine(lineStart, lineEnd);
                lineStart = lineEnd;
            }
        }

        /*
        
        Transform handleTransform = line.transform;
        Quaternion handleRotation = handleTransform.rotation;
        Vector3 p0 = handleTransform.TransformPoint(line.p0);
        Vector3 p1 = handleTransform.TransformPoint(line.p1);

        Handles.color = Color.white;
        Handles.DrawLine(p0, p1);
        Handles.DoPositionHandle(p0, handleRotation);
        Handles.DoPositionHandle(p1, handleRotation);
        */
    }

}
#endif
