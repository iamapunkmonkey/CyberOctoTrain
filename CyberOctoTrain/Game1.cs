using CyberOctoTrain.GameState;
using CyberOctoTrain.StateManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CyberOctoTrain
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
	    private readonly GraphicsDeviceManager _graphics;
	    public static Rectangle ScreenRectangle { get; private set; }
	    public SpriteBatch SpriteBatch { get; private set; }
	    public GameStateManager GameStateManager { get; }
	    public ITitleIntroState IntroState { get; }
	    public IMainMenuState StartMenuState { get; }
        public IGamePlayState GamePlayState { get; }

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

		    ScreenRectangle = new Rectangle(0, 0, 1280, 720);
		    _graphics.PreferredBackBufferHeight = ScreenRectangle.Height;
		    _graphics.PreferredBackBufferWidth = ScreenRectangle.Width;

		    GameStateManager = new GameStateManager(this);
		    Components.Add(GameStateManager);

		    IntroState = new TitleIntroState(this);
		    StartMenuState = new MainMenuState(this);
            GamePlayState = new GamePlayState(this);

		    GameStateManager.ChangeState(IntroState, PlayerIndex.One);
		}

		protected override void LoadContent()
		{
		    SpriteBatch = new SpriteBatch(GraphicsDevice);
		}

		protected override void Update(GameTime gameTime)
		{
#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
#endif

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			_graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			base.Draw(gameTime);
		}
	}
}
