using System;
using UnityEditor;
using UnityEngine;

public class ConfirmWindow : EditorWindow
{
    string tipText = "Are you sure ?";
    Action action = null;

    static void Create(string tipText,Action action)
    {
        ConfirmWindow window = (ConfirmWindow)EditorWindow.GetWindow(typeof(ConfirmWindow));
        window.tipText = tipText;
        window.action = action;
        window.Show();
    }

    static void Create(Action action)
    {
        ConfirmWindow window = (ConfirmWindow)EditorWindow.GetWindow(typeof(ConfirmWindow));
        window.action = action;
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.Space();

        GUILayout.Label(tipText, EditorStyles.boldLabel);

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("OK"))
        {
            this.Close();
            action();
        }
        if (GUILayout.Button("Cancel"))
            this.Close();
        EditorGUILayout.EndHorizontal();
    }
}
