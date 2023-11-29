using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Module;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.MapSystem
{
     class Map25DView : MapView
    {
        private readonly Texture2D _txtBG;

        private readonly Texture2D _txtWall3;
        private readonly Texture2D _txtWall2;
        private readonly Texture2D _txtWall1;

        private readonly Texture2D _txtSide3;
        private readonly Texture2D _txtSide3f;
        private readonly Texture2D _txtSide3b;
        private readonly Texture2D _txtSide3bf;
        private readonly Texture2D _txtSide2;
        private readonly Texture2D _txtSide2f;
        private readonly Texture2D _txtSide2b;
        private readonly Texture2D _txtSide2bf;
        private readonly Texture2D _txtSide1;
        private readonly Texture2D _txtSide1f;
        private readonly Texture2D _txtSide0;
        private readonly Texture2D _txtSide0f;

        private Vector2[] _ptWall3;
        private Vector2[] _ptWall2;
        private Vector2[] _ptWall1;
        private Vector2[] _ptWall0;

        public Map25DView(Game game, Player player, Map map, int renderWitdth, int renderHeight) : base(game, player, map, renderWitdth, renderHeight)
        {

            //Texture de base (celle par-dessus laquelle on va afficher les autres)
            _txtBG = game.Content.Load<Texture2D>("bg");

            //Texture des murs de face à différente distance
            _txtWall1 = game.Content.Load<Texture2D>("wall-1");
            _txtWall2 = game.Content.Load<Texture2D>("wall-2");
            _txtWall3 = game.Content.Load<Texture2D>("wall-3");

            //Textures des murs de coté, F correspond à flip
            _txtSide0 = game.Content.Load<Texture2D>("side-0");
            _txtSide0f = game.Content.Load<Texture2D>("side-0f");
            _txtSide1 = game.Content.Load<Texture2D>("side-1");
            _txtSide1f = game.Content.Load<Texture2D>("side-1f");
            _txtSide2 = game.Content.Load<Texture2D>("side-2");
            _txtSide2f = game.Content.Load<Texture2D>("side-2f");
            _txtSide2b = game.Content.Load<Texture2D>("side-2b");
            _txtSide2bf = game.Content.Load<Texture2D>("side-2bf");
            _txtSide3 = game.Content.Load<Texture2D>("side-3");
            _txtSide3f = game.Content.Load<Texture2D>("side-3f");
            _txtSide3b = game.Content.Load<Texture2D>("side-3b");
            _txtSide3bf = game.Content.Load<Texture2D>("side-3bf");

            //Les murs à dist 3 font 48 piexel de large
            _ptWall3 = new Vector2[] { new Vector2(16 - 48, 23), new Vector2(16, 23), new Vector2(63, 23), new Vector2(111, 23), new Vector2(159, 23)};
            _ptWall2 = new Vector2[5] { new Vector2(0, 22), new Vector2(47 - 80, 15), new Vector2(47, 15), new Vector2(127, 15), new Vector2(159, 22) };
            _ptWall1 = new Vector2[3] { new Vector2(23 - 128, 3), new Vector2(23, 3), new Vector2(151, 3) };
            _ptWall0 = new Vector2[3] { new Vector2(0, 0), new Vector2(), new Vector2(151, 0) };
        }


        public override void Draw(SpriteBatch spriteBatch)
        {

            _game.GraphicsDevice.SetRenderTarget(_render);

            spriteBatch.Begin();

            

            Point[][] tuiles3D = new Point[4][];
            tuiles3D[0] = dungeonMap.view[(int)_player.CurrentOrientation][0];
            tuiles3D[1] = dungeonMap.view[(int)_player.CurrentOrientation][1];
            tuiles3D[2] = dungeonMap.view[(int)_player.CurrentOrientation][2];
            tuiles3D[3] = dungeonMap.view[(int)_player.CurrentOrientation][3];


            spriteBatch.Draw(_txtBG, new Vector2(0, 0), Color.White);

            //======================================================================================
            //Niveau 3 (algorithme du paintre on fait apparaitre ce qu'il y a de plus loin d'abord)
            //======================================================================================
            
            //Mur 0 = le plus a gauche
            int tile_3_0 = dungeonMap.GetWallAt(_player.posX + tuiles3D[3][0].X, _player.posY + tuiles3D[3][0].Y);
            if(tile_3_0 == 1)
                spriteBatch.Draw(_txtWall3, _ptWall3[0], Color.White);

            //Mur 1 = le deuxieme le plus a gauche
            int tile_3_1 = dungeonMap.GetWallAt(_player.posX + tuiles3D[3][1].X, _player.posY + tuiles3D[3][1].Y);
            if (tile_3_1 == 1)
                spriteBatch.Draw(_txtWall3, _ptWall3[1], Color.White);
            else if (tile_3_0 == 1)
                spriteBatch.Draw(_txtSide3b, _ptWall3[1],Color.White); //side du mur 0 

            //Mur 4 = le plus a droite
            int tile_3_4 = dungeonMap.GetWallAt(_player.posX + tuiles3D[3][4].X, _player.posY + tuiles3D[3][4].Y);
            if(tile_3_4 == 1) 
                spriteBatch.Draw(_txtWall3, _ptWall3[4], Color.White);

            //Mur 3 = le deuxieme le plus a droite 
            int tile_3_3 = dungeonMap.GetWallAt(_player.posX + tuiles3D[3][3].X, _player.posY + tuiles3D[3][3].Y);
            if (tile_3_3 == 1)
                spriteBatch.Draw(_txtWall3, _ptWall3[3], Color.White);
            else if (tile_3_4 == 1)//Side du Mur 4
                spriteBatch.Draw(_txtSide3bf, _ptWall3[4] - new Vector2(_txtSide3bf.Width, 0), Color.White);

            //Mur 2 (mur du centre)
            int tile_3_2 = dungeonMap.GetWallAt(_player.posX + tuiles3D[3][2].X, _player.posY + tuiles3D[3][2].Y);
            if (tile_3_2 == 1)
                spriteBatch.Draw(_txtWall3, _ptWall3[2], Color.White);
            else
            {
                if (tile_3_1 == 1)
                {
                    spriteBatch.Draw(_txtSide3, _ptWall3[1] + new Vector2(_txtWall3.Width,0), Color.White);
                }
                if (tile_3_3 == 1)
                {
                    spriteBatch.Draw(_txtSide3f, _ptWall3[3] - new Vector2(_txtSide3f.Width, 0), Color.White);
                }
            }

            // =========================================================================================================
            // = Niveau 2 (5 murs à afficher)
            // =========================================================================================================
            // Mur 0 (le plus à gauche, on voit qu'un côté)
            int tile_2_0 = dungeonMap.GetWallAt(_player.posX + tuiles3D[2][0].X, _player.posY + tuiles3D[2][0].Y);
            if (tile_2_0 == 1)
            {
                spriteBatch.Draw(_txtSide2b, _ptWall2[0], Color.White);
            }
            // Mur 1 (le 2e à gauche)
            int tile_2_1 = dungeonMap.GetWallAt(_player.posX + tuiles3D[2][1].X, _player.posY + tuiles3D[2][1].Y);
            if (tile_2_1 == 1)
            {
                spriteBatch.Draw(_txtWall2, _ptWall2[1], Color.White);
            }
            // Mur 4 (le plus à droite, on voit qu'un côté)
            int tile_2_4 = dungeonMap.GetWallAt(_player.posX + tuiles3D[2][4].X, _player.posY + tuiles3D[2][4].Y);
            if (tile_2_4 == 1)
            {
                spriteBatch.Draw(_txtSide2bf, _ptWall2[4], Color.White);
            }
            // Mur 3 (le 2ème de droite)
            int tile_2_3 = dungeonMap.GetWallAt(_player.posX + tuiles3D[2][3].X, _player.posY + tuiles3D[2][3].Y);
            if (tile_2_3 == 1)
            {
                spriteBatch.Draw(_txtWall2, _ptWall2[3], Color.White);
            }
            // Mur 2 (au centre)
            int tile_2_2 = dungeonMap.GetWallAt(_player.posX + tuiles3D[2][2].X, _player.posY + tuiles3D[2][2].Y);
            if (tile_2_2 == 1)
            {
                spriteBatch.Draw(_txtWall2, _ptWall2[2], Color.White);
            }
            else
            {
                // Side du mur 1 (si pas de mur 2 et mur 1)
                if (tile_2_1 == 1)
                    spriteBatch.Draw(_txtSide2, _ptWall2[1] + new Vector2(_txtWall2.Width, 0), Color.White);
                // Side du mur 3 (si pas de mur 2 et mur 3)
                if (tile_2_3 == 1)
                    spriteBatch.Draw(_txtSide2f, _ptWall2[3] - new Vector2(_txtSide2f.Width, 0), Color.White);
            }

            // =========================================================================================================
            // = Niveau 1 (3 murs à afficher)
            // =========================================================================================================
            // Mur 0 (le plus à gauche)
            int tile_1_0 = dungeonMap.GetWallAt(_player.posX + tuiles3D[1][0].X, _player.posY + tuiles3D[1][0].Y);
            if (tile_1_0 == 1)
            {
                spriteBatch.Draw(_txtWall1, _ptWall1[0], Color.White);
            }
            // Mur 2 (le plus à droite)
            int tile_1_2 = dungeonMap.GetWallAt(_player.posX + tuiles3D[1][2].X, _player.posY + tuiles3D[1][2].Y);
            if (tile_1_2 == 1)
            {
                spriteBatch.Draw(_txtWall1, _ptWall1[2], Color.White);
            }
            // Mur 1 (au centre)
            int tile_1_1 = dungeonMap.GetWallAt(_player.posX + tuiles3D[1][1].X, _player.posY + tuiles3D[1][1].Y);
            if (tile_1_1 == 1)
            {
                spriteBatch.Draw(_txtWall1, _ptWall1[1], Color.White);
            }
            else
            {
                // Side du mur 0 (si pas de mur 1 et mur 0)
                if (tile_1_0 == 1)
                    spriteBatch.Draw(_txtSide1, _ptWall1[1], Color.White);
                // Side du mur 2 (si pas de mur 1 et mur 2)
                if (tile_1_2 == 1)
                    spriteBatch.Draw(_txtSide1f, _ptWall1[2] - new Vector2(_txtSide1f.Width, 0), Color.White);
            }

            // =========================================================================================================
            // = Niveau 1 (2 côté de mure à afficher)
            // =========================================================================================================
            // Mur 0 (le plus à gauche)
            int tile_0_0 = dungeonMap.GetWallAt(_player.posX + tuiles3D[0][0].X, _player.posY + tuiles3D[0][0].Y);
            if (tile_0_0 == 1)
            {
                spriteBatch.Draw(_txtSide0, _ptWall0[0], Color.White);
            }
            // Mur 2 (le plus à droite)
            int tile_0_2 = dungeonMap.GetWallAt(_player.posX + tuiles3D[0][2].X, _player.posY + tuiles3D[0][2].Y);
            if (tile_0_2 == 1)
            {
                spriteBatch.Draw(_txtSide0f, _ptWall0[2], Color.White);
            }


            spriteBatch.End();

            _game.GraphicsDevice.SetRenderTarget(null);
        }
    }
}
