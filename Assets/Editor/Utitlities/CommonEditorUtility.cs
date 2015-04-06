using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public static class CommonEditorUtility
    {
        public static bool DrawData<T>(List<T> data, ref T currentData, bool isEditable = false) where T : Data
        {
            T previoutData = currentData;

            for (int i = 0; i < data.Count; ++i)
            {
                GUILayout.BeginHorizontal(GUILayout.Width(150));
                {
                    T item = data[i];

                    string label = isEditable ?
                        string.Format("{0}. ", item.ID) :
                        string.Format("{0}. {1}", item.ID, item.Name);

                    bool isSelected = currentData == item;
                    isSelected = GUILayout.Toggle(isSelected, label);
                    if (isSelected) currentData = item;

                    if (isEditable)
                    {
                        item.Name = GUILayout.TextField(item.Name, GUILayout.Width(120));
                    }
                } GUILayout.EndHorizontal();
            }

            if (previoutData != currentData)
            {
                GUI.FocusControl("");
                return true;
            }
            return false;
        }

        public static T DrawAddData<T>(List<T> data, ref T currentData) where T : Data, new()
        {
            bool isClicked = GUILayout.Button("Add");
            if (isClicked)
            {
                int newID = 1;
                if (data.Count > 0)
                {
                    T lastData = data[data.Count - 1];
                    newID = lastData.ID + 1;
                }

                T newData = new T();
                newData.ID = newID;
                newData.Name = "New";

                data.Add(newData);
                currentData = newData;
                return newData;
            }
            return null;
        }

        public static T DrawRemoveData<T>(List<T> data, ref T currentData) where T : Data
        {
            bool isClicked = GUILayout.Button("Remove");
            if (isClicked)
            {
                bool isOK = EditorUtility.DisplayDialog("Warning", "This data will be removed!", "OK", "Cancel");
                if (isOK)
                {
                    T removeData = currentData;
                    data.RemoveAll(item => item == removeData);
                    currentData = null;
                    return removeData;
                }
            }

            return null;
        }

        public static void DrawSaveData(ScriptableObject save)
        {
            bool isClicked = GUILayout.Button("Save");
            if (isClicked)
            {
                ScriptableObjectUtility.SaveAsset(save);
            }
        }

        public static void DrawHorizontalLine(Color color)
        {
            Color originalColor = GUI.color;
            GUI.color = color;
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
            GUI.color = originalColor;
        }
        public static void DrawParam(ParamData param, string key, System.Type type)
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField(key, GUILayout.Width(100));
                if (type == typeof(float))
                    param.Set(key, EditorGUILayout.FloatField(param.GetFloatOrDefault(key), GUILayout.Width(100)));
                else if (type == typeof(int))
                    param.Set(key, EditorGUILayout.IntField(param.GetIntOrDefault(key), GUILayout.Width(100)));
                else if (type == typeof(bool))
                    param.Set(key, EditorGUILayout.Toggle(param.GetBoolOrDefault(key), GUILayout.Width(100)));
                else if (type == typeof(string))
                    param.Set(key, EditorGUILayout.TextField(param.GetStringOrDefault(key), GUILayout.Width(100)));
                else if (type == typeof(RatioValueType))
                    param.Set(key, (RatioValueType)EditorGUILayout.EnumPopup(param.GetRatioValueOrDefault(key), GUILayout.Width(100)));
                else if (type == typeof(ComparisonType))
                    param.Set(key, (ComparisonType)EditorGUILayout.EnumMaskField(param.GetComparisonOrDefault(key), GUILayout.Width(100)));
                
            } EditorGUILayout.EndHorizontal();
        }

        public static void DrawEffect(BattleEffectProfile profile)
        {
            if (profile == null) return;
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.LabelField("Effect Params");
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Prefab", GUILayout.Width(100));
                    GameObject prevObj = profile.Prefab;
                    profile.Prefab = EditorGUILayout.ObjectField(profile.Prefab, typeof(GameObject), GUILayout.Width(100)) as GameObject;
                    if (prevObj != profile.Prefab)
                        profile.Name = profile.Prefab.name;

                } EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Delay", GUILayout.Width(100));
                    profile.Delay = EditorGUILayout.FloatField(profile.Delay, GUILayout.Width(100));
                } EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Duration", GUILayout.Width(100));
                    profile.Duration = EditorGUILayout.FloatField(profile.Duration, GUILayout.Width(100));
                } EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("IsGlobal", GUILayout.Width(100));
                    profile.IsGlobal = EditorGUILayout.Toggle(profile.IsGlobal, GUILayout.Width(100));
                } EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Node Type", GUILayout.Width(100));
                    profile.NodeType = (EffectNodeType)EditorGUILayout.EnumPopup(profile.NodeType, GUILayout.Width(100));
                } EditorGUILayout.EndHorizontal();
            } EditorGUILayout.EndVertical();
        }
    }
}