using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

// [CustomPropertyDrawer(typeof(ActionCounter))]
// public class ActionCounterUIE : PropertyDrawer
// {
//     public override VisualElement CreatePropertyGUI(SerializedProperty property)
//     {
//         // Create property container element.
//         var container = new VisualElement();
//         
//         // Create property fields.
//         var amountField = new PropertyField(property.FindPropertyRelative("amount"));
//         var unitField = new PropertyField(property.FindPropertyRelative("unit"));
//         var nameField = new PropertyField(property.FindPropertyRelative("name"), "Fancy Name");
//         
//         // Add fields to the container.
//         container.Add(amountField);
//         container.Add(unitField);
//         container.Add(nameField);
//         
//         return container;
//     }
//     
//     public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
//     {
//         // Using BeginProperty / EndProperty on the parent property means that
//         // prefab override logic works on the entire property.
//         EditorGUI.BeginProperty(position, label, property);
//         
//         // Draw label
//         //position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
//         
//         // Calculate rects
//         var newPos = new Rect(position.x, position.y + 9, position.width, position.height);
//
//         SerializedProperty arrayProp = property.FindPropertyRelative("Count");
//
//         SerializedProperty firstIntProp = arrayProp.GetArrayElementAtIndex(0);
//
//         EditorGUI.PropertyField(position, firstIntProp, label);
//
//         SerializedProperty firstIntProp2 = arrayProp.GetArrayElementAtIndex(1);
//
//         EditorGUI.PropertyField(newPos, firstIntProp2, label);
//         
//         EditorGUI.EndProperty();
//
//         // etc
//     }
// }