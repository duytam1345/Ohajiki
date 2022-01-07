using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;

public static class EnableBitcodePostProcessBuild
{
    [PostProcessBuild(100)]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {
        // Only perform these steps for iOS builds
        if (buildTarget == BuildTarget.iOS)
        {
            ProcessPostBuildIOS(buildTarget, path);
        }
    }

    [System.Diagnostics.Conditional("ENABLE_BITCODE_NO")]
    private static void ProcessPostBuildIOS(BuildTarget buildTarget, string path)
    {

        Debug.Log("[EnableBitcodePostProcessBuild] ProcessPostBuild - iOS - ENABLE_BITCODE = NO.");

        // get XCode project path
        string pbxPath = PBXProject.GetPBXProjectPath(path);

        // Add linked frameworks
        PBXProject pbxProject = new PBXProject();
        pbxProject.ReadFromString(File.ReadAllText(pbxPath));
        string[] targetGUIDs =
        {
#if UNITY_2019_3_OR_NEWER
            pbxProject.GetUnityFrameworkTargetGuid()
            ,pbxProject.GetUnityMainTargetGuid()
#else
            pbxProject.TargetGuidByName(PBXProject.GetUnityTargetName())
#endif
        };
        foreach (var targetGUID in targetGUIDs)
        {
            pbxProject.SetBuildProperty(targetGUID, "ENABLE_BITCODE", "NO");
        }
        
        File.WriteAllText(pbxPath, pbxProject.WriteToString());

    }
}
