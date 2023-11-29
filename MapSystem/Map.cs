using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.MapSystem
{

    public class Map
    {
        public int[,] mapWalls;
        public int height = 12, width = 12;

        public readonly Point[][][] view;
        public Map()
        {
            mapWalls = new int[,]{
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
                { 1, 0, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1 },
                { 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1 },
                { 1, 1, 0, 1, 1, 1, 0, 1, 0, 0, 0, 1 },
                { 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1 },
                { 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1 },
                { 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 0, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
            };

            view = new Point[4][][];

            //up
            int o = (int)Orientation.Up;
            view[o] = new Point[4][];
            view[o][0] = new Point[] { new Point(-1, 0), new Point(0, 0), new Point(1, 0) };
            view[o][1] = new Point[] { new Point(-1, -1), new Point(0, -1), new Point(-1, -1) };
            view[o][2] = new Point[] { new Point (-2,-2), new Point(-1, -2), new Point(0,-2), new Point(1,-2), new Point(2,-2)};
            view[o][3] = new Point[] { new Point (-2,-3), new Point(-1, -3), new Point(0,-3), new Point(1,-3), new Point(2,-3)};
            
            //right
            o = (int)Orientation.Right;
            view[o] = new Point[4][];
            view[o][0] = new Point[] { new Point(0, -1), new Point(0, 0), new Point(0, 1) };
            view[o][1] = new Point[] { new Point(1, -1), new Point(1, 0), new Point(1, 1) };
            view[o][2] = new Point[] { new Point (2,-2), new Point(2, -1), new Point(2,0), new Point(2,1), new Point(2,2)};
            view[o][3] = new Point[] { new Point (3,-2), new Point(3, -1), new Point(3,0), new Point(3,1), new Point(3,2)};

            //down
            o = (int)Orientation.Down;
            view[o] = new Point[4][];
            view[o][0] = new Point[] { new Point(1, 0), new Point(0, 0), new Point(-1, 0) };
            view[o][1] = new Point[] { new Point(1, 1), new Point(0, 1), new Point(-1, 1) };
            view[o][2] = new Point[] { new Point(2, 2), new Point(1, 2), new Point(0, 2), new Point(-1, 2), new Point(-2, 2) };
            view[o][3] = new Point[] { new Point(2, 3), new Point(1, 3), new Point(0, 3), new Point(-1, 3), new Point(-2, 3) };

            o = (int)Orientation.Left;
            view[o] = new Point[4][];
            view[o][0] = new Point[] { new Point(0, 1), new Point(0, 0), new Point(0, -1) };
            view[o][1] = new Point[] { new Point(-1, 1), new Point(-1, 0), new Point(-1, -1) };
            view[o][2] = new Point[] { new Point(-2, 2), new Point(-2, 1), new Point(-2, 0), new Point(-2, -1), new Point(-2, -2) };
            view[o][3] = new Point[] { new Point(-3, 2), new Point(-3, 1), new Point(-3, 0), new Point(-3, -1), new Point(-3, -2) };

        }
        public int GetWallAt(int column, int line)
        {
            if (column >= 0 && column < width && line >= 0 && line < height)
            {
                return mapWalls[line, column];
            }
            else
                return 0;
        }
    }
}
