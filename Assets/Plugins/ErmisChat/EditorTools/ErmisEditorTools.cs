using System;
using System.Linq;
using System.Text;
using ErmisChat.EditorTools.CommandLineParsers;
using Ermis.Core.LowLevelClient;
using ErmisChat.EditorTools.Builders;
using ErmisChat.EditorTools.DefineSymbols;
using UnityEditor;
using UnityEngine;
#if UNITY_ANDROID
using UnityEditor.Android;
#endif

namespace ErmisChat.EditorTools
{
    public static class ErmisEditorTools
    {
        [MenuItem(MenuPrefix + "Toggle " + ErmisTestsEnabledCompilerFlag + " compiler flag")]
        public static void ToggleErmisTestsEnabledCompilerFlag() => ToggleCompilerFlag(ErmisTestsEnabledCompilerFlag);

        [MenuItem(MenuPrefix + "Toggle " + ErmisDebugModeEnabledCompilerFlag + " compiler flag")]
        public static void ToggleErmisDebugModeCompilerFlag()
            => ToggleCompilerFlag(ErmisDebugModeEnabledCompilerFlag);

        [MenuItem(MenuPrefix + "Open " + nameof(SpriteAtlasUtilityEditor))]
        public static void ShowSpriteAtlasUtilityEditorWindow()
        {
            var window = EditorWindow.GetWindow<SpriteAtlasUtilityEditor>();
            window.Show();
        }

        [MenuItem(MenuPrefix + "Open " + nameof(ErmisPackageExportEditor))]
        public static void ShowErmisPackageExportEditorWindow()
        {
            var window = EditorWindow.GetWindow<ErmisPackageExportEditor>();
            window.Show();
        }

        public static void BuildSampleApp()
        {
            var parser = new BuildSettingsCommandLineParser();
            var builder = new ErmisAppBuilder();

            var (buildSettings, authCredentials) = parser.Parse();

            var report = builder.BuildSampleApp(buildSettings, authCredentials);
            EditorApplication.Exit(report.summary.totalErrors > 0 ? 1 : 0);
        }

        public static void PrintAndroidExternalToolsInfo()
        {
            Debug.Log($"Called {nameof(PrintAndroidExternalToolsInfo)}");

#if !UNITY_ANDROID
            Debug.LogError("Enable Android platform in order to print Android external tools info.");
            Application.Quit(1);
#else
            var sb = new StringBuilder();
            sb.AppendLine(
                $"{nameof(AndroidExternalToolsSettings.sdkRootPath)}: {AndroidExternalToolsSettings.sdkRootPath}");
            sb.AppendLine(
                $"{nameof(AndroidExternalToolsSettings.jdkRootPath)}: {AndroidExternalToolsSettings.jdkRootPath}");
            sb.AppendLine(
                $"{nameof(AndroidExternalToolsSettings.ndkRootPath)}: {AndroidExternalToolsSettings.ndkRootPath}");
            sb.AppendLine(
                $"{nameof(AndroidExternalToolsSettings.gradlePath)}: {AndroidExternalToolsSettings.gradlePath}");
            Debug.Log(sb.ToString());
            Application.Quit(0);
#endif
        }

        public static void SetAndroidExternalTools()
        {
            Debug.Log($"Called {nameof(SetAndroidExternalTools)}");

#if !UNITY_ANDROID
            Debug.LogError("Enable Android platform in order to print Android external tools info.");
            Application.Quit(1);
#else
            try
            {
                var sb = new StringBuilder();
                var parser = new AndroidExternalToolsCommandLineParser();
                var androidExternalToolsSettings = parser.Parse();

                if (!string.IsNullOrEmpty(androidExternalToolsSettings.AndroidSdkPath))
                {
                    sb.AppendLine(
                        $"Setting {nameof(AndroidExternalToolsSettings.sdkRootPath)} to: {androidExternalToolsSettings.AndroidSdkPath}");
                    AndroidExternalToolsSettings.sdkRootPath = androidExternalToolsSettings.AndroidSdkPath;
                }

                if (!string.IsNullOrEmpty(androidExternalToolsSettings.JdkPath))
                {
                    sb.AppendLine(
                        $"Setting {nameof(AndroidExternalToolsSettings.jdkRootPath)} to: {androidExternalToolsSettings.JdkPath}");
                    AndroidExternalToolsSettings.jdkRootPath = androidExternalToolsSettings.JdkPath;
                }

                if (!string.IsNullOrEmpty(androidExternalToolsSettings.AndroidNdkPath))
                {
                    sb.AppendLine(
                        $"Setting {nameof(AndroidExternalToolsSettings.ndkRootPath)} to: {androidExternalToolsSettings.AndroidNdkPath}");
                    AndroidExternalToolsSettings.ndkRootPath = androidExternalToolsSettings.AndroidNdkPath;
                }

                if (!string.IsNullOrEmpty(androidExternalToolsSettings.GradlePath))
                {
                    sb.AppendLine(
                        $"Setting {nameof(AndroidExternalToolsSettings.gradlePath)} to: {androidExternalToolsSettings.GradlePath}");
                    AndroidExternalToolsSettings.gradlePath = androidExternalToolsSettings.GradlePath;
                }

                Debug.Log(sb.ToString());
                Application.Quit(0);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
#endif
        }

        public static void EnableErmisTestsEnabledCompilerFlag()
            => SetErmisTestsEnabledCompilerFlag(ErmisTestsEnabledCompilerFlag, true);

        private static void ToggleCompilerFlag(string flagKeyword)
        {
            var unityDefineSymbols = new UnityDefineSymbolsFactory().CreateDefault();

            var activeBuildTarget = EditorUserBuildSettings.activeBuildTarget;

            var symbols = unityDefineSymbols.GetScriptingDefineSymbols(activeBuildTarget).ToList();

            var nextState = !symbols.Contains(flagKeyword);

            SetErmisTestsEnabledCompilerFlag(flagKeyword, nextState);
        }

        public static void SetErmisTestsEnabledCompilerFlag(string flagKeyword, bool enabled)
        {
            var unityDefineSymbols = new UnityDefineSymbolsFactory().CreateDefault();

            var activeBuildTarget = EditorUserBuildSettings.activeBuildTarget;

            var symbols = unityDefineSymbols.GetScriptingDefineSymbols(activeBuildTarget).ToList();

            var prevCombined = string.Join(", ", symbols);

            if (enabled && !symbols.Contains(flagKeyword))
            {
                symbols.Add(flagKeyword);
            }

            if (!enabled && symbols.Contains(flagKeyword))
            {
                symbols.Remove(flagKeyword);
            }

            var currentCombined = string.Join(", ", symbols);

            unityDefineSymbols.SetScriptingDefineSymbols(activeBuildTarget, symbols);

            Debug.Log(
                $"Editor scripting define symbols have been modified from: `{prevCombined}` to: `{currentCombined}` for named build target: `{Enum.GetName(typeof(BuildTarget), activeBuildTarget)}`");
        }

        private const string MenuPrefix = "Tools/" + ErmisChatLowLevelClient.MenuPrefix;

        private const string ErmisTestsEnabledCompilerFlag = "ERMIS_TESTS_ENABLED";
        private const string ErmisDebugModeEnabledCompilerFlag = "ERMIS_DEBUG_ENABLED";
    }
}