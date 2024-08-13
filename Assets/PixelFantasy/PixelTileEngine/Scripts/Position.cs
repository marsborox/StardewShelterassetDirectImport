using UnityEngine;

namespace Assets.PixelFantasy.PixelTileEngine.Scripts
{
    public struct Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position(float x, float y)
        {
            X = Mathf.RoundToInt(x);
            Y = Mathf.FloorToInt(y);
        }

        public static Position FromPointer(Vector2 pointer)
        {
            var position = Camera.main.ScreenToWorldPoint(pointer);

            return new Position(position.x, position.y);
        }

        public float Distance(Position other)
        {
            return Mathf.Sqrt(Mathf.Pow(X - other.X, 2) + Mathf.Pow(Y - other.Y, 2));
        }

        public static bool operator ==(Position a, Position b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Position a, Position b)
        {
            return !a.Equals(b);
        }

        public static Position operator +(Position a, Position b)
        {
            return new Position(a.X + b.X, a.Y + b.Y);
        }

        public static Position operator -(Position a, Position b)
        {
            return new Position(a.X - b.X, a.Y - b.Y);
        }

        public static Position operator *(Position a, int b)
        {
            return new Position(a.X * b, a.Y * b);
        }

        public override string ToString()
        {
            return $"{X}:{Y}";
        }

        public bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object other)
        {
            return other is Position position && position.Equals(this);
        }

        public override int GetHashCode()
        {
            return X + Y;
        }
    }
}