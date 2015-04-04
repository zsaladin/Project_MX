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
    }
}
