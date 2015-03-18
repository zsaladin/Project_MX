using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public static class CommonEditorUnitity 
{
    public static bool DrawData<T>(List<T> data, ref T currentData) where T : Data
    {
        T previoutData = currentData;

        int profileCount = data.Count;
        for(int i = 0; i < profileCount; ++i)
        {
            T item = data[i];

            string label = string.Format("{0}. {1}", item.ID, item.Name);
            bool isSelected = currentData == item;
            isSelected = GUILayout.Toggle(isSelected, label);
            if (isSelected) currentData = item;
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
}






