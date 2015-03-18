using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public static class ScriptableObjectUtility
{
    static Dictionary<DataType, string> _savePath = new Dictionary<DataType, string>();
    static ScriptableObjectUtility()
    {
        _savePath.Add(DataType.Profile, "Data/Profiles");
        _savePath.Add(DataType.Record, "Data/Records");
    }

    public static T GetAsset<T>(DataType dataType) where T : ScriptableObject
    {
        string assetPath = string.Format("{0}/{1}", _savePath[dataType], typeof(T).Name);
        T asset = Resources.Load<T>(assetPath);
        if (asset != null) return asset;

        return CreateAsset<T>(dataType);
    }

    public static T CreateAsset<T>(DataType dataType) where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        string assetPath = string.Format("Assets/Resources/{0}/{1}.asset", _savePath[dataType], typeof(T).Name);

        AssetDatabase.CreateAsset(asset, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        return asset;
    }

    public static void SaveAsset(ScriptableObject scriptableObject)
    {
        EditorUtility.SetDirty(scriptableObject);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

public enum DataType
{
    Profile,
    Record,
}