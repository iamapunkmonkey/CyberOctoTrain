using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CyberOctoTrain.StateManager
{
    public class TitleIntroState : BaseGameState, ITitleIntroState
    {
        private Texture2D _background;
        private Rectangle _backgroundDestination;
        private SpriteFont _font;
        private TimeSpan _elapsed;
        private Vector2 _position;
        private string _message;

        public TitleIntroState(Game game) : base(game)
        {
            game.Services.AddService(typeof(ITitleIntroState), this);
        }

        public override void Initialize()
        {
            _backgroundDestination = Game1.ScreenRectangle;
            _elapsed = TimeSpan.Zero;
            _message = "PRESS SPACE TO CONTINUE";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _background = Content.Load<Texture2D>(@"GameScreens\titlescreen");
            _font = Content.Load<SpriteFont>(@"Fonts\InterfaceFont");

            var size = _font.MeasureString(_message);
            _position = new Vector2((Game1.ScreenRectangle.Width - size.X) / 2,
                Game1.ScreenRectangle.Bottom - 50 - _font.LineSpacing);
 
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var index = PlayerIndex.One;
            _elapsed += gameTime.ElapsedGameTime;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();

            GameRef.SpriteBatch.Draw(_background, _backgroundDestination, Color.White);

            var color = new Color(1f, 1f, 1f) * (float) Math.Abs(Math.Sin(_elapsed.TotalSeconds * 2));

            GameRef.SpriteBatch.DrawString(_font, _message, _position, color);

            GameRef.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}