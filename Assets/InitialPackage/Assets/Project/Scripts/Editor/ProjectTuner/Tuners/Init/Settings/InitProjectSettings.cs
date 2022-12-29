using System.Collections.Generic;
using UnityEditor;

namespace Project.Editor.Tuner.Init.Settings
{
    public class InitProjectSettings
    {
        public const string DebugResourcesName = "Settings";

        public static readonly string[] PackagePaths =
        {
            "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask"
        };

        public static readonly string[] InitialScenes =
        {
            "Scenes/Startup",
            "Scenes/UICommon",
            "Scenes/EmptyScene"
        };
        
        public static readonly Dictionary<int, BuildTargetGroup> BuildTargetGroups = new Dictionary<int, BuildTargetGroup>()
        {
            { 0, BuildTargetGroup.Android },
            { 1, BuildTargetGroup.iOS }
        };

        public static readonly Dictionary<int, BuildTarget> BuildTargets = new Dictionary<int, BuildTarget>() 
        {
            { 0, BuildTarget.Android },
            { 1, BuildTarget.iOS }
        };
    }
}