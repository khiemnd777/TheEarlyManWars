using UnityEditor;

internal class EnablePSDRemoveMatte : AssetPostprocessor
{
    private void OnPreprocessTexture()
    {
        if (assetPath.Contains(".psd"))
        {
            var textureImporter = (TextureImporter)assetImporter;
            var serializedObject = new SerializedObject(textureImporter);
            serializedObject.FindProperty("m_PSDRemoveMatte").boolValue = true;
            serializedObject.ApplyModifiedProperties();
        }
    }
}