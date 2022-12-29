using System;
using System.Linq;
using Project.Meta;
using UnityEngine;
using Zenject;

namespace Project.Settings
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Scriptable/LevelSettings", order = 0)]
    public class LevelSettings : ScriptableObject
    {
        [Serializable]
        public class FinishCoinPreset
        {
            [field: SerializeField]
            public IntRange LevelIndex
            {
                get;
                private set;
            }

            [field: SerializeField]
            public int Coin
            {
                get;
                private set;
            }
        }
        
#if UNITY_EDITOR
        [SerializeField, Header("Test Group")]
        private bool _isTestSceneEnabled = false;

        [SerializeField, EnabledIf(nameof(_isTestSceneEnabled), true, EnabledIfAttribute.HideMode.Invisible)]
        private string _testSceneName = string.Empty;
#endif
        
        [SerializeField, Header("Main Group")]
        private string _tutorialSceneName = string.Empty;

        [SerializeField]
        private string[] _levels = null;
        
        [SerializeField]
        private string[] _loopedLevels = null;

        [Header("Finish")]
        [SerializeField]
        private FinishCoinPreset[] _coinPresets = null;
        
        [field: SerializeField]
        public float ResultDelay
        {
            get;
            private set;
        }
        
        [field: SerializeField]
        public float FailDelay
        {
            get;
            private set;
        }

        [InjectOptional]
        private ILevelData _levelData;
        
        public string GetScene
        {
            get
            {
#if UNITY_EDITOR
                if (_isTestSceneEnabled)
                {
                    return _testSceneName;
                }
#endif
                int levelIndex = _levelData.LevelIndex;

                if (levelIndex == 0)
                {
                    return _tutorialSceneName;
                }
                else
                {
                    // NOTE: учитываем туториал
                    levelIndex -= 1;
                }

                if (levelIndex < _levels.Length)
                {
                    return _levels[levelIndex % _levels.Length];
                }
                else
                {
                    levelIndex -= _levels.Length;

                    return _loopedLevels[levelIndex % _loopedLevels.Length];
                }
            }
        }
        
        public int CompleteCoinCount
        {
            get
            {
                var coinPreset = _coinPresets.FirstOrDefault(pr => pr.LevelIndex.InRange(_levelData.LevelIndex - 1));
                if (coinPreset == null)
                {
                    coinPreset = _coinPresets[_coinPresets.Length - 1];
                }

                return coinPreset.Coin;
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            string sceneFormat = "{0}.unity";
            int defaultSceneCount = 2;
            
            var scenesGUIDs = UnityEditor.AssetDatabase.FindAssets("t:Scene");
            var scenesPaths = scenesGUIDs.Select(UnityEditor.AssetDatabase.GUIDToAssetPath);

            System.Collections.Generic.List<string> scenesToAdd = new System.Collections.Generic.List<string>();
            System.Collections.Generic.List<UnityEditor.EditorBuildSettingsScene> buildScenes =
                new System.Collections.Generic.List<UnityEditor.EditorBuildSettingsScene>();

            for (int i = 0; i < defaultSceneCount; i++)
            {
                buildScenes.Add(UnityEditor.EditorBuildSettings.scenes[i]);
            }

            void process(string[] levels)
            {
                foreach (var level in levels)
                {
                    foreach (var scene in scenesPaths)
                    {
                        if (scene.EndsWith(string.Format(sceneFormat, level)) && !scenesToAdd.Contains(scene))
                        {
                            scenesToAdd.Add(scene);
                        }
                    }
                }
                
                for (int i = 0; i < scenesToAdd.Count; i++)
                {
                    if (buildScenes.Select(bs => bs.path).All(bs => !bs.Contains(scenesToAdd[i])))
                    {
                        buildScenes.Add(new UnityEditor.EditorBuildSettingsScene(scenesToAdd[i], true));
                    }
                    else
                    {
                        int index = -1;

                        for (int j = 0; j < buildScenes.Count; j++)
                        {
                            if (buildScenes[j].path.Equals(scenesToAdd[i]))
                            {
                                index = j;
                            
                                break;
                            }
                        }

                        if (index != -1)
                        {
                            var editorScene = buildScenes[index];

                            if (!buildScenes.Contains(editorScene))
                            {
                                buildScenes.Add(editorScene);
                            }
                        }
                    }
                }
            }
            
            process(new []{ _tutorialSceneName });
            process(_levels);
            process(_loopedLevels);
            UnityEditor.EditorBuildSettings.scenes = buildScenes.ToArray();
        }
#endif
    }
}