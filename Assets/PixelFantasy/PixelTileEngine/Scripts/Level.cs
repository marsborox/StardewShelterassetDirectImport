using System.Collections.Generic;

namespace Assets.PixelFantasy.PixelTileEngine.Scripts
{
    public class Level
    {
        public List<string> TileTable;
        public int[,,] GroundMap;
        public int[,,] CoverMap;
        public int[,,] PropsMap;

        public Level(int width, int height, int depth)
        {
            TileTable = new List<string>();
            GroundMap = new int[width, height, depth];
            CoverMap = new int[width, height, depth];
            PropsMap = new int[width, height, depth];
        }

        public int AddTexture(string textureName)
        {
            var index = TileTable.IndexOf(textureName);

            if (index != -1) return index;

            TileTable.Add(textureName);
                
            return TileTable.Count - 1;
        }
    }
}