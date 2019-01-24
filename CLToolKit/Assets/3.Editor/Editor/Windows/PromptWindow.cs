using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PromptWindow : EditorWindow
{

    string tipText = "";

    static void Create(string tipText)
    {
        PromptWindow window = (PromptWindow)EditorWindow.GetWindow(typeof(PromptWindow));
        window.tipText = tipText;
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.Space();

        GUILayout.Label(tipText, EditorStyles.boldLabel);

        EditorGUILayout.Space();
        
        if (GUILayout.Button("OK"))
        {
            this.Close();
        }
    }
}
