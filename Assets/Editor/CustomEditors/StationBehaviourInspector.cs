using System;
using System.Linq;
using Core.Actions;
using Features.StationLogic;
using UnityEditor;
using UnityEngine;

namespace CustomEditors
{
    // [CustomEditor(typeof(StationBehaviour)), CanEditMultipleObjects]
    // public class StationBehaviourInspector : UnityEditor.Editor
    // { 
    //     public override void OnInspectorGUI()
    //     {
    //         base.OnInspectorGUI();
    //
    //         StationBehaviour station = (StationBehaviour) target;
    //
    //         GUILayout.Space(10);
    //
    //         ResizeArrays(station);
    //         
    //         HandlePropertyFields(station);
    //         
    //         //serializedObject.Update();
    //         //Undo.RecordObject(target, "Set actions in inspector");
    //     }
    //
    //     private void HandlePropertyFields(StationBehaviour station)
    //     {
    //         for (int i = 0; i < station.actionTypes.Count; i++)
    //         {
    //             EditorGUI.BeginChangeCheck();
    //             int number = EditorGUILayout.IntField(station.actionTypes[i].ToString(), station.actionCounts[i]);
    //
    //             if (EditorGUI.EndChangeCheck())
    //             {
    //                 //Undo.RecordObject(target, "Changed number of " + myTarget.actionTypes[i]);
    //                 station.actionCounts[i] = number;
    //                 EditorUtility.SetDirty(target);
    //             }
    //         }
    //     }
    //
    //     private void ResizeArrays(StationBehaviour station)
    //     {
    //         var targetTypes = (ActionType[]) Enum.GetValues(typeof(ActionType));
    //         var currentTypes = station.actionTypes.Union(new[] {ActionType.None}).ToArray();
    //         
    //         var oldTypes = currentTypes.Except(targetTypes);
    //         var missingTypes = targetTypes.Except(currentTypes);
    //
    //         foreach (var key in oldTypes)
    //         {
    //             Debug.Log("Remove: " + key);
    //             int index = station.actionTypes.FindIndex(type => key == type);
    //             station.actionTypes.RemoveAt(index);
    //             station.actionCounts.RemoveAt(index);
    //             EditorUtility.SetDirty(target);
    //         }
    //         
    //         foreach (var key in missingTypes)
    //         {
    //             Debug.Log("Add: " + key);
    //             station.actionTypes.Add(key);
    //             station.actionCounts.Add(0);
    //             EditorUtility.SetDirty(target);
    //         }
    //     }
    // }
}