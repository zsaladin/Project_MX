using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class ActorTypeProfileEditor : EditorWindow
{
    ActorTypeProfileSave _save;
    ActorTypeProfile _currentProfile;

    ActionType _selectedActionType;
    Vector2 _actionScroll;

    ConditionStateSet _currentConditionSet;
    BattleConditionProfile _currentConditionProfile;
    ConditionType _selectedConditionType;
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
                CommonEditorUnitity.DrawData(_save.ActorTypeProfiles, ref _currentProfile);
                CommonEditorUnitity.DrawAddData(_save.ActorTypeProfiles, ref _currentProfile);
                CommonEditorUnitity.DrawRemoveData(_save.ActorTypeProfiles, ref _currentProfile);
                CommonEditorUnitity.DrawSaveData(_save);

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

                GUILayout.BeginHorizontal();
                {
                    GUILayout.BeginVertical();
                    {
                        GUILayout.Label("States");
                        _stateScroll = GUILayout.BeginScrollView(_stateScroll, GUILayout.Width(150), GUILayout.Height(150));
                        {
                            var preStateProfile = _currentStateProfile;
                            CommonEditorUnitity.DrawData(_currentProfile.States, ref _currentStateProfile);
                            CommonEditorUnitity.DrawAddData(_currentProfile.States, ref _currentStateProfile);
                            CommonEditorUnitity.DrawRemoveData(_currentProfile.States, ref _currentStateProfile);
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
                    GUILayout.Label("Name" , GUILayout.Width(150));
                    _currentStateProfile.Name = GUILayout.TextField(_currentStateProfile.Name, GUILayout.Width(150));
                } GUILayout.EndHorizontal();

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
                        GUILayout.Label("Condition Property(Don't change the values)");
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
        _conditionScroll = GUILayout.BeginScrollView(_conditionScroll, GUILayout.Width(350), GUILayout.Height(150));
        {
            var states = _currentProfile.States.Select(item => item.Name).ToList();
            states.Insert(0, "Invalid");
            for(int i = 0; i < _currentStateProfile.ConditionStateSet.Count; ++i)
            {
                GUILayout.BeginHorizontal();
                {
                    ConditionStateSet set = _currentStateProfile.ConditionStateSet[i];
                    bool isSelected = _currentConditionSet == set;
                    if (EditorGUILayout.Toggle(isSelected))
                        _currentConditionSet = set;

                    set.ConditionType = (ConditionType)EditorGUILayout.EnumPopup(set.ConditionType, GUILayout.Width(150));
                    set.ConditionProfileID = EditorGUILayout.IntField(set.ConditionProfileID, GUILayout.Width(30));
                    GUILayout.Label("=>", GUILayout.Width(20));
                    set.StateProfileID =  EditorGUILayout.Popup(set.StateProfileID, states.ToArray(), GUILayout.Width(110));
                } GUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add", GUILayout.Width(150)))
            {
                _currentStateProfile.ConditionStateSet.Add(new ConditionStateSet());
            }

            if (GUILayout.Button("Remove", GUILayout.Width(150)))
            {
                _currentStateProfile.ConditionStateSet.Remove(_currentConditionSet);
            }
        } GUILayout.EndScrollView();

        
    }

    void DrawConditionDetail()
    {
        if (_currentConditionSet != null)
        {
            BattleConditionProfile selectedProfile = Manager.Data.ConditionProfileSave.GetConditionProfile(_currentConditionSet.ConditionType, _currentConditionSet.ConditionProfileID);
            if (selectedProfile != null)
                selectedProfile.DrawConditionInEditor();
        }
    }
}
