using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public static class BattleSkillEditorUtility
    {
        public static void DrawParams(BattleSkillProfile profile)
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(200));
            {
                EditorGUILayout.LabelField("Skill Params");
                switch (profile.Type)
                {
                    case SkillType.Bomb:
                        DrawSkillBomb(profile);
                        break;

                    case SkillType.Splash:
                        DrawSkillSplash(profile);
                        break;
                }
            } EditorGUILayout.EndVertical();
        }

        static void DrawSkillBomb(BattleSkillProfile profile)
        {
            CommonEditorUtility.DrawParam(profile.Params, "ProjectileID", typeof(int));
            CommonEditorUtility.DrawParam(profile.Params, "Radius", typeof(float));
            CommonEditorUtility.DrawParam(profile.Params, "Damage", typeof(float));
        }

        static void DrawSkillSplash(BattleSkillProfile profile)
        {
            CommonEditorUtility.DrawParam(profile.Params, "Radius", typeof(float));
            CommonEditorUtility.DrawParam(profile.Params, "Damage", typeof(float));
        }

        public static void DrawConditions(BattleSkillProfile profile, BattleSkillConditionProfile conditionProfile)
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Condition Type", GUILayout.Width(100));
                conditionProfile.Type = (SkillConditionType)EditorGUILayout.EnumPopup(conditionProfile.Type, GUILayout.Width(100));
                if (GUILayout.Button("X", GUILayout.Width(30)))
                {
                    profile.Conditions.Remove(conditionProfile);
                    return;
                }
            } EditorGUILayout.EndHorizontal();
            switch (conditionProfile.Type)
            {
                case SkillConditionType.CoolTime:
                    DrawCoolTime(conditionProfile);
                    break;
                case SkillConditionType.Range:
                    DrawRange(conditionProfile);
                    break;
            }
        }

        static void DrawCoolTime(BattleSkillConditionProfile profile)
        {
            CommonEditorUtility.DrawParam(profile.Params, "CoolTime", typeof(float));
        }

        static void DrawRange(BattleSkillConditionProfile profile)
        {
            CommonEditorUtility.DrawParam(profile.Params, "Range", typeof(float));
        }

        public static void DrawBuffActions(BattleBuffProfile buffProfile, ref BattleBuffActionProfile actionProfile)
        {
            if (buffProfile == null) return;
            //if (actionProfile == null) return;
            EditorGUILayout.BeginVertical(GUILayout.Width(150));
            {
                EditorGUILayout.LabelField("Buff Actions");

                foreach (var action in buffProfile.Actions)
                {
                    GUILayout.BeginHorizontal(GUILayout.Width(80));
                    {
                        string label = string.Format("{0}. ", action.ID.ToString());
                        if (GUILayout.Toggle(action == actionProfile, label, GUILayout.Width(30)))
                            actionProfile = action;
                        action.Type = (BuffActionType)EditorGUILayout.EnumPopup(action.Type, GUILayout.Width(100));
                    } GUILayout.EndHorizontal();
                }
                CommonEditorUtility.DrawAddData(buffProfile.Actions, ref actionProfile);
                CommonEditorUtility.DrawRemoveData(buffProfile.Actions, ref actionProfile);
                
            } EditorGUILayout.EndVertical();
        }

        public static void DrawBuffActionParams(BattleBuffProfile buffProfile, BattleBuffActionProfile actionProfile)
        {
            if (buffProfile == null) return;
            if (actionProfile == null) return;
            EditorGUILayout.BeginVertical(GUILayout.Width(150));
            {
                EditorGUILayout.LabelField("Buff Action Params");
                switch(actionProfile.Type)
                {
                    case BuffActionType.Interruption:
                        break;
                    case BuffActionType.Airborn:
                        CommonEditorUtility.DrawParam(actionProfile.Params, "Duration", typeof(float));
                        break;
                }
            } EditorGUILayout.EndVertical();

        }

        public static void DrawBuffCondition(BattleBuffProfile buffProfile, ref BattleBuffConditionProfile conditionProfile)
        {
            if (buffProfile == null) return;
            
            EditorGUILayout.BeginVertical(GUILayout.Width(150));
            {
                EditorGUILayout.LabelField("Buff Conditions");

                foreach (var condition in buffProfile.Conditions)
                {
                    GUILayout.BeginHorizontal(GUILayout.Width(80));
                    {
                        string label = string.Format("{0}. ", condition.ID.ToString());
                        if (GUILayout.Toggle(condition == conditionProfile, label, GUILayout.Width(30)))
                            conditionProfile = condition;
                        condition.Type = (BuffConditionType)EditorGUILayout.EnumPopup(condition.Type, GUILayout.Width(100));
                    } GUILayout.EndHorizontal();
                }
                CommonEditorUtility.DrawAddData(buffProfile.Conditions, ref conditionProfile);
                CommonEditorUtility.DrawRemoveData(buffProfile.Conditions, ref conditionProfile);

            } EditorGUILayout.EndVertical();
        }

        public static void DrawBuffConditionParams(BattleBuffProfile buffProfile, BattleBuffConditionProfile conditionProfile)
        {
            if (buffProfile == null) return;
            if (conditionProfile == null) return;

            EditorGUILayout.BeginVertical(GUILayout.Width(150));
            {
                EditorGUILayout.LabelField("Buff Condition Params");
                switch (conditionProfile.Type)
                {
                    case BuffConditionType.Duration:
                        CommonEditorUtility.DrawParam(conditionProfile.Params, "Duration", typeof(float));
                        break;
                }
            } EditorGUILayout.EndVertical();
        }
    }
}
