using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public struct IndexedPosition
    {
        public int X;
        public int Y;

        public IndexedPosition(int y, int x)
        {
            X = x;
            Y = y;
        }
    }
}