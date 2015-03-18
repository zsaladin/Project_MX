using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class ActorProfileEditor : EditorWindow 
{
    ActorProfileSave _save;
    ActorProfile _currentProfile;

    [MenuItem("Custom/Profile/Actor")]
    static public void CreateActorProfileWindow()
    {
        EditorWindow.GetWindow<ActorProfileEditor>();
    }

    void Init()
    {
        if (_save == null)
            _save = ScriptableObjectUtility.GetAsset<ActorProfileSave>(DataType.Profile);
    }

    void OnGUI()
    {
        Init();

        GUILayout.BeginHorizontal();
        {
            GUILayout.BeginVertical(GUILayout.Width(200));
            {
                CommonEditorUnitity.DrawData(_save.ActorProfiles, ref _currentProfile);
                CommonEditorUnitity.DrawAddData(_save.ActorProfiles, ref _currentProfile);
                CommonEditorUnitity.DrawRemoveData(_save.ActorProfiles, ref _currentProfile);
                CommonEditorUnitity.DrawSaveData(_save);
            } GUILayout.EndVertical();

            if (_currentProfile == null) return;

            GUILayout.BeginVertical(GUILayout.Width(150));
            {
                GUILayout.Label("Name");
                GUILayout.Label("Prefab");
                GUILayout.Label("Actor Type");
                GUILayout.Label("Size");
                GUILayout.Label("HitPoint");
                GUILayout.Label("OffenseType");
                GUILayout.Label("OffensePower");
                GUILayout.Label("OffenseTime");
                GUILayout.Label("OffenseDealTime");
                GUILayout.Label("OffenseRange");
                GUILayout.Label("DefenseType");
                GUILayout.Label("MovingSpeed");
                
            } GUILayout.EndVertical();

            GUILayout.BeginVertical(GUILayout.Width(150));
            {
                _currentProfile.Name = EditorGUILayout.TextField(_currentProfile.Name);
                _currentProfile.Prefab = EditorGUILayout.ObjectField(_currentProfile.Prefab, typeof(GameObject)) as GameObject;

                var actionTypes = Manager.Data.ActorTypeProfileSave.ActorTypeProfiles.Select(item => item.Name).ToList();
                actionTypes.Insert(0, "Invalid");
                _currentProfile.ActorType = EditorGUILayout.Popup(_currentProfile.ActorType, actionTypes.ToArray());

                _currentProfile.Size = EditorGUILayout.FloatField(_currentProfile.Size);
                _currentProfile.HitPointMax = EditorGUILayout.FloatField(_currentProfile.HitPointMax);
                _currentProfile.OffenseType = (OffenseType) EditorGUILayout.EnumPopup(_currentProfile.OffenseType);
                _currentProfile.OffensePower = EditorGUILayout.FloatField(_currentProfile.OffensePower);
                _currentProfile.OffenseTime = EditorGUILayout.FloatField(_currentProfile.OffenseTime);
                _currentProfile.OffenseDealTime = EditorGUILayout.FloatField(_currentProfile.OffenseDealTime);
                _currentProfile.OffenseRange = EditorGUILayout.FloatField(_currentProfile.OffenseRange);
                _currentProfile.DefenseType = (DefenseType)EditorGUILayout.EnumPopup(_currentProfile.DefenseType);
                _currentProfile.MovingSpeed = EditorGUILayout.FloatField(_currentProfile.MovingSpeed);
                
            } GUILayout.EndVertical();


        } GUILayout.EndHorizontal();
    }
}
