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
     class Map2DView : MapView
    {
        private Texture2D _flecheMap;
        private GCRectangle _monRectangle;
        


        public Map2DView( Game game, Player player, Map map, int renderWitdth, int renderHeight) : base(game, player, map, renderWitdth, renderHeight)
        {
            _flecheMap = game. Content.Load<Texture2D>("fleche");
            _monRectangle = new GCRectangle(game, GCRectangle.Type.outline, 0, 0, renderWitdth/ map.width , renderHeight/map.height , Color.Gray, Color.White);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            _game.GraphicsDevice.SetRenderTarget(_render);
            spriteBatch.Begin();
            for (int l = 0; l < dungeonMap.height; l++)
            {
                for (int c = 0; c < dungeonMap.width; c++)
                {
                    _monRectangle.MoveTo(c * _monRectangle.Rect.Width, l * _monRectangle.Rect.Height);
                    if (dungeonMap.mapWalls[l, c] == 1)
                    {
                        _monRectangle.FillColor = Color.Gray;
                    }
                    else
                    {
                        _monRectangle.FillColor = Color.Black;
                    }
                    _monRectangle.Draw(spriteBatch);
                }
            }

            Vector2 vecFleche = new Vector2(
                (_player.posX * _monRectangle.Rect.Width) + (_monRectangle.Rect.Width / 2),
                (_player.posY * _monRectangle.Rect.Height) + (_monRectangle.Rect.Height / 2)
                );

            Rectangle rectFleche = new Rectangle(0, 0, _flecheMap.Width, _flecheMap.Height);

            float angle = 0f;

            switch (_player.CurrentOrientation)
            {
                case Orientation.Up:
                    angle = 0;
                    break;
                case Orientation.Right:
                    angle = MathHelper.Pi * 90f / 180f;
                    break;
                case Orientation.Down:
                    angle = MathHelper.Pi * 180f / 180f;
                    break;
                case Orientation.Left:
                    angle = MathHelper.Pi * 270f / 180f;
                    break;
                default:
                    Debug.Fail("Erreur dans l'orientation");
                    break;
            }

            spriteBatch.Draw(
                _flecheMap,
                vecFleche,
                rectFleche,
                Color.White,
                angle,
                new Vector2(_flecheMap.Width / 2, _flecheMap.Height / 2),
            1f,
                SpriteEffects.None, 1);
            
            spriteBatch.End();
            _game.GraphicsDevice.SetRenderTarget(null);
            
        }
    }
}
