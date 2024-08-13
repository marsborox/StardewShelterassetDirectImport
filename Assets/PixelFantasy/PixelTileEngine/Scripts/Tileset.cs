using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.PixelFantasy.PixelTileEngine.Scripts
{
    [Serializable]
    public class Tileset
    {
        public string Name;
        public Texture2D Texture;
        public List<Sprite> Sprites;

        public List<TileMeta> Meta => Sprites.Count == 32 ? TileRules.Ground : TileRules.Cover;

        #if UNITY_EDITOR

        public Tileset(Texture2D texture)
        {
            Name = texture.name;
            Texture = texture;
            Sprites = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(UnityEditor.AssetDatabase.GetAssetPath(Texture)).OfType<Sprite>().OrderBy(i => int.Parse(i.name)).ToList();
        }

        #endif

        public Tileset(Texture2D texture, List<Sprite> sprites)
        {
            Name = texture.name;
            Texture = texture;
            Sprites = sprites.OrderBy(i => int.Parse(i.name)).ToList();
        }

        public Sprite ResolveSprite(int[,] mask, out bool flipX)
        {
            var index = -1;
            var matches = Meta.Where(i => i.Match(mask)).ToList();

            if (matches.Any())
            {
                index = Meta.IndexOf(PickRandom(matches));
            }

            flipX = false;

            if (index == -1)
            {
                matches = Meta.Where(i => i.Match(mask, flipX: true)).ToList();

                if (matches.Any())
                {
                    index = Meta.IndexOf(PickRandom(matches));
                    flipX = true;
                }
            }

            if (index == -1)
            {
                Debug.LogWarning("Not found: " + JsonConvert.SerializeObject(mask));
            }
            else
            {
                //Debug.Log("Resolved: " + JsonConvert.SerializeObject(mask));
            }

            return index == -1 ? null : Sprites.Single(i => i.name == Meta[index].Id);
        }

        private TileMeta PickRandom(List<TileMeta> tiles)
        {
            if (tiles.Count == 1) return tiles[0];

            var rand = Random.Range(0, tiles.Sum(i => i.Weight));
            var state = 0;

            foreach (var tile in tiles)
            {
                state += tile.Weight;

                if (rand < state) return tile;
            }

            return tiles[Random.Range(0, tiles.Count)];
        }
    }
}