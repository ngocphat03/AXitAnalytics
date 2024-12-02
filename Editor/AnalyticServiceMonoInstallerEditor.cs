#if UNITY_EDITOR
namespace AXitUnityTemplate.Analytics.Editor
{
    using UnityEditor;
    using AXitUnityTemplate.Analytics.Runtime.Installers;

    [CustomEditor(typeof(AnalyticServiceMonoInstaller))]
    public class AnalyticServiceMonoInstallerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var script = (AnalyticServiceMonoInstaller)this.target;

            script.isPersistentAcrossScenes = EditorGUILayout.Toggle("Is Persistent Across Scenes", script.isPersistentAcrossScenes);

            EditorGUILayout.HelpBox(
                "Determines whether this GameObject should persist across multiple scenes.\n" +
                "- Enabled: The GameObject will not be destroyed when loading a new scene.\n" +
                "- Disabled: The GameObject will be destroyed when the current scene is unloaded.",
                MessageType.Info
            );
        }
    }
}
#endif