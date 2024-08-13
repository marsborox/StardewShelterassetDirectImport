namespace Assets.PixelFantasy.PixelTileEngine.Scripts
{
    public class TileMeta
    {
        public string Id;
        //public Rect Rect;
        public int[,] Mask;
        public int Weight;
        public bool FlipX;

        public TileMeta(string id, int[,] mask, int weight = 0, bool flipX = false)
        {
            Id = id;
            Mask = mask;
            Weight = weight;
            FlipX = flipX;
        }

        public bool Match(int[,] mask, bool flipX = false)
        {
            if (Mask.GetLength(0) != mask.GetLength(0) || Mask.GetLength(1) != mask.GetLength(1)) return false;

            var width = Mask.GetLength(0);
            var height = Mask.GetLength(1);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    if (Mask[x, y] != mask[x, flipX ? height - 1 - y : y]) return false;
                }
            }

            return true;
        }
    }
}