using CyberOctoTrain.Components;
using CyberOctoTrain.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CyberOctoTrain.StateManager
{
    public class MainMenuState : BaseGameState, IMainMenuState
    {
        private Texture2D _background;
        private SpriteFont _font;
        private MenuComponent _menuComponent;

        public MainMenuState(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IMainMenuState), this);
        }

        protected override void LoadContent()
        {
            _font = Game.Content.Load<SpriteFont>(@"Fonts\InterfaceFont");
            _background = Game.Content.Load<Texture2D>(@"GameScreens\menuscreen");
            var texture = Game.Content.Load<Texture2D>(@"Misc\wooden-button");

            string[] menuItems = { "NEW GAME", "CONTINUE", "OPTIONS", "EXIT" };

            _menuComponent = new MenuComponent(_font, texture, menuItems);

            var position = new Vector2
            {
                Y = 90,
                X = 1200 - _menuComponent.Width
            };

            _menuComponent.Position = position;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _menuComponent.Update(gameTime);
            if (Xin.CheckKeyReleased(Keys.Space) || Xin.CheckKeyReleased(Keys.Enter) ||
                (_menuComponent.MouseOver && Xin.CheckMouseReleased(MouseButtons.Left)))
            {
                switch (_menuComponent.SelectedIndex)
                {
                    case 0:
                        Xin.FlushInput();

                        GameRef.GamePlayState.SetUpNewGame();
                        GameRef.GamePlayState.StartGame();
                        Manager.PushState(GameRef.GamePlayState, PlayerIndexInControl);
                        break;
                    case 1:
                        Xin.FlushInput();

                        GameRef.GamePlayState.LoadExistingGame();
                        GameRef.GamePlayState.StartGame();
                        Manager.PushState(GameRef.GamePlayState, PlayerIndexInControl);
                        break;
                    case 2:
                        Xin.FlushInput();
                        break;
                    case 3:
                        Game.Exit();
                        break;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();

            GameRef.SpriteBatch.Draw(_background, Vector2.Zero, Color.White);

            GameRef.SpriteBatch.End();

            base.Draw(gameTime);

            GameRef.SpriteBatch.Begin();

            _menuComponent.Draw(gameTime, GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }
    }
}