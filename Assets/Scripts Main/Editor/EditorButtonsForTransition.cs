using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TransitionController))]
public class EditorButtonsForTransition : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TransitionController transitionController = (TransitionController)target;
        if (GUILayout.Button("Go To Mars"))
        {
            transitionController.GoToMars();
        }
        if (GUILayout.Button("Go to Moon"))
        {
            transitionController.GoToMoon();
        }
        if (GUILayout.Button("Go to Earth"))
        {
            transitionController.GoToEarth();
        }
        if (GUILayout.Button("Go to Classroom"))
        {
            transitionController.GoToClassroom();
        }
        if (GUILayout.Button("Land"))
        {
            transitionController.Land();
        }
    }
}
