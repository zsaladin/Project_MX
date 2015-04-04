using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    [System.Serializable]
    public class ParamData : Data
    {
        public List<KeyIntValue> IntParams = new List<KeyIntValue>();
        public List<KeyFloatValue> FloatParams = new List<KeyFloatValue>();
        public List<KeyBoolValue> BoolParams = new List<KeyBoolValue>();
        public List<KeyStringValue> StringParams = new List<KeyStringValue>();
        public List<KeyComparisonTypeValue> ComparisonParams = new List<KeyComparisonTypeValue>();
        public List<KeyRatioValueTypeValue> RatioValueParams = new List<KeyRatioValueTypeValue>();
        public List<KeyActionTypeValue> ActionTypeParams = new List<KeyActionTypeValue>();
        public List<KeyBuffActionTypeValue> BuffActionTypeParams = new List<KeyBuffActionTypeValue>();

        public int? GetInt(string key)
        {
            var result = IntParams.Find(item => item.Key == key);
            if (result == null) return null;

            return result.Value;
        }

        public float? GetFloat(string key)
        {
            var result = FloatParams.Find(item => item.Key == key);
            if (result == null) return null;

            return result.Value;
        }

        public bool? GetBool(string key)
        {
            var result = BoolParams.Find(item => item.Key == key);
            if (result == null) return null;

            return result.Value;
        }

        public string GetString(string key)
        {
            var result = StringParams.Find(item => item.Key == key);
            if (result == null) return null;

            return result.Value;
        }

        public ComparisonType? GetComparison(string key)
        {
            var result = ComparisonParams.Find(item => item.Key == key);
            if (result == null) return null;

            return result.Value;
        }

        public RatioValueType? GetRatioValue(string key)
        {
            var result = RatioValueParams.Find(item => item.Key == key);
            if (result == null) return null;

            return result.Value;
        }

        public ActionType? GetActionTypeValue(string key)
        {
            var result = ActionTypeParams.Find(item => item.Key == key);
            if (result == null) return null;

            return result.Value;
        }

        public BuffActionType? GetBuffActionTypeValue(string key)
        {
            var result = BuffActionTypeParams.Find(item => item.Key == key);
            if (result == null) return null;

            return result.Value;
        }

        public void Set(string key, int value)
        {
            var result = IntParams.Find(item => item.Key == key);
            if (result == null)
            {
                result = new KeyIntValue { Key = key };
                IntParams.Add(result);
            }

            result.Value = value;
        }

        public void Set(string key, float value)
        {
            var result = FloatParams.Find(item => item.Key == key);
            if (result == null)
            {
                result = new KeyFloatValue { Key = key };
                FloatParams.Add(result);
            }

            result.Value = value;
        }

        public void Set(string key, bool value)
        {
            var result = BoolParams.Find(item => item.Key == key);
            if (result == null)
            {
                result = new KeyBoolValue { Key = key };
                BoolParams.Add(result);
            }

            result.Value = value;
        }

        public void Set(string key, string value)
        {
            var result = StringParams.Find(item => item.Key == key);
            if (result == null)
            {
                result = new KeyStringValue { Key = key };
                StringParams.Add(result);
            }

            result.Value = value;
        }

        public void Set(string key, ComparisonType value)
        {
            var result = ComparisonParams.Find(item => item.Key == key);
            if (result == null)
            {
                result = new KeyComparisonTypeValue { Key = key };
                ComparisonParams.Add(result);
            }

            result.Value = value;
        }

        public void Set(string key, RatioValueType value)
        {
            var result = RatioValueParams.Find(item => item.Key == key);
            if (result == null)
            {
                result = new KeyRatioValueTypeValue { Key = key };
                RatioValueParams.Add(result);
            }

            result.Value = value;
        }

        public void Set(string key, ActionType value)
        {
            var result = ActionTypeParams.Find(item => item.Key == key);
            if (result == null)
            {
                result = new KeyActionTypeValue { Key = key };
                ActionTypeParams.Add(result);
            }

            result.Value = value;
        }

        public void Set(string key, BuffActionType value)
        {
            var result = BuffActionTypeParams.Find(item => item.Key == key);
            if (result == null)
            {
                result = new KeyBuffActionTypeValue { Key = key };
                BuffActionTypeParams.Add(result);
            }

            result.Value = value;
        }


        public int GetIntOrDefault(string key)
        {
            var result = IntParams.Find(item => item.Key == key);
            if (result == null) Set(key, 0);

            return GetInt(key).Value;
        }

        public float GetFloatOrDefault(string key)
        {
            var result = FloatParams.Find(item => item.Key == key);
            if (result == null) Set(key, 0.0f);

            return GetFloat(key).Value;
        }

        public bool GetBoolOrDefault(string key)
        {
            var result = BoolParams.Find(item => item.Key == key);
            if (result == null) Set(key, false);

            return GetBool(key).Value;
        }

        public string GetStringOrDefault(string key)
        {
            var result = StringParams.Find(item => item.Key == key);
            if (result == null) Set(key, "");

            return GetString(key);
        }

        public ComparisonType GetComparisonOrDefault(string key)
        {
            var result = ComparisonParams.Find(item => item.Key == key);
            if (result == null) Set(key, ComparisonType.Equal);

            return GetComparison(key).Value;
        }

        public RatioValueType GetRatioValueOrDefault(string key)
        {
            var result = RatioValueParams.Find(item => item.Key == key);
            if (result == null) Set(key, RatioValueType.Ratio);

            return GetRatioValue(key).Value;
        }

        public ActionType GetActionTypeOrDefault(string key)
        {
            var result = ActionTypeParams.Find(item => item.Key == key);
            if (result == null) Set(key, ActionType.Invalid);

            return GetActionTypeValue(key).Value;
        }

        public BuffActionType GetBuffActionTypeOrDefault(string key)
        {
            var result = BuffActionTypeParams.Find(item => item.Key == key);
            if (result == null) Set(key, BuffActionType.Invalid);

            return GetBuffActionTypeValue(key).Value;
        }
    }

    [System.Serializable]
    public class KeyIntValue
    {
        public string Key;
        public int Value;
    }

    [System.Serializable]
    public class KeyFloatValue
    {
        public string Key;
        public float Value;
    }

    [System.Serializable]
    public class KeyBoolValue
    {
        public string Key;
        public bool Value;
    }

    [System.Serializable]
    public class KeyStringValue
    {
        public string Key;
        public string Value;
    }

    [System.Serializable]
    public class KeyComparisonTypeValue
    {
        public string Key;
        public ComparisonType Value;
    }

    [System.Serializable]
    public class KeyRatioValueTypeValue
    {
        public string Key;
        public RatioValueType Value;
    }

    [System.Serializable]
    public class KeyActionTypeValue
    {
        public string Key;
        public ActionType Value;
    }

    [System.Serializable]
    public class KeyBuffActionTypeValue
    {
        public string Key;
        public BuffActionType Value;
    }
}