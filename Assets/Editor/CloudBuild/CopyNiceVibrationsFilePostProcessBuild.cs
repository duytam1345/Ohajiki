using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;

public static class CopyNiceVibrationsFilePostProcessBuild
{
    [PostProcessBuild(9999)]
    public static void OnPostProcessBuildCopyNiceVibrationsFile(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            Debug.Log("[CopyNiceVibrationsFilePostProcessBuild] ProcessPostBuild - iOS - Copy MMNViOSCoreHapticsInterface.swift & UnitySwift-Bridging-Header.h.");
            File.Copy(Application.dataPath + "/_ExternalAssets/NiceVibrations/Common/Plugins/iOS/Swift/MMNViOSCoreHapticsInterface.swift", pathToBuiltProject + "/Libraries/_ExternalAssets/NiceVibrations/Common/Plugins/iOS/Swift/MMNViOSCoreHapticsInterface.swift");
            File.Copy(Application.dataPath + "/_ExternalAssets/NiceVibrations/Common/Plugins/iOS/Swift/UnitySwift-Bridging-Header.h", pathToBuiltProject + "/Libraries/_ExternalAssets/NiceVibrations/Common/Plugins/iOS/Swift/UnitySwift-Bridging-Header.h");
        }
    }

}
