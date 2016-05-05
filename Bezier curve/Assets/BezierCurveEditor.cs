using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveEditor : Editor
{
    SerializedProperty endPoints;
    SerializedProperty controlPoints;

    Color color = Color.red;
    float size = 5.0f;
    
    void OnEnable()
    {
        endPoints = serializedObject.FindProperty("endPoints");
        controlPoints = serializedObject.FindProperty("controlPoints");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // simple version
        // EditorGUILayout.PropertyField(endPoints, true);
        // EditorGUILayout.PropertyField(controlPoints, true);

        // design-friendly version
        EditorGUILayout.PropertyField(endPoints.GetArrayElementAtIndex(0), new GUIContent("Start point"));
        EditorGUILayout.PropertyField(controlPoints.GetArrayElementAtIndex(0), new GUIContent("Start control point"));
        EditorGUILayout.PropertyField(controlPoints.GetArrayElementAtIndex(1), new GUIContent("End control point"));
        EditorGUILayout.PropertyField(endPoints.GetArrayElementAtIndex(1), new GUIContent("End point"));

        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Inspector Only", EditorStyles.boldLabel);
        color = EditorGUILayout.ColorField("Color", color);
        size = EditorGUILayout.FloatField("Size", size);
    }

    void OnSceneGUI()
    {
        serializedObject.Update();

        // Display gizmos to move points
        for (int i = 0; i < 2; ++i)
        {
            SerializedProperty endPoint = endPoints.GetArrayElementAtIndex(i);
            SerializedProperty controlPoint = controlPoints.GetArrayElementAtIndex(i);

            Vector3 offset = controlPoint.vector3Value - endPoint.vector3Value;
            endPoint.vector3Value = Handles.PositionHandle(endPoint.vector3Value, Quaternion.identity);
            controlPoint.vector3Value = Handles.PositionHandle(endPoint.vector3Value + offset, Quaternion.identity);

            Handles.DrawLine(endPoint.vector3Value, controlPoint.vector3Value);
        }

        serializedObject.ApplyModifiedProperties();

        Handles.DrawBezier(
            endPoints.GetArrayElementAtIndex(0).vector3Value,
            endPoints.GetArrayElementAtIndex(1).vector3Value,
            controlPoints.GetArrayElementAtIndex(0).vector3Value,
            controlPoints.GetArrayElementAtIndex(1).vector3Value,
            color,
            null,
            size);
    }
}
