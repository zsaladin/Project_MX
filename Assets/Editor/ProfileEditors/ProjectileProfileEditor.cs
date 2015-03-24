using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class ProjectileProfileEditor : EditorWindow 
{
    ProjectileProfileSave _save;
    ProjectileProfile _currentProfile;

    [MenuItem("Custom/Profile/Projectile")]
    static public void CreateProjectileProfileWindow()
    {
        EditorWindow.GetWindow<ProjectileProfileEditor>();
    }

    void Init()
    {
        if (_save == null)
            _save = ScriptableObjectUtility.GetAsset<ProjectileProfileSave>(DataType.Profile);
    }

    void OnGUI()
    {
        Init();

        GUILayout.BeginHorizontal();
        {
            GUILayout.BeginVertical(GUILayout.Width(200));
            {
                CommonEditorUnitity.DrawData(_save.ProjectileProfiles, ref _currentProfile);
                CommonEditorUnitity.DrawAddData(_save.ProjectileProfiles, ref _currentProfile);
                CommonEditorUnitity.DrawRemoveData(_save.ProjectileProfiles, ref _currentProfile);
                CommonEditorUnitity.DrawSaveData(_save);
            } GUILayout.EndVertical();

            if (_currentProfile == null) return;

            GUILayout.BeginVertical(GUILayout.Width(200));
            {
                EditorGUILayout.LabelField("Name", GUILayout.Width(150));
                EditorGUILayout.LabelField("Prefab", GUILayout.Width(150));
                EditorGUILayout.LabelField("Projectile Type", GUILayout.Width(150));
                EditorGUILayout.LabelField("Speed", GUILayout.Width(150));
                if (_currentProfile.ProjectileMotionType == ProjectileMotionType.Arc)
                    EditorGUILayout.LabelField("Max Height", GUILayout.Width(150));
            } GUILayout.EndVertical();

            GUILayout.BeginVertical(GUILayout.Width(200));
            {
                _currentProfile.Name = EditorGUILayout.TextField(_currentProfile.Name, GUILayout.Width(150));
                _currentProfile.Prefab = EditorGUILayout.ObjectField(_currentProfile.Prefab, typeof(GameObject), GUILayout.Width(150)) as GameObject;
                _currentProfile.ProjectileMotionType = (ProjectileMotionType) EditorGUILayout.EnumPopup(_currentProfile.ProjectileMotionType, GUILayout.Width(150));
                _currentProfile.Speed = EditorGUILayout.FloatField(_currentProfile.Speed, GUILayout.Width(150));
                if (_currentProfile.ProjectileMotionType == ProjectileMotionType.Arc)
                    _currentProfile.MaxHeight = EditorGUILayout.FloatField(_currentProfile.MaxHeight, GUILayout.Width(150));
            } GUILayout.EndVertical();
        } GUILayout.EndHorizontal();

        


    }
}
