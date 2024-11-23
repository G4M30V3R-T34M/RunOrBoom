using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GunSO))]
public class GunSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get reference to the target ScriptableObject
        GunSO gunSo = (GunSO)target;

        // Draw the default inspector, excluding the LayerMask
        DrawCustomFields(gunSo);

        // Apply any modified properties to the serialized object
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawCustomFields(GunSO gunSo)
    {
        SerializedProperty property = serializedObject.GetIterator();
        property.NextVisible(true);
        while (property.NextVisible(false))
        {
            if (property.name == "collisionLayerMask")
            {
                DisplayLayerMask(gunSo);
                AddGenerateLayerMaskButton(gunSo);
            }
            else
            {
                EditorGUILayout.PropertyField(property, true);
            }
        }
    }

    private void DisplayLayerMask(GunSO gunSo)
    {
        // Display the LayerMask as read-only
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.IntField("Generated Collision LayerMask", gunSo.collisionLayerMask);
        EditorGUI.EndDisabledGroup();
    }

    private void AddGenerateLayerMaskButton(GunSO gunSo)
    {
        // Add a button to generate the LayerMask
        if (GUILayout.Button("Generate LayerMask"))
        {
            gunSo.collisionLayerMask = LayerMaskHelper.CreateLayerMask(gunSo.collisionLayers);
            Debug.Log("Generated LayerMask: " + gunSo.collisionLayerMask);

            // Mark the ScriptableObject as dirty to save changes
            EditorUtility.SetDirty(gunSo);
        }
    }
}
