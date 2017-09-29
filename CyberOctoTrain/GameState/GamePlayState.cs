using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CyberOctoTrain.Components;
using CyberOctoTrain.Input;
using CyberOctoTrain.StateManager;
using CyberOctoTrain.TileEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CyberOctoTrain.GameState
{
    class GamePlayState : BaseGameState, IGamePlayState
    {
        private Engine _engine = new Engine(Game1.ScreenRectangle, 64, 64);
        private TileMap _map;
        private Camera _camera;

        public GamePlayState(Game game) 
            : base(game)
        {
            game.Services.AddService(typeof(IGamePlayState), this);
        }

        public override void Update(GameTime gameTime)
        {
            var motion = Vector2.Zero;

            if (Xin.KeyboadState.IsKeyDown(Keys.W) 
                && Xin.KeyboadState.IsKeyDown(Keys.A))
            {
                motion.X = -1;
                motion.Y = -1;
            }
            else if (Xin.KeyboadState.IsKeyDown(Keys.W)
                       && Xin.KeyboadState.IsKeyDown(Keys.D))
            {
                motion.X = 1;
                motion.Y = -1;
            }
            else if (Xin.KeyboadState.IsKeyDown(Keys.S)
                       && Xin.KeyboadState.IsKeyDown(Keys.A))
            {
                motion.X = -1;
                motion.Y = 1;
            }
            else if (Xin.KeyboadState.IsKeyDown(Keys.S)
                       && Xin.KeyboadState.IsKeyDown(Keys.D))
            {
                motion.X = 1;
                motion.Y = 1;
            }
            else if (Xin.KeyboadState.IsKeyDown(Keys.W))
            {
                motion.Y = -1;
            }
            else if (Xin.KeyboadState.IsKeyDown(Keys.S))
            {
                motion.Y = 1;
            }
            else if (Xin.KeyboadState.IsKeyDown(Keys.A))
            {
                motion.X = -1;
            }
            else if (Xin.KeyboadState.IsKeyDown(Keys.D))
            {
                motion.X = 1;
            }

            if (motion != Vector2.Zero)
            {
                motion.Normalize();
                motion *= _camera.Speed;
                _camera.Position += motion;
                _camera.LockCamera(_map, Game1.ScreenRectangle);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (_map != null && _camera != null)
                _map.Draw(gameTime, GameRef.SpriteBatch, _camera);
        }

        public void SetUpNewGame()
        {
            var tiles = GameRef.Content.Load<Texture2D>(@"Tiles\tileset1");
            var set = new TileSet(8, 8, 32, 32) {Texture = tiles};

            var background = new TileLayer(200, 200);
            var edge = new TileLayer(200, 200);
            var building = new TileLayer(200, 200);
            var decor = new TileLayer(200,200);

            _map = new TileMap(set, background, edge, building, decor, "test-map");

            _map.FillEdge();
            _map.FillBuilding();
            _map.FillDecoration();

            _camera = new Camera();
        }

        public void LoadExistingGame()
        {
            
        }

        public void StartGame()
        {
            
        }
    }
}
