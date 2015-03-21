using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class ConditionProfileEditor : EditorWindow
{
    static ConditionProfileSave _save;

    BattleConditionProfile_HitPoint _currentHitPointProfile;
    BattleConditionProfile_IsTargetInOffenseRange _currentRangeProfile;
    BattleConditionProfile_IsOffenseEnded _currentOffenseEndedProfile;
    BattleConditionProfile_ExistsTarget _currentExistsTargetProfile;

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

        GUILayout.BeginHorizontal();
        {
            GUILayout.BeginVertical(GUILayout.Width(200));
            {
                GUILayout.Label("Is OffenseEnded");
                CommonEditorUnitity.DrawData(_save.Conditions_IsOffenseEnded, ref _currentOffenseEndedProfile);
                CommonEditorUnitity.DrawAddData(_save.Conditions_IsOffenseEnded, ref _currentOffenseEndedProfile);
                CommonEditorUnitity.DrawRemoveData(_save.Conditions_IsOffenseEnded, ref _currentOffenseEndedProfile);
            } GUILayout.EndVertical();

            DrawDetail(_currentOffenseEndedProfile);
        } GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        {
            GUILayout.BeginVertical(GUILayout.Width(200));
            {
                GUILayout.Label("Exists Target");
                CommonEditorUnitity.DrawData(_save.Conditions_ExistsTarget, ref _currentExistsTargetProfile);
                CommonEditorUnitity.DrawAddData(_save.Conditions_ExistsTarget, ref _currentExistsTargetProfile);
                CommonEditorUnitity.DrawRemoveData(_save.Conditions_ExistsTarget, ref _currentExistsTargetProfile);
            } GUILayout.EndVertical();

            DrawDetail(_currentExistsTargetProfile);
        } GUILayout.EndHorizontal();

        CommonEditorUnitity.DrawSaveData(_save);
    }

    void DrawDetail(BattleConditionProfile profile)
    {
        if (profile == null) return;
        GUILayout.Label("Name", GUILayout.Width(150));
        profile.Name = EditorGUILayout.TextField(profile.Name, GUILayout.Width(150));
        profile.DrawConditionInEditor();
    }
}
