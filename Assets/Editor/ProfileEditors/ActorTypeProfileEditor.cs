using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class ActorTypeProfileEditor : EditorWindow
    {
        ActorTypeProfileSave _save;
        ActorTypeProfile _currentProfile;

        ActionType _selectedActionType;
        Vector2 _actionScroll;

        ConditionStateSet _currentConditionSet;
        BattleStateConditionProfile _currentConditionProfile;
        StateConditionType _selectedConditionType;
        Vector2 _conditionScroll;

        BattleStateProfile _currentStateProfile;
        Vector2 _stateScroll;

        [MenuItem("Custom/Profile/ActorType")]
        static public void CreateActorTypeProfileWindow()
        {
            EditorWindow.GetWindow<ActorTypeProfileEditor>();
        }

        void Init()
        {
            if (_save == null)
            {
                _save = ScriptableObjectUtility.GetAsset<ActorTypeProfileSave>(DataType.Profile);
            }
        }

        void OnGUI()
        {
            Init();

            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical(GUILayout.Width(200));
                {
                    var preProfile = _currentProfile;
                    CommonEditorUtility.DrawData(_save.ActorTypeProfiles, ref _currentProfile);
                    CommonEditorUtility.DrawAddData(_save.ActorTypeProfiles, ref _currentProfile);
                    CommonEditorUtility.DrawRemoveData(_save.ActorTypeProfiles, ref _currentProfile);
                    CommonEditorUtility.DrawSaveData(_save);

                    if (preProfile != _currentProfile)
                    {
                        _currentStateProfile = null;
                        _currentConditionSet = null;
                    }
                } GUILayout.EndVertical();

                if (_currentProfile == null)
                {
                    return;
                }

                GUILayout.BeginVertical(GUILayout.Width(300));
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Action Type Name", GUILayout.Width(150));
                        _currentProfile.Name = EditorGUILayout.TextField(_currentProfile.Name, GUILayout.Width(150));
                    } GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal(GUILayout.Width(400));
                    {
                        GUILayout.BeginVertical();
                        {
                            GUILayout.Label("States");
                            _stateScroll = GUILayout.BeginScrollView(_stateScroll, GUILayout.Width(200), GUILayout.Height(250));
                            {
                                var preStateProfile = _currentStateProfile;
                                CommonEditorUtility.DrawData(_currentProfile.States, ref _currentStateProfile);
                                CommonEditorUtility.DrawAddData(_currentProfile.States, ref _currentStateProfile);
                                CommonEditorUtility.DrawRemoveData(_currentProfile.States, ref _currentStateProfile);
                                if (preStateProfile != _currentStateProfile)
                                {
                                    _currentConditionSet = null;
                                }

                            } GUILayout.EndScrollView();
                        } GUILayout.EndVertical();

                        if (_currentStateProfile == null)
                        {
                            return;
                        }
                        GUILayout.Label("Name", GUILayout.Width(50));
                        _currentStateProfile.Name = GUILayout.TextField(_currentStateProfile.Name, GUILayout.Width(150));
                    } GUILayout.EndHorizontal();

                    _currentStateProfile.IsDefault = GUILayout.Toggle(_currentStateProfile.IsDefault, "Is Default");
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.BeginVertical();
                        {
                            GUILayout.Label("Actions");
                            DrawActions();
                        } GUILayout.EndVertical();

                        GUILayout.BeginVertical();
                        {
                            GUILayout.Label("Conditions");
                            DrawConditions();
                        } GUILayout.EndVertical();
                        GUILayout.BeginVertical();
                        {
                            GUILayout.Label("Condition Property");
                            DrawConditionDetail();
                        } GUILayout.EndVertical();
                    } GUILayout.EndHorizontal();

                } GUILayout.EndVertical();

            } GUILayout.EndHorizontal();
        }

        void DrawActions()
        {
            _actionScroll = GUILayout.BeginScrollView(_actionScroll, GUILayout.Width(150), GUILayout.Height(150));
            {
                for (int i = 0; i < _currentStateProfile.Actions.Count; ++i)
                {
                    GUILayout.BeginHorizontal();
                    {
                        ActionType action = _currentStateProfile.Actions[i];
                        _currentStateProfile.Actions[i] = (ActionType)EditorGUILayout.EnumPopup(action, GUILayout.Width(110));
                        if (GUILayout.Button("X", GUILayout.Width(20)))
                            _currentStateProfile.Actions.RemoveAt(i);
                    } GUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Add"))
                {
                    _currentStateProfile.Actions.Add(ActionType.Invalid);
                }

            } GUILayout.EndScrollView();
        }

        void DrawConditions()
        {
            _conditionScroll = GUILayout.BeginScrollView(_conditionScroll, GUILayout.Width(395), GUILayout.Height(200));
            {
                var states = _currentProfile.States.Select(item => item.Name).ToList();
                states.Insert(0, "Invalid");
                for (int i = 0; i < _currentStateProfile.ConditionStateSet.Count; ++i)
                {
                    GUILayout.BeginHorizontal();
                    {
                        ConditionStateSet set = _currentStateProfile.ConditionStateSet[i];

                        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
                        buttonStyle.alignment = TextAnchor.MiddleCenter;
                        buttonStyle.fixedWidth = 35;
                        if (GUILayout.Button("▲", buttonStyle))
                        {
                            if (i > 0)
                            {
                                ExchangeConditionStateSet(i - 1, i);
                            }
                            break;
                        }
                        else if (GUILayout.Button("▼", buttonStyle))
                        {
                            if (i < _currentStateProfile.ConditionStateSet.Count - 1)
                            {
                                ExchangeConditionStateSet(i, i + 1);
                            }
                            break;
                        }


                        bool isSelected = _currentConditionSet == set;
                        if (EditorGUILayout.Toggle(isSelected))
                            _currentConditionSet = set;

                        set.ConditionProfile.Type = (StateConditionType)EditorGUILayout.EnumPopup(set.ConditionProfile.Type, GUILayout.Width(150));
                        GUILayout.Label("=>", GUILayout.Width(20));
                        set.StateProfileID = EditorGUILayout.Popup(set.StateProfileID, states.ToArray(), GUILayout.Width(80));
                    } GUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Add", GUILayout.Width(150)))
                {
                    _currentConditionSet = new ConditionStateSet();
                    _currentStateProfile.ConditionStateSet.Add(_currentConditionSet);
                }

                if (GUILayout.Button("Remove", GUILayout.Width(150)))
                {
                    _currentStateProfile.ConditionStateSet.Remove(_currentConditionSet);
                }
            } GUILayout.EndScrollView();


        }

        void ExchangeConditionStateSet(int first, int second)
        {
            ConditionStateSet temp = _currentStateProfile.ConditionStateSet[first];
            _currentStateProfile.ConditionStateSet[first] = _currentStateProfile.ConditionStateSet[second];
            _currentStateProfile.ConditionStateSet[second] = temp;
        }

        void DrawConditionDetail()
        {
            if (_currentConditionSet != null)
            {
                BattleStateEditorUtility.DrawParams(_currentConditionSet.ConditionProfile);
            }
        }
    }
}