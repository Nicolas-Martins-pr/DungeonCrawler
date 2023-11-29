using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public enum Orientation
    {
        Up, Right, Down, Left
    }

    public class Player
    {
        public int posX { get; private set; }
        public int posY { get; private set; }
        public Orientation CurrentOrientation { get; private set; }

        public void SetOriantation(Orientation orientation)
        {
            CurrentOrientation = orientation;
        }
        public void TurnRight()
        {
            CurrentOrientation++;
            if (CurrentOrientation > Orientation.Left) 
                CurrentOrientation = Orientation.Up;
        }
        public void TurnLeft()
        {
            CurrentOrientation--;
            if (CurrentOrientation < Orientation.Up)
                CurrentOrientation = Orientation.Left;
        }
        public void SetPosition(int pX,int pY)
        {
            posX = pX; 
            posY = pY;
        }

        public Player()
        {
            posX = 1; posY = 1;
            CurrentOrientation = Orientation.Down;
        }
    }
}
