using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class ConditionProfileEditor : EditorWindow
{
    static ConditionProfileSave _save;

    BattleConditionProfile_HitPoint _currentHitPointProfile;
    BattleConditionProfile_IsTargetInOffenseRange _currentRangeProfile;

    [MenuItem("Custom/Profile/Condition")]
    static public void CreateConditionProfileWindow()
    {
        EditorWindow.GetWindow<ConditionProfileEditor>();
    }

    static void Init()
    {
        if (_save == null)
        {
            _save = ScriptableObjectUtility.GetAsset<ConditionProfileSave>(DataType.Profile);
        }
    }

    void OnGUI()
    {
        Init();

        GUILayout.BeginHorizontal();
        {
            GUILayout.BeginVertical(GUILayout.Width(200));
            {
                GUILayout.Label("Hit Point");
                CommonEditorUnitity.DrawData(_save.Conditions_HitPoint, ref _currentHitPointProfile);
                CommonEditorUnitity.DrawAddData(_save.Conditions_HitPoint, ref _currentHitPointProfile);
                CommonEditorUnitity.DrawRemoveData(_save.Conditions_HitPoint, ref _currentHitPointProfile);
            } GUILayout.EndVertical();


            DrawDetail(_currentHitPointProfile);
        } GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        {
            GUILayout.BeginVertical(GUILayout.Width(200));
            {
                GUILayout.Label("Is Target In Offensive Range");
                CommonEditorUnitity.DrawData(_save.Conditions_IsTargetInOffenseRange, ref _currentRangeProfile);
                CommonEditorUnitity.DrawAddData(_save.Conditions_IsTargetInOffenseRange, ref _currentRangeProfile);
                CommonEditorUnitity.DrawRemoveData(_save.Conditions_IsTargetInOffenseRange, ref _currentRangeProfile);
            } GUILayout.EndVertical();

            DrawDetail(_currentRangeProfile);
        } GUILayout.EndHorizontal();

        CommonEditorUnitity.DrawSaveData(_save);

            //GUILayout.BeginVertical();
            //{
            //    GUILayout.BeginHorizontal();
            //    {
            //        GUILayout.Label("Name", GUILayout.Width(150));
            //        EditorGUILayout.TextField(_currentProfile.Name, GUILayout.Width(150));
            //    } GUILayout.EndHorizontal();

            //    GUILayout.BeginHorizontal();
            //    {
            //        BattleConditionProfile newCondition = null;
            //        DrawSelectedConditionDetail(ref newCondition);
            //        if (newCondition != null)
            //        {
            //            int index = _save.Conditions.IndexOf(_currentProfile);
            //            _save.Conditions[index] = newCondition;
            //            _currentProfile = newCondition;
            //        }
            //    } GUILayout.EndHorizontal();
            //} GUILayout.EndVertical();
        
    }

    void DrawDetail(BattleConditionProfile profile)
    {
        if (profile == null) return;
        GUILayout.Label("Name", GUILayout.Width(150));
        profile.Name = EditorGUILayout.TextField(profile.Name, GUILayout.Width(150));
        profile.DrawConditionInEditor();
    }
}
