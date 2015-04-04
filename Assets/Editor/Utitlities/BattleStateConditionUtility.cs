using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public static class BattleStateConditionUtility
    {
        public static void DrawParams(BattleStateConditionProfile profile)
        {
            GUILayoutOption option = GUILayout.Width(120);
            switch (profile.Type)
            {
                case StateConditionType.HitPoint:
                    DrawParamHitPoint(profile, option);
                    break;

                case StateConditionType.ExistsTarget:
                    DrawParamExistsTarget(profile, option);
                    break;

                case StateConditionType.IsTargetInOffenseRange:
                    DrawParamExistsTarget(profile, option);
                    break;

                case StateConditionType.IsActionEnded:
                    string key;
                    EditorGUILayout.BeginHorizontal();
                    {
                        key = "ActionType";
                        EditorGUILayout.LabelField(key, option);
                        profile.Params.Set(key, (ActionType)EditorGUILayout.EnumPopup(profile.Params.GetActionTypeOrDefault(key), option));
                    } EditorGUILayout.EndHorizontal();
                    break;

            }
        }

        public static void DrawParamHitPoint(BattleStateConditionProfile profile, GUILayoutOption option)
        {
            string key;
            EditorGUILayout.BeginHorizontal();
            {
                key = "StandardHitPoint";
                EditorGUILayout.LabelField(key, option);
                profile.Params.Set(key, EditorGUILayout.FloatField(profile.Params.GetFloatOrDefault(key), option));
            } EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            {
                key = "RatioValueType";
                EditorGUILayout.LabelField(key, option);
                profile.Params.Set(key, (RatioValueType)EditorGUILayout.EnumPopup(profile.Params.GetRatioValueOrDefault(key), option));
            } EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                key = "ComparisonType";
                EditorGUILayout.LabelField(key, option);
                profile.Params.Set(key, (ComparisonType)EditorGUILayout.EnumMaskField(profile.Params.GetComparisonOrDefault(key), option));
            } EditorGUILayout.EndHorizontal();
        }

        public static void DrawParamExistsTarget(BattleStateConditionProfile profile, GUILayoutOption option)
        {
            string key;
            EditorGUILayout.BeginHorizontal();
            {
                key = "IsTrue";
                EditorGUILayout.LabelField(key, option);
                profile.Params.Set(key, EditorGUILayout.Toggle(profile.Params.GetBoolOrDefault(key), option));
            } EditorGUILayout.EndHorizontal();
        }
    }
}