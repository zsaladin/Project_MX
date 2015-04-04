using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class UserRecordEditor : EditorWindow
    {
        UserRecordSave _save;
        UserRecord _currentRecord;

        Vector2 _actorScroll;
        ActorRecord _currentActorRecord;

        [MenuItem("Custom/Record/User")]
        static public void CreateUserRecordWindow()
        {
            EditorWindow.GetWindow<UserRecordEditor>();
        }

        void Init()
        {
            if (_save == null)
                _save = ScriptableObjectUtility.GetAsset<UserRecordSave>(DataType.Record);
        }

        void OnGUI()
        {
            Init();

            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical(GUILayout.Width(200));
                {
                    bool isChagned = CommonEditorUtility.DrawData(_save.UserRecords, ref _currentRecord);
                    CommonEditorUtility.DrawAddData(_save.UserRecords, ref _currentRecord);
                    CommonEditorUtility.DrawRemoveData(_save.UserRecords, ref _currentRecord);
                    CommonEditorUtility.DrawSaveData(_save);

                    if (isChagned)
                    {
                        _currentActorRecord = null;
                        _actorScroll = Vector2.zero;
                    }
                } GUILayout.EndVertical();

                if (_currentRecord == null) return;


                GUILayout.BeginVertical();
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Name", GUILayout.Width(150));
                        _currentRecord.Name = EditorGUILayout.TextField(_currentRecord.Name, GUILayout.Width(150));
                    } GUILayout.EndHorizontal();



                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.BeginVertical(GUILayout.Width(150));
                        {
                            _actorScroll = GUILayout.BeginScrollView(_actorScroll, GUILayout.Width(150), GUILayout.Height(100));
                            {
                                CommonEditorUtility.DrawData(_currentRecord.ActorRecords, ref _currentActorRecord);
                            } GUILayout.EndScrollView();
                            CommonEditorUtility.DrawAddData(_currentRecord.ActorRecords, ref _currentActorRecord);
                            CommonEditorUtility.DrawRemoveData(_currentRecord.ActorRecords, ref _currentActorRecord);
                        } GUILayout.EndVertical();


                        if (_currentActorRecord != null)
                        {
                            GUILayout.BeginVertical(GUILayout.Width(150));
                            {
                                GUILayout.Label("Type");
                                GUILayout.Label("Position");
                            } GUILayout.EndVertical();

                            GUILayout.BeginVertical(GUILayout.Width(150));
                            {
                                ActorProfile profile = Manager.Data.ActorProfileSave.Get(_currentActorRecord.ProfileID);
                                int actorIndex = Manager.Data.ActorProfileSave.ActorProfiles.IndexOf(profile);
                                string[] actorProfileNames = Manager.Data.ActorProfileSave.ActorProfiles.Select(item => item.Name).ToArray();

                                actorIndex = EditorGUILayout.Popup(actorIndex, actorProfileNames);
                                if (actorIndex >= 0)
                                {
                                    profile = Manager.Data.ActorProfileSave.ActorProfiles[actorIndex];
                                    _currentActorRecord.ProfileID = profile.ID;
                                    _currentActorRecord.Name = profile.Name;
                                }

                                _currentActorRecord.Position = EditorGUILayout.Vector3Field("Vector3", _currentActorRecord.Position);
                            } GUILayout.EndVertical();
                        }
                    } GUILayout.EndHorizontal();
                } GUILayout.EndVertical();
            } GUILayout.EndHorizontal();
        }
    }
}