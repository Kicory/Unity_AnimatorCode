using System.Text;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(AnimatorParamHandler))]
public class AnimatorParamHandler_Editor : Editor {
	private ReorderableList listEnter;
	private ReorderableList listLeave;

	private void OnEnable() {
		listEnter = new ReorderableList(serializedObject, serializedObject.FindProperty("Enter"), true, true, true, true);
		listLeave = new ReorderableList(serializedObject, serializedObject.FindProperty("Leave"), true, true, true, true);
		listEnter.drawElementCallback = (Rect rect, int idx, bool isActive, bool isFocused) => {
			var elem = listEnter.serializedProperty.GetArrayElementAtIndex(idx);
			rect.y += 2;
			var typeProperty = elem.FindPropertyRelative("type");
			EditorGUI.PropertyField(new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
				typeProperty, GUIContent.none);
			EditorGUI.PropertyField(new Rect(rect.x + 60, rect.y, 40, EditorGUIUtility.singleLineHeight),
				elem.FindPropertyRelative("toDo"), GUIContent.none);
			EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y, 100, EditorGUIUtility.singleLineHeight),
				elem.FindPropertyRelative("name"), GUIContent.none);
			int typeIdx = typeProperty.enumValueIndex;
			if(typeIdx == 0 || typeIdx == 1) {
				//float or Int
				SerializedProperty valueHolder;
				if(typeIdx == 0) {
					valueHolder = elem.FindPropertyRelative("floatData");
				} else {
					//typeIdx == 1; (int)
					valueHolder = elem.FindPropertyRelative("intData");
				}
				EditorGUI.PropertyField(new Rect(rect.x + 200, rect.y, rect.width - 200, EditorGUIUtility.singleLineHeight),
					valueHolder, GUIContent.none);
			}
		};
		listLeave.drawElementCallback = (Rect rect, int idx, bool isActive, bool isFocused) => {
			var elem = listLeave.serializedProperty.GetArrayElementAtIndex(idx);
			rect.y += 2;
			var typeProperty = elem.FindPropertyRelative("type");
			EditorGUI.PropertyField(new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
				typeProperty, GUIContent.none);
			EditorGUI.PropertyField(new Rect(rect.x + 60, rect.y, 40, EditorGUIUtility.singleLineHeight),
				elem.FindPropertyRelative("toDo"), GUIContent.none);
			EditorGUI.PropertyField(new Rect(rect.x + 100, rect.y, 100, EditorGUIUtility.singleLineHeight),
				elem.FindPropertyRelative("name"), GUIContent.none);
			int typeIdx = typeProperty.enumValueIndex;
			if(typeIdx == 0 || typeIdx == 1) {
				//float or Int
				SerializedProperty valueHolder;
				if(typeIdx == 0) {
					valueHolder = elem.FindPropertyRelative("floatData");
				} else {
					//typeIdx == 1; (int)
					valueHolder = elem.FindPropertyRelative("intData");
				}
				EditorGUI.PropertyField(new Rect(rect.x + 200, rect.y, rect.width - 200, EditorGUIUtility.singleLineHeight),
					valueHolder, GUIContent.none);
			}
		};
	}
	public override void OnInspectorGUI() {
		serializedObject.Update();
		EditorGUILayout.LabelField("Enter");
		listEnter.DoLayoutList();
		EditorGUILayout.LabelField("Leave");
		listLeave.DoLayoutList();
		serializedObject.ApplyModifiedProperties();
	}
}