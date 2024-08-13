using UnityEngine;

namespace Assets.PixelFantasy.PixelTileEngine.Scripts
{
    public class TileMap
    {
        private Block[,,] _map;

        public int Width => _map.GetLength(0);
        public int Height => _map.GetLength(1);
        public int Depth => _map.GetLength(2);

        public TileMap(int width, int height, int depth)
        {
            _map = new Block[width, height, depth];
        }

        public Block this[int x, int y, int z]
        {
            get => x >= 0 && x < Width && y >= 0 && y < Height && z >= 0 && z < Depth ? _map[x, y, z] : null;
            set => _map[x, y, z] = value;
        }

        public void ExtendMap(Position position)
        {
            var xMin = Mathf.Min(position.X, 0);
            var xMax = Mathf.Max(position.X, Width - 1);
            var yMin = Mathf.Min(position.Y, 0);
            var yMax = Mathf.Max(position.Y, Height - 1);
            var mapEx = new Block[xMax - xMin + 1, yMax - yMin + 1, Depth];

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    for (var z = 0; z < Depth; z++)
                    {
                        mapEx[x - xMin, y - yMin, z] = _map[x, y, z];
                    }
                }
            }

            _map = mapEx;
        }

        public void Destroy(int x, int y, int z)
        {
            if (_map[x, y, z] != null) Object.Destroy(_map[x, y, z].GameObject);

            _map[x, y, z] = null;
        }

        public int[,] GetBitmap(int x, int y, int z)
        {
            var name = this[x, y, z].Name;
            var map = new int[Width, Height];

            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    map[i, j] = _map[i, j, z] != null && _map[i, j, z].Name == name ? 1 : 0;
                }
            }

            return map;
        }
    }
}