using System;
using UnityEditor;
using UnityEngine;
using Game.Utility;

namespace Game.DevEditor
{
    //Control visibility of properties in Level Transition class
    [CustomEditor(typeof(LevelTransition))]
    public class LevelTransitionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            LevelTransition levelTransition = target as LevelTransition;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("targetScene"));
            levelTransition.useLoadingScreen = GUILayout.Toggle(levelTransition.useLoadingScreen, "Use Loading Screen");

            using (var loadingScreenGroup = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(levelTransition.useLoadingScreen)))
            {
                if (loadingScreenGroup.visible)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("loadScreenLocation"));
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("loadScreenLocationVariable"), new GUIContent("Transform Reference"));
                    EditorGUI.indentLevel--;

                    levelTransition.minLoadingWaitTime = EditorGUILayout.FloatField("Min Loading Wait", levelTransition.minLoadingWaitTime);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("loadingScreenEvent"));
                    EditorGUI.indentLevel--;
                }
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("profileVR"));

            serializedObject.ApplyModifiedProperties();
        }

    }
}

