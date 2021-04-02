using UnityEditor;
using UnityEngine;
using Game.Data;

namespace Game.DevEditor
{
    [CustomPropertyDrawer(typeof(IntReference))]  //IntReference class is the SerializedProperty value
    public class IntReferenceDrawer : PropertyDrawer
    {
        private readonly string[] popupOptions =
        {
            "Use Constant", "Use Variable"
        };

        private GUIStyle popupStyle;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)    /*Draw the property inside a rect.
                                                                                                    Rect position = FloatReference property field in Inspector.
                                                                                                    SerializedProperty property = IntReference instance
                                                                                                    GUIContent label = name of IntReference instance*/
        {
            if (popupStyle == null)
            {
                popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));    //Create a new GUIStyle using PaneOptions as a template (Where does PaneOptions come from?)
                popupStyle.imagePosition = ImagePosition.ImageOnly;             //Only display the image, no text
            }

            label = EditorGUI.BeginProperty(position, label, property);

            #region Create Property Wrapper
            position = EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();

            #region Change Check
            // Get properties from FloatReference
            SerializedProperty useConstant = property.FindPropertyRelative("UseConstant");          //public bool UseConstant
            SerializedProperty constantValue = property.FindPropertyRelative("ConstantValue");      //public float ConstantValue
            SerializedProperty variable = property.FindPropertyRelative("Variable");                //public IntVariable Variable

            // Calculate rect for configuration button
            Rect buttonRect = new Rect(position);
            buttonRect.yMin += popupStyle.margin.top;
            buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
            position.xMin = buttonRect.xMax;                                                        //place the property field immediately right of the configuration button

            // Store old indent level and set it to 0, the PrefixLabel takes care of it
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            int result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, popupOptions, popupStyle);  //if useConstant is true, select index 0 of dropdown menu ("Use Constant" from popupOptions array),
                                                                                                                //else select index 1 ("Use Variable")
        
            useConstant.boolValue = result == 0;                                                                //Compare result to 0. Return true if result = 0, otherwise false. useConstant equals the returned value


            EditorGUI.PropertyField(position, useConstant.boolValue ? constantValue : variable, GUIContent.none);   //Display IntReference.constantValue in Inspector if useConstant is true, or IntReference.variable if false
            #endregion

            if (EditorGUI.EndChangeCheck())      //returns true if a change to the properties in the Inspector occured, update the associated properties in the source IntReference
            {
                property.serializedObject.ApplyModifiedProperties();
            }

            EditorGUI.indentLevel = indent;
            #endregion

            EditorGUI.EndProperty();
        }
    }
}

