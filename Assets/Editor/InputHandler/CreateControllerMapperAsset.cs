using UnityEngine;
using System.Collections;
using UnityEditor;

namespace InputHandler
{
    public class CreateControllerMapperAsset
    {
        [MenuItem("InputHandler/Create/ControllerMapper")]
        public static void CreateInputAsset()
        {
            ControllerMapperAsset asset = ControllerMapperAsset.CreateInstance<ControllerMapperAsset>();
            AssetDatabase.CreateAsset(asset, "Assets/ControllerMapper.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }
}