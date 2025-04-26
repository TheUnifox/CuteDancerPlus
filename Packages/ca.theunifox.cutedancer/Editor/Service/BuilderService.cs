#if VRC_SDK_VRCSDK3
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using ExpressionParameters = VRC.SDK3.Avatars.ScriptableObjects.VRCExpressionParameters;
using ExpressionsMenu = VRC.SDK3.Avatars.ScriptableObjects.VRCExpressionsMenu;
using UnityEditor.Animations;

namespace VRF
{
    public class BuilderService
    {
        private static Logger log = new Logger("BuilderService");

        ParameterBuilder parameterBuilder = new ParameterBuilder();
        MenuBuilder menuBuilder = new MenuBuilder();
        PrefabBuilder contactsPrefabBuilder = new PrefabBuilder();
        AnimFxOffBuilder animFxOffBuilder = new AnimFxOffBuilder();
        AnimFxOnBuilder animFxOnBuilder = new AnimFxOnBuilder();
        ActionControllerBuilder actionControllerBuilder = new ActionControllerBuilder();
        FxControllerBuilder fxControllerBuilder = new FxControllerBuilder();
        BuildInfoBuilder buildInfoBuilder = new BuildInfoBuilder();

        public void Build(SettingsBuilderData settings)
        {
            float startBuildTime = Time.realtimeSinceStartup;

            if (settings.dances.Count == 0)
            {
                EditorUtility.DisplayDialog("CuteDancer Builder", "Please select at least one dance to proceed.", "OK");
                return;
            }

            Directory.CreateDirectory(settings.outputDirectory);

            parameterBuilder.Build(settings);
            menuBuilder.Build(settings);
            contactsPrefabBuilder.Build(settings);
            animFxOffBuilder.Build(settings);
            animFxOnBuilder.Build(settings);
            actionControllerBuilder.Build(settings);
            fxControllerBuilder.Build(settings);
            buildInfoBuilder.Build(settings);

            FinalizeBuild(settings);

            AssetDatabase.Refresh();

            SettingsService.Instance.SaveFromSettingsBuilderData(settings);

            log.LogInfo($"Build finished in {Time.realtimeSinceStartup - startBuildTime:0.00} seconds");
        }

        public void Rebuild(SettingsBuilderData settings)
        {
            BuildInfoData oldBuildInfo = buildInfoBuilder.GetBuildInfoData(settings.outputDirectory);
            List<BuildInfoData.FilePathGuid> oldFileInfos = oldBuildInfo?.filePathUuids;

            if (oldBuildInfo)
            {
                foreach (var fileInfo in oldBuildInfo.filePathUuids)
                {
                    log.LogDebug($"Delete asset [{fileInfo.path}]");
                    AssetDatabase.DeleteAsset(fileInfo.path);
                }
            }

            Build(settings);
            
            if (oldFileInfos != null)
            {
                log.LogDebug("Restoring GUIDs of previous build.");
                buildInfoBuilder.RestoreGuids(settings.outputDirectory, oldFileInfos);
            }
        }

        private void FinalizeBuild(SettingsBuilderData settings)
        {
            string prefabText = File.ReadAllText(contactsPrefabBuilder.outputPath);

            ExpressionParameters expressionParameters = AssetDatabase.LoadAssetAtPath<ExpressionParameters>(parameterBuilder.outputPath);
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(expressionParameters.GetInstanceID(), out string outputParamsGUID, out long _);
            ExpressionsMenu expressionsMenu = AssetDatabase.LoadAssetAtPath<ExpressionsMenu>(menuBuilder.outputPath);
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(expressionsMenu.GetInstanceID(), out string outputMenuGUID, out long _);
            AnimatorController actionAnimator = AssetDatabase.LoadAssetAtPath<AnimatorController>(actionControllerBuilder.outputPath);
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(actionAnimator.GetInstanceID(), out string outputActionGUID, out long _);
            AnimatorController fxAnimator = AssetDatabase.LoadAssetAtPath<AnimatorController>(fxControllerBuilder.outputPath);
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(fxAnimator.GetInstanceID(), out string outputFXGUID, out long _);

            prefabText = prefabText.Replace(parameterBuilder         .sourcePath.Replace('\\', '/'), parameterBuilder        .outputPath.Replace('\\', '/'));
            prefabText = prefabText.Replace(menuBuilder              .sourcePath.Replace('\\', '/'), menuBuilder             .outputPath.Replace('\\', '/'));
            prefabText = prefabText.Replace(actionControllerBuilder  .sourcePath.Replace('\\', '/'), actionControllerBuilder .outputPath.Replace('\\', '/'));
            prefabText = prefabText.Replace(fxControllerBuilder      .sourcePath.Replace('\\', '/'), fxControllerBuilder     .outputPath.Replace('\\', '/'));
            
            prefabText = prefabText.Replace(parameterBuilder         .sourceGUID, outputParamsGUID);
            prefabText = prefabText.Replace(menuBuilder              .sourceGUID, outputMenuGUID);
            prefabText = prefabText.Replace(actionControllerBuilder  .sourceGUID, outputActionGUID);
            prefabText = prefabText.Replace(fxControllerBuilder      .sourceGUID, outputFXGUID);

            File.WriteAllText(contactsPrefabBuilder.outputPath, prefabText);
        }
    }
}

#endif