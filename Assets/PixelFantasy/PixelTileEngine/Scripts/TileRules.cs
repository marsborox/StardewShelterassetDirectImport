using System.Collections.Generic;

namespace Assets.PixelFantasy.PixelTileEngine.Scripts
{
    public static class TileRules
    {
        // The tile [mask] describes tile connections and is used for resolving tiles automatically.
        // For example, the following [mask] describes a block that has 2 neighbors, at the left and at the right.
        // 0 0 0
        // 1 1 1 
        // 0 0 0
        // [flipX] allows sprite to be flipped.
        // [weight] is used to pick a random tile when multiple tiles match the same rule.
        public static List<TileMeta> Ground = new List<TileMeta>
        {
            new TileMeta("0", new[,] { { 0, 0, 0 }, { 0, 1, 1 }, { 0, 1, 1 } }, flipX: true),
            new TileMeta("1", new[,] { { 0, 0, 0 }, { 1, 1, 1 }, { 1, 1, 1 } }),
            new TileMeta("2", new[,] { { 0, 0, 0 }, { 1, 1, 1 }, { 1, 1, 1 } }),
            
            new TileMeta("3", new[,] { { 1, 1, 0 }, { 1, 1, 1 }, { 0, 1, 1 } }, flipX: true),
            new TileMeta("4", new[,] { { 0, 1, 0 }, { 1, 1, 1 }, { 1, 1, 0 } }, flipX: true),
            new TileMeta("5", new[,] { { 0, 1, 0 }, { 1, 1, 1 }, { 1, 1, 1 } }),
            new TileMeta("6", new[,] { { 0, 0, 0 }, { 1, 1, 1 }, { 0, 1, 0 } }),
            new TileMeta("7", new[,] { { 0, 0, 0 }, { 1, 1, 0 }, { 0, 1, 0 } }, flipX: true),

            new TileMeta("8", new[,] { { 0, 1, 1 }, { 0, 1, 1 }, { 0, 1, 1 } }, flipX: true),
            new TileMeta("9", new[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } }, weight: 4),
            new TileMeta("10", new[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } }, weight: 1),
            new TileMeta("11", new[,] { { 0, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } }, flipX: true),
            new TileMeta("12", new[,] { { 1, 1, 0 }, { 1, 1, 1 }, { 0, 1, 0 } }, flipX: true),
            new TileMeta("13", new[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 0, 1, 0 } }),
            new TileMeta("14", new[,] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 0, 0 } }),
            new TileMeta("15", new[,] { { 0, 1, 0 }, { 1, 1, 0 }, { 0, 0, 0 } }, flipX: true),

            new TileMeta("16", new[,] { { 0, 1, 1 }, { 0, 1, 1 }, { 0, 0, 0 } }, flipX: true),
            new TileMeta("17", new[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 0, 0, 0 } }),
            new TileMeta("18", new[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 0, 1, 1 } }, flipX: true),
            new TileMeta("19", new[,] { { 1, 1, 0 }, { 1, 1, 1 }, { 1, 1, 0 } }, flipX: true),
            new TileMeta("20", new[,] { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } }),
            new TileMeta("21", new[,] { { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 0 } }),
            new TileMeta("22", new[,] { { 0, 1, 0 }, { 0, 1, 1 }, { 0, 1, 1 } }, flipX: true),
            new TileMeta("23", new[,] { { 0, 0, 0 }, { 1, 1, 1 }, { 1, 1, 0 } }, flipX: true),

            new TileMeta("24", new[,] { { 0, 0, 0 }, { 0, 1, 1 }, { 0, 0, 0 } }, flipX: true),
            new TileMeta("25", new[,] { { 0, 0, 0 }, { 1, 1, 1 }, { 0, 0, 0 } }),
            new TileMeta("26", new[,] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 1, 0 } }),
            new TileMeta("27", new[,] { { 0, 1, 0 }, { 1, 1, 0 }, { 0, 1, 0 } }, flipX: true),
            new TileMeta("28", new[,] { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 1, 0 } }),
            new TileMeta("29", new[,] { { 0, 1, 0 }, { 0, 1, 0 }, { 0, 0, 0 } }),
            new TileMeta("30", new[,] { { 0, 1, 1 }, { 0, 1, 1 }, { 0, 1, 0 } }, flipX: true),
            new TileMeta("31", new[,] { { 1, 1, 0 }, { 1, 1, 1 }, { 0, 0, 0 } }, flipX: true)
        };

        public static List<TileMeta> Cover = new List<TileMeta>
        {
            new TileMeta("2", new[,] { { 0, 0, 0 }, { 2, 1, 2 }, { 0, 0, 0 } }),
            new TileMeta("1", new[,] { { 0, 0, 0 }, { 0, 1, 2 }, { 0, 0, 0 } }, flipX: true),
            new TileMeta("5", new[,] { { 0, 0, 0 }, { 1, 1, 2 }, { 0, 0, 0 } }, flipX: true),
            new TileMeta("3", new[,] { { 0, 0, 0 }, { 0, 1, 1 }, { 0, 0, 0 } }, flipX: true),
            new TileMeta("4", new[,] { { 0, 0, 0 }, { 1, 1, 1 }, { 0, 0, 0 } }),
            new TileMeta("0", new[,] { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } })
        };
    }
}