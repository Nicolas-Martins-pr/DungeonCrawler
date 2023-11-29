using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.MapSystem
{
    abstract class MapView : IDisposable
    {
        protected RenderTarget2D _render;
        protected Player _player;
        protected Game _game;
        public Map dungeonMap;
        


        public RenderTarget2D GetRender() { return _render; }
        public MapView( Game game, Player player, Map map, int renderWitdth, int renderHeight)
        {
            _game = game;
            _player = player;
            dungeonMap = map;

            PresentationParameters pp = _game.GraphicsDevice.PresentationParameters;
            _render = new RenderTarget2D(_game.GraphicsDevice,
                renderWitdth ,
                renderHeight ,
                false,
                SurfaceFormat.Color,
                DepthFormat.None,
                pp.MultiSampleCount,
                RenderTargetUsage.DiscardContents);
        }
        abstract public void Draw(SpriteBatch spriteBatch);
        public void Dispose()
        {
            _render.Dispose();
        }
        
    }
}
