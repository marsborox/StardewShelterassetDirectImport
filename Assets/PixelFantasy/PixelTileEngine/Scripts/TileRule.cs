namespace Assets.PixelFantasy.PixelTileEngine.Scripts
{
    public class TileRule
    {
        internal readonly int Id;
        internal int[] Pattern;
        internal readonly bool Rotate;
        internal readonly bool Flip;
        internal readonly bool RandomFlip;

        internal TileRule(int id, int[] pattern, bool rotate = false, bool flip = false, bool randomFlip = false)
        {
            Id = id;
            Pattern = pattern;
            Rotate = rotate;
            Flip = flip;
            RandomFlip = randomFlip;
        }
    }
}