using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.PixelFantasy.PixelTileEngine.Scripts
{
    /// <summary>
    /// Scriptable object that automatically grabs all required images.
    /// </summary>
    [CreateAssetMenu(fileName = "SpriteCollection", menuName = "Pixel Environments/SpriteCollection")]
    public class SpriteCollection : ScriptableObject
    {
        public Object TilesFolder;

        public List<Tileset> GroundTilesets;
        public List<Tileset> CoverTilesets;
        public List<Sprite> PropsSprites;
        public List<Sprite> OtherSprites;
        public Sprite DeleteSprite;

        #if UNITY_EDITOR

        public void Refresh()
        {
            GroundTilesets = LoadTextures(TilesFolder, "Ground").Select(i => new Tileset(i)).ToList();
            CoverTilesets = LoadTextures(TilesFolder, "Cover").Select(i => new Tileset(i)).ToList();
            PropsSprites = LoadSprites(TilesFolder, "Props");
            OtherSprites = LoadSprites(TilesFolder, "Ladder").Union(LoadSprites(TilesFolder, "Bridge")).ToList();
            UnityEditor.EditorUtility.SetDirty(this);
            Debug.Log("Refresh done!");
        }

        private static List<Texture2D> LoadTextures(Object folder, string subFolder)
        {
            var files = Directory.GetFiles(UnityEditor.AssetDatabase.GetAssetPath(folder) + $"/{subFolder}", "*.png", SearchOption.TopDirectoryOnly).ToList();
            var textures = files.Select(UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>).ToList();

            return textures;
        }

        private static List<Sprite> LoadSprites(Object folder, string subFolder)
        {
            var files = Directory.GetFiles(UnityEditor.AssetDatabase.GetAssetPath(folder) + $"/{subFolder}", "*.png", SearchOption.TopDirectoryOnly).ToList();
            var sprites = files.SelectMany(UnityEditor.AssetDatabase.LoadAllAssetsAtPath).OfType<Sprite>().ToList();

            return sprites;
        }

        #endif
    }
}