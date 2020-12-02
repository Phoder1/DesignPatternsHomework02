using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private bool ControlsSettings = false;
    private bool LaserSettings = false;
    SerializedProperty laserPrefab;
    SerializedProperty mouseSensitivity;
    SerializedProperty invertMouse;
    SerializedProperty laserSpeed;
    SerializedProperty laserLife;
    SerializedProperty maxNumOfLasers;
    SerializedProperty collisionCopies;
    SerializedProperty copiesSpread;

    private void OnEnable() {
        //Is that really is the best way to do it?! I don't like using strings as names...
        laserPrefab = serializedObject.FindProperty("laserPrefab");
        mouseSensitivity = serializedObject.FindProperty("mouseSensitivity");
        invertMouse = serializedObject.FindProperty("invertMouse");
        laserSpeed = serializedObject.FindProperty("laserSpeed");
        laserLife = serializedObject.FindProperty("laserLife");
        maxNumOfLasers = serializedObject.FindProperty("maxNumOfLasers");
        collisionCopies = serializedObject.FindProperty("collisionCopies");
        copiesSpread = serializedObject.FindProperty("copiesSpread");
    }

    public override void OnInspectorGUI() {


        serializedObject.Update();
        ControlsSettings = EditorGUILayout.Foldout(ControlsSettings, "Controls Settings");
        if (ControlsSettings) {
            mouseSensitivity.floatValue = EditorGUILayout.Slider("Mouse Sensitivity", mouseSensitivity.floatValue, 100f, 200f);
            invertMouse.boolValue = EditorGUILayout.Toggle("Invert Mouse",invertMouse.boolValue);
        }
        LaserSettings = EditorGUILayout.Foldout(LaserSettings, "Laser Settings");
        if (LaserSettings) {
            laserSpeed.floatValue = EditorGUILayout.Slider("Laser Speed", laserSpeed.floatValue, 2f, 10f);
            laserLife.floatValue = EditorGUILayout.Slider("Laser Life (Seconds)", laserLife.floatValue, 0f, 3f);
            maxNumOfLasers.intValue = EditorGUILayout.IntSlider("Max # of Lasers", maxNumOfLasers.intValue, 10, 40);
            collisionCopies.intValue = EditorGUILayout.IntSlider("# of Collision Copies", collisionCopies.intValue, 0, 5);
            copiesSpread.floatValue = EditorGUILayout.Slider("Copies Spread", copiesSpread.floatValue, 2f, 20f);
            EditorGUILayout.PropertyField(laserPrefab,new GUIContent("Laser Prefab"));
        }
        serializedObject.ApplyModifiedProperties();
    }
}
