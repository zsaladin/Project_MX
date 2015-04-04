using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class ActorProfileEditor : EditorWindow
    {
        ActorProfileSave _save;
        ActorProfile _currentProfile;

        BattleSkillProfile _currentSkillProfile;
        BattleBuffProfile _currentBuffProfile;
        BattleBuffActionProfile _currentBuffActionProfile;
        BattleBuffConditionProfile _currentBuffCondProfile;

        [MenuItem("Custom/Profile/Actor")]
        static public void CreateActorProfileWindow()
        {
            EditorWindow.GetWindow<ActorProfileEditor>();
        }

        void Init()
        {
            if (_save == null)
                _save = ScriptableObjectUtility.GetAsset<ActorProfileSave>(DataType.Profile);
        }

        void OnGUI()
        {
            Init();

            GUILayout.BeginHorizontal();
            {
                DrawActorProfiles();
                if (_currentProfile == null) return;

                DrawActorBaseProperties();

                GUILayout.BeginVertical();
                {
                    GUILayout.BeginHorizontal();
                    {
                        DrawSkills();

                        if (_currentSkillProfile == null) return;
                        DrawSkillParams();
                        DrawSkillCondition();
                    } GUILayout.EndHorizontal();

                    GUILayout.BeginVertical();
                    {
                        DrawBuffs();
                        GUILayout.BeginHorizontal();
                        {
                            BattleSkillEditorUtility.DrawBuffActions(_currentBuffProfile, ref _currentBuffActionProfile);
                            BattleSkillEditorUtility.DrawBuffActionParams(_currentBuffProfile, _currentBuffActionProfile);
                        } GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        {
                            BattleSkillEditorUtility.DrawBuffCondition(_currentBuffProfile, ref _currentBuffCondProfile);
                            BattleSkillEditorUtility.DrawBuffConditionParams(_currentBuffProfile, _currentBuffCondProfile);
                        } GUILayout.EndHorizontal();
                    } GUILayout.EndVertical();
                } GUILayout.EndVertical();

            } GUILayout.EndHorizontal();
        }

        private void DrawBuffs()
        {
            GUILayout.BeginVertical(GUILayout.Width(100));
            {
                EditorGUILayout.LabelField("Buffs");
                CommonEditorUtility.DrawData(_currentSkillProfile.Buffs, ref _currentBuffProfile, true);
                CommonEditorUtility.DrawAddData(_currentSkillProfile.Buffs, ref _currentBuffProfile);
                CommonEditorUtility.DrawRemoveData(_currentSkillProfile.Buffs, ref _currentBuffProfile);
            } GUILayout.EndVertical();
        }

        void DrawActorProfiles()
        {
            GUILayout.BeginVertical(GUILayout.Width(200));
            {
                bool isChanged = CommonEditorUtility.DrawData(_save.ActorProfiles, ref _currentProfile);
                CommonEditorUtility.DrawAddData(_save.ActorProfiles, ref _currentProfile);
                CommonEditorUtility.DrawRemoveData(_save.ActorProfiles, ref _currentProfile);
                CommonEditorUtility.DrawSaveData(_save);
                if (isChanged)
                {
                    _currentSkillProfile = null;
                    _currentBuffProfile = null;
                    _currentBuffActionProfile = null;
                    _currentBuffCondProfile = null;
                }
            } GUILayout.EndVertical();
        }

        void DrawActorBaseProperties()
        {
            GUILayout.BeginVertical(GUILayout.Width(150));
            {
                GUILayout.Label("Name");
                GUILayout.Label("Prefab");
                GUILayout.Label("Actor Type");
                GUILayout.Label("Size");
                GUILayout.Label("HitPoint");
                GUILayout.Label("OffenseType");
                if (_currentProfile.OffenseType == OffenseType.Range || _currentProfile.OffenseType == OffenseType.Magic)
                {
                    GUILayout.Label("Offense Projectile Type");
                }
                GUILayout.Label("OffensePower");
                GUILayout.Label("OffenseTime");
                GUILayout.Label("OffenseDealTime");
                GUILayout.Label("OffenseRange");
                GUILayout.Label("DefenseType");
                GUILayout.Label("MovingSpeed");

            } GUILayout.EndVertical();

            GUILayout.BeginVertical(GUILayout.Width(150));
            {
                _currentProfile.Name = EditorGUILayout.TextField(_currentProfile.Name);
                _currentProfile.Prefab = EditorGUILayout.ObjectField(_currentProfile.Prefab, typeof(GameObject)) as GameObject;

                var actionTypes = Manager.Data.ActorTypeProfileSave.ActorTypeProfiles.Select(item => item.Name).ToList();
                actionTypes.Insert(0, "Invalid");
                _currentProfile.ActorType = EditorGUILayout.Popup(_currentProfile.ActorType, actionTypes.ToArray());

                _currentProfile.Size = EditorGUILayout.FloatField(_currentProfile.Size);
                _currentProfile.HitPointMax = EditorGUILayout.FloatField(_currentProfile.HitPointMax);
                _currentProfile.OffenseType = (OffenseType)EditorGUILayout.EnumPopup(_currentProfile.OffenseType);
                if (_currentProfile.OffenseType == OffenseType.Range || _currentProfile.OffenseType == OffenseType.Magic)
                {
                    var list = Manager.Data.ProjectileProfileSave.ProjectileProfiles.ToList();
                    int currentIndex = list.Select(item => item.ID).ToList().IndexOf(_currentProfile.OffenseProjectileType);
                    currentIndex = EditorGUILayout.Popup(currentIndex, list.Select(item => item.Name).ToArray());
                    if (currentIndex >= 0)
                        _currentProfile.OffenseProjectileType = list[currentIndex].ID;
                }
                _currentProfile.OffensePower = EditorGUILayout.FloatField(_currentProfile.OffensePower);
                _currentProfile.OffenseTime = EditorGUILayout.FloatField(_currentProfile.OffenseTime);
                _currentProfile.OffenseDealTime = EditorGUILayout.FloatField(_currentProfile.OffenseDealTime);
                _currentProfile.OffenseRange = EditorGUILayout.FloatField(_currentProfile.OffenseRange);
                _currentProfile.DefenseType = (DefenseType)EditorGUILayout.EnumPopup(_currentProfile.DefenseType);
                _currentProfile.MovingSpeed = EditorGUILayout.FloatField(_currentProfile.MovingSpeed);

            } GUILayout.EndVertical();
        }

        void DrawSkills()
        {
            if (!IncludeSkillAction()) return;

            GUILayout.BeginVertical(GUILayout.Width(100));
            {
                EditorGUILayout.LabelField("Skills");
                foreach(var skill in _currentProfile.Skills)
                {
                    GUILayout.BeginHorizontal(GUILayout.Width(80));
                    {
                        var prevSkillProfile = _currentBuffCondProfile;
                        string label = string.Format("{0}. ", skill.ID.ToString());
                        if (GUILayout.Toggle(skill == _currentSkillProfile, label, GUILayout.Width(30)))
                            _currentSkillProfile = skill;
                        skill.Type = (SkillType)EditorGUILayout.EnumPopup(skill.Type, GUILayout.Width(100));

                        if (prevSkillProfile != _currentBuffCondProfile)
                        {
                            _currentBuffProfile = null;
                            _currentBuffActionProfile = null;
                            _currentBuffCondProfile = null;
                        }
                    } GUILayout.EndHorizontal();
                }
                CommonEditorUtility.DrawAddData(_currentProfile.Skills, ref _currentSkillProfile);
                CommonEditorUtility.DrawRemoveData(_currentProfile.Skills, ref _currentSkillProfile);
            } GUILayout.EndVertical();
        }

        void DrawSkillParams()
        {
            BattleSkillEditorUtility.DrawParams(_currentSkillProfile);
        }

        void DrawSkillCondition()
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(150));
            {
                EditorGUILayout.LabelField("Skill Conditions");
                foreach (var skillCondition in _currentSkillProfile.Conditions)
                {
                    BattleSkillEditorUtility.DrawConditions(_currentSkillProfile, skillCondition);
                    EditorGUILayout.LabelField("");
                }

                if (GUILayout.Button("Add"))
                {
                    _currentSkillProfile.Conditions.Add(new BattleSkillConditionProfile());
                }
            } EditorGUILayout.EndVertical();
        }

        bool IncludeSkillAction()
        {
            ActorTypeProfile actorTypeProfile = Manager.Data.ActorTypeProfileSave.Get(_currentProfile.ActorType);
            if (actorTypeProfile == null) return false;

            bool doesItIncludeSkillAction = false;
            foreach (var state in actorTypeProfile.States)
            {
                foreach (var action in state.Actions)
                {
                    if (action == ActionType.Skill)
                    {
                        doesItIncludeSkillAction = true;
                        break;
                    }
                }
            }
            return doesItIncludeSkillAction;
        }
    }
}