using System;
using UnityEngine;

namespace Assets.PixelFantasy.PixelTileEngine.Scripts
{
    public static class TextureHelper
    {
        public static Color[] Flip(Color[] source, int width, int height, bool flipX, bool flipY)
        {
            if (!flipX && !flipY) return source;

            var flipped = new Color[source.Length];

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var dx = flipX ? width - 1 - x : x;
                    var dy = flipY ? height - 1 - y : y;

                    flipped[x + y * width] = source[dx + dy * width];
                }
            }

            return flipped;
        }

        public static Color[] Rotate(Color[] source, int width, int height, int angle)
        {
            if (angle == 0) return source;

            if (width != height) throw new NotImplementedException();

            while (angle < 0) angle += 360;

            switch (angle)
            {
                case 0: return source;
                case 90: return Rotate90(source, width, clockwise: false);
                case 180: return Rotate90(Rotate90(source, width), width);
                case 270: return Rotate90(source, width);
                default: throw new Exception(angle.ToString());
            }
        }

        public static Color[] Rotate90(Color[] source, int size, bool clockwise = true)
        {
            var rotated = new Color[source.Length];

            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    var p = source[x + y * size];

                    if (p.a <= 0) continue;

                    var rx = clockwise ? y : size - 1 - y;
                    var ry = !clockwise ? x : size - 1 - x;

                    rotated[rx + ry * size] = p;
                }
            }

            return rotated;
        }
    }
}