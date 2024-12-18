using System;
using System.Collections;
using System.Collections.Generic;

namespace Loyufei 
{
    public struct Offset2
    {
        public Offset2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public static implicit operator Offset2Int(Offset2 offset)
        {
            return new Offset2Int((int)offset.X, (int)offset.Y);
        }

        public static implicit operator Offset3(Offset2 offset)
        {
            return new Offset3(offset.X, offset.Y, 0);
        }

        public static implicit operator Offset3Int(Offset2 offset)
        {
            return new Offset3Int((int)offset.X, (int)offset.Y, 0);
        }
    }

    public struct Offset2Int
    {
        public Offset2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public static implicit operator Offset2(Offset2Int offset) 
        {
            return new Offset2(offset.X, offset.Y);
        }

        public static implicit operator Offset3(Offset2Int offset)
        {
            return new Offset3(offset.X, offset.Y, 0);
        }

        public static implicit operator Offset3Int(Offset2Int offset)
        {
            return new Offset3Int(offset.X, offset.Y, 0);
        }
    }

    public struct Offset3 
    {
        public Offset3(float x, float y, float z) 
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public static implicit operator Offset2(Offset3 offset)
        {
            return new Offset2(offset.X, offset.Y);
        }

        public static implicit operator Offset2Int(Offset3 offset)
        {
            return new Offset2Int((int)offset.X, (int)offset.Y);
        }

        public static implicit operator Offset3Int(Offset3 offset)
        {
            return new Offset3Int((int)offset.X, (int)offset.Y, (int)offset.Z);
        }
    }

    public struct Offset3Int
    {
        public Offset3Int(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public static implicit operator Offset2(Offset3Int offset)
        {
            return new Offset2(offset.X, offset.Y);
        }

        public static implicit operator Offset2Int(Offset3Int offset)
        {
            return new Offset2Int(offset.X, offset.Y);
        }

        public static implicit operator Offset3(Offset3Int offset)
        {
            return new Offset3Int(offset.X, offset.Y, offset.Z);
        }
    }
}
