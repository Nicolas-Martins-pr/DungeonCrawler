using DungeonCrawler.MapSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Module;
using System.Diagnostics;

namespace DungeonCrawler
{
    public class DungeonCrawler : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        private Map _dungeonMap;
        private Player _player;
        private InputType _inputManager;

        private Map2DView _map2DView;
        private Map25DView _map25DView;
        
        enum GameMode
        {
            Dungeon,
            Map
        }

        GameMode _mode;

        public DungeonCrawler()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _mode = GameMode.Dungeon;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
           
            _inputManager = new InputType();

            _dungeonMap = new Map();
            _player = new Player();
            _player.SetOriantation(Orientation.Down);
            _player.SetPosition(1, 1);

             
            _map2DView = new Map2DView(this, _player,  _dungeonMap, GraphicsDevice.Viewport.Width/2,GraphicsDevice.Viewport.Height/2);
            _map25DView = new Map25DView(this, _player, _dungeonMap, 176, 112);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _inputManager.Update();

           
            //Change le type de vue, soit on regarde la map soit on est en first person
            if (_inputManager.GetKeyDown(Keys.M))
            {
                if (_mode == GameMode.Dungeon)
                {
                    _mode = GameMode.Map;
                    _map2DView.Draw(_spriteBatch);
                }
                else
                {
                    _mode = GameMode.Dungeon;
                }
            }
        
            if(_mode == GameMode.Dungeon) {

                

                //Fait tourner la fleche + player
                if (_inputManager.GetKeyDown(Keys.Right))
                    _player.TurnRight();
                if (_inputManager.GetKeyDown(Keys.Left))
                    _player.TurnLeft();

                //Fait avancer la fleche + player
                if (_inputManager.GetKeyDown(Keys.Up))
                    UpdatePlayerPos(_player.CurrentOrientation);

            }
           
                
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _map25DView.Draw(_spriteBatch);


            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            switch (_mode)
            {
                case GameMode.Dungeon:
                    _spriteBatch.Draw(_map25DView.GetRender(), new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), null, Color.White, 0, new Vector2(_map25DView.GetRender().Width/2, _map25DView.GetRender().Height / 2), 4f, SpriteEffects.None, 0);
                    break;
                case GameMode.Map:

                    _spriteBatch.Draw(_map2DView.GetRender(), new Vector2(0, 0), Color.White);
                    break;
                default:
                    break;
            }

            


            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void UpdatePlayerPos(Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.Up:
                    if (_player.posY > 0)
                    {
                        if (_dungeonMap.mapWalls[_player.posY - 1, _player.posX] == 0)
                            _player.SetPosition(_player.posX, _player.posY - 1);
                    }
                    break;
                case Orientation.Right:
                    if (_player.posX < _dungeonMap.width - 1)
                    {
                        if (_dungeonMap.mapWalls[_player.posY, _player.posX + 1] == 0)
                            _player.SetPosition(_player.posX + 1, _player.posY);
                    }
                    break;
                case Orientation.Down:
                    if (_player.posY < _dungeonMap.height - 1)
                    {
                        if (_dungeonMap.mapWalls[_player.posY + 1, _player.posX] == 0)
                            _player.SetPosition(_player.posX, _player.posY + 1);
                    }
                    break;
                case Orientation.Left:
                    if (_player.posX > 0)
                    {
                        if (_dungeonMap.mapWalls[_player.posY, _player.posX - 1] == 0)
                            _player.SetPosition(_player.posX - 1, _player.posY);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}