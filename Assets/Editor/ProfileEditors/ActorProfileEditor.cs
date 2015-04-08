using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class ActorProfileEditor : EditorWindow
    {
        ActorProfileSave _save;
        BattleActorProfile _currentProfile;
        BattleEffectProfile _currentBaseOnDealProfile;
        BattleEffectProfile _currentBaseOnHitProfile;
        BattleEffectProfile _currentBaseOnSpotProfile;

        BattleSkillProfile _currentSkillProfile;
        BattleEffectProfile _currentOnSkillEffectProfile;
        BattleEffectProfile _currentOnHitEffectProfile;
        BattleEffectProfile _currentOnSpotEffectProfile;

        BattleBuffProfile _currentBuffProfile;
        BattleBuffActionProfile _currentBuffActionProfile;
        BattleBuffConditionProfile _currentBuffCondProfile;
        BattleEffectProfile _currentBuffEffectProfile;

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

                GUILayout.BeginVertical(GUILayout.Width(300));
                {
                    DrawActorBaseProperties();
                    CommonEditorUtility.DrawHorizontalLine(Color.white);

                    DrawActorBaseEffects();
                } GUILayout.EndVertical();

                GUILayout.BeginVertical();
                {
                    DrawSkills();
                    CommonEditorUtility.DrawHorizontalLine(Color.white);
                    if (_currentSkillProfile == null) return;

                    GUILayout.BeginHorizontal();
                    {
                        DrawSkillParams();
                        DrawSkillCondition();
                    } GUILayout.EndHorizontal();
                    CommonEditorUtility.DrawHorizontalLine(Color.white);

                    GUILayout.BeginHorizontal(GUILayout.Width(600));
                    {
                        DrawSkillEffectsOnSkill();
                        DrawSkillEffectsOnHit();
                        DrawSkillEffectsOnSpot();
                    } GUILayout.EndHorizontal();

                    CommonEditorUtility.DrawHorizontalLine(Color.red);
                    GUILayout.BeginVertical();
                    {
                        DrawBuffs();
                        CommonEditorUtility.DrawHorizontalLine(Color.white);

                        GUILayout.BeginHorizontal();
                        {
                            BattleSkillEditorUtility.DrawBuffActions(_currentBuffProfile, ref _currentBuffActionProfile);
                            BattleSkillEditorUtility.DrawBuffActionParams(_currentBuffProfile, _currentBuffActionProfile);
                        } GUILayout.EndHorizontal();
                        CommonEditorUtility.DrawHorizontalLine(Color.white);

                        GUILayout.BeginHorizontal();
                        {
                            BattleSkillEditorUtility.DrawBuffCondition(_currentBuffProfile, ref _currentBuffCondProfile);
                            BattleSkillEditorUtility.DrawBuffConditionParams(_currentBuffProfile, _currentBuffCondProfile);
                        } GUILayout.EndHorizontal();
                        CommonEditorUtility.DrawHorizontalLine(Color.white);

                        GUILayout.BeginHorizontal();
                        {
                            DrawBuffEffects();
                        } GUILayout.EndHorizontal();
                        CommonEditorUtility.DrawHorizontalLine(Color.white);
                    } GUILayout.EndVertical();
                } GUILayout.EndVertical();

            } GUILayout.EndHorizontal();
        }

        private void DrawBuffs()
        {
            GUILayout.BeginVertical(GUILayout.Width(100));
            {
                EditorGUILayout.LabelField("Buffs");
                bool isChanged = CommonEditorUtility.DrawData(_currentSkillProfile.Buffs, ref _currentBuffProfile, true);
                CommonEditorUtility.DrawAddData(_currentSkillProfile.Buffs, ref _currentBuffProfile);
                CommonEditorUtility.DrawRemoveData(_currentSkillProfile.Buffs, ref _currentBuffProfile);
                if (isChanged)
                {
                    OnBuffProfileChanged();
                }
            } GUILayout.EndVertical();
        }

        void DrawBuffEffects()
        {
            if (_currentBuffProfile == null) return;
            //if (_currentBuffEffectProfile == null) return;
            GUILayout.BeginVertical(GUILayout.Width(200));
            {
                EditorGUILayout.LabelField("Buffs Effects");
                CommonEditorUtility.DrawData(_currentBuffProfile.Effects, ref _currentBuffEffectProfile);
                CommonEditorUtility.DrawAddData(_currentBuffProfile.Effects, ref _currentBuffEffectProfile);
                CommonEditorUtility.DrawRemoveData(_currentBuffProfile.Effects, ref _currentBuffEffectProfile);
            } GUILayout.EndVertical();

            CommonEditorUtility.DrawEffect(_currentBuffEffectProfile);
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
                    OnActorProfileChanged();
                }
            } GUILayout.EndVertical();
        }

        void DrawActorBaseEffects()
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical(GUILayout.Width(200));
                {
                    GUILayout.Label("Base Effects On Deal");
                    CommonEditorUtility.DrawData(_currentProfile.OnDealEffects, ref _currentBaseOnDealProfile);
                    CommonEditorUtility.DrawAddData(_currentProfile.OnDealEffects, ref _currentBaseOnDealProfile);
                    CommonEditorUtility.DrawRemoveData(_currentProfile.OnDealEffects, ref _currentBaseOnDealProfile);
                } GUILayout.EndVertical();

                CommonEditorUtility.DrawEffect(_currentBaseOnDealProfile);
            } GUILayout.EndHorizontal();

            CommonEditorUtility.DrawHorizontalLine(Color.white);
            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical(GUILayout.Width(200));
                {
                    GUILayout.Label("Base Effects On Hit");
                    CommonEditorUtility.DrawData(_currentProfile.OnHitEffects, ref _currentBaseOnHitProfile);
                    CommonEditorUtility.DrawAddData(_currentProfile.OnHitEffects, ref _currentBaseOnHitProfile);
                    CommonEditorUtility.DrawRemoveData(_currentProfile.OnHitEffects, ref _currentBaseOnHitProfile);
                } GUILayout.EndVertical();

                CommonEditorUtility.DrawEffect(_currentBaseOnHitProfile);
            } GUILayout.EndHorizontal();

            CommonEditorUtility.DrawHorizontalLine(Color.white);
            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical(GUILayout.Width(200));
                {
                    GUILayout.Label("Base Effects On Spot");
                    CommonEditorUtility.DrawData(_currentProfile.OnSpotEffects, ref _currentBaseOnSpotProfile);
                    CommonEditorUtility.DrawAddData(_currentProfile.OnSpotEffects, ref _currentBaseOnSpotProfile);
                    CommonEditorUtility.DrawRemoveData(_currentProfile.OnSpotEffects, ref _currentBaseOnSpotProfile);
                } GUILayout.EndVertical();

                CommonEditorUtility.DrawEffect(_currentBaseOnSpotProfile);
            } GUILayout.EndHorizontal();
            CommonEditorUtility.DrawHorizontalLine(Color.white);
        }

        void DrawActorBaseProperties()
        {
            GUILayout.BeginHorizontal();
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
            } GUILayout.EndHorizontal();
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
                        var prevSkillProfile = _currentSkillProfile;
                        string label = string.Format("{0}. ", skill.ID.ToString());
                        if (GUILayout.Toggle(skill == _currentSkillProfile, label, GUILayout.Width(30)))
                            _currentSkillProfile = skill;
                        skill.Type = (SkillType)EditorGUILayout.EnumPopup(skill.Type, GUILayout.Width(100));

                        if (prevSkillProfile != _currentSkillProfile)
                        {
                            OnSkillProfieChanged();
                        }
                    } GUILayout.EndHorizontal();
                }
                CommonEditorUtility.DrawAddData(_currentProfile.Skills, ref _currentSkillProfile);
                CommonEditorUtility.DrawRemoveData(_currentProfile.Skills, ref _currentSkillProfile);
            } GUILayout.EndVertical();
        }

        void DrawSkillEffectsOnSkill()
        {
            GUIStyle style = new GUIStyle();
            //style.normal.background = MakeTex(600, 1, new Color(0.9f, 0.9f, 0.9f));

            GUILayout.BeginHorizontal(style);
            {
                GUILayout.BeginVertical(GUILayout.Width(200));
                {
                    GUILayout.Label("Effects On Skill");
                    CommonEditorUtility.DrawData(_currentSkillProfile.OnSkillEffects, ref _currentOnSkillEffectProfile);
                    CommonEditorUtility.DrawAddData(_currentSkillProfile.OnSkillEffects, ref _currentOnSkillEffectProfile);
                    CommonEditorUtility.DrawRemoveData(_currentSkillProfile.OnSkillEffects, ref _currentOnSkillEffectProfile);
                } GUILayout.EndVertical();

                GUILayout.BeginVertical(GUILayout.Width(200));
                {
                    CommonEditorUtility.DrawEffect(_currentOnSkillEffectProfile);
                } GUILayout.EndVertical();
            } GUILayout.EndHorizontal();
        }

        void DrawSkillEffectsOnHit()
        {
            GUIStyle style = new GUIStyle();
            //style.normal.background = MakeTex(600, 1, new Color(0.8f, 0.8f, 0.8f));

            GUILayout.BeginHorizontal(style);
            {
                GUILayout.BeginVertical(GUILayout.Width(200));
                {
                    GUILayout.Label("Effects On Hit");
                    CommonEditorUtility.DrawData(_currentSkillProfile.OnHitEffects, ref _currentOnHitEffectProfile);
                    CommonEditorUtility.DrawAddData(_currentSkillProfile.OnHitEffects, ref _currentOnHitEffectProfile);
                    CommonEditorUtility.DrawRemoveData(_currentSkillProfile.OnHitEffects, ref _currentOnHitEffectProfile);
                } GUILayout.EndVertical();

                CommonEditorUtility.DrawEffect(_currentOnHitEffectProfile);
            } GUILayout.EndHorizontal();
        }

        void DrawSkillEffectsOnSpot()
        {
            GUIStyle style = new GUIStyle();
            //style.normal.background = MakeTex(600, 1, new Color(0.7f, 0.7f, 0.7f));

            GUILayout.BeginHorizontal(style);
            {
                GUILayout.BeginVertical(GUILayout.Width(200));
                {
                    GUILayout.Label("Effects On Spot");
                    CommonEditorUtility.DrawData(_currentSkillProfile.OnSpotEffects, ref _currentOnSpotEffectProfile);
                    CommonEditorUtility.DrawAddData(_currentSkillProfile.OnSpotEffects, ref _currentOnSpotEffectProfile);
                    CommonEditorUtility.DrawRemoveData(_currentSkillProfile.OnSpotEffects, ref _currentOnSpotEffectProfile);
                } GUILayout.EndVertical();

                CommonEditorUtility.DrawEffect(_currentOnSpotEffectProfile);
            } GUILayout.EndHorizontal();
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
            BattleActorTypeProfile actorTypeProfile = Manager.Data.ActorTypeProfileSave.Get(_currentProfile.ActorType);
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

        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];

            for (int i = 0; i < pix.Length; i++)
                pix[i] = col;

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }

        void OnActorProfileChanged()
        {
            _currentBaseOnDealProfile = null;
            _currentBaseOnHitProfile = null;
            _currentBaseOnSpotProfile = null;

            _currentSkillProfile = null;  
            OnSkillProfieChanged();
        }

        void OnSkillProfieChanged()
        {
            _currentOnHitEffectProfile = null;
            _currentOnSkillEffectProfile = null;
            _currentOnSpotEffectProfile = null;

            _currentBuffProfile = null;
            OnBuffProfileChanged();
        }

        void OnBuffProfileChanged()
        {
            _currentBuffActionProfile = null;
            _currentBuffCondProfile = null;
            _currentBuffEffectProfile = null;
        }
 
    }
}