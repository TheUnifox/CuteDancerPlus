#if VRC_SDK_VRCSDK3
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using ExpressionParameters = VRC.SDK3.Avatars.ScriptableObjects.VRCExpressionParameters;

namespace VRF
{
    public class ParameterBuilder : BuilderInterface
    {
        private static Logger log = new Logger("ParameterBuilder");
        public string sourcePath = "";
        public string outputPath = "";
        public string sourceGUID = "";

        public void Build(SettingsBuilderData settings)
        {
            sourcePath = Path.Combine(CuteResources.CUTEDANCER_RUNTIME, "TemplateVRCParams.asset");
            outputPath = Path.Combine(settings.outputDirectory, "CuteDancer-VRCParams.asset");

            if (!AssetDatabase.CopyAsset(sourcePath, outputPath))
            {
                throw new Exception("Error copying template: VRCParams");
            }

            ExpressionParameters expressionParameters = AssetDatabase.LoadAssetAtPath<ExpressionParameters>(outputPath);
            ExpressionParameters templateParameters = AssetDatabase.LoadAssetAtPath<ExpressionParameters>(sourcePath);

            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(templateParameters.GetInstanceID(), out sourceGUID, out long _);

            foreach (ExpressionParameters.Parameter parameter in expressionParameters.parameters)
            {
                if (parameter.name.Contains("{PARAM}"))
                {
                    parameter.name = parameter.name.Replace("{PARAM}", settings.parameterName);
                }
            }

            log.LogInfo("Save file [name = " + outputPath + "]");
            EditorUtility.SetDirty(expressionParameters);
            AssetDatabase.SaveAssets();
        }

    }
}

#endif