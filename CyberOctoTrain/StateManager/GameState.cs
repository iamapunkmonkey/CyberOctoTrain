using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CyberOctoTrain.StateManager
{
    public abstract class GameState : DrawableGameComponent, IGameState
    {
        public IGameState Tag { get; }
        public PlayerIndex? PlayerIndexInControl { get; set; }
        public IStateManager Manager { get; }
        public ContentManager Content { get; }
        public List<GameComponent> Components { get; } = new List<GameComponent>();

        public GameState(Game game)
            : base(game)
        {
            Tag = this;

            Content = Game.Content;

            Manager = (IStateManager) Game.Services.GetService(typeof(IStateManager));
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in Components)
            {
                if (component.Enabled)
                {
                    component.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (var component in Components)
            {
                var gameComponent = component as DrawableGameComponent;
                if(gameComponent != null && gameComponent.Visible)
                    gameComponent.Draw(gameTime);
            }
        }

        public virtual void StateChanged(object sender, EventArgs e)
        {
            if (Manager.CurrentState == Tag)
                Show();
            else
                Hide();
        }

        public virtual void Show()
        {
            Enabled = true;
            Visible = true;

            foreach (var component in Components)
            {
                component.Enabled = true;

                var gameComponent = component as DrawableGameComponent;
                if (gameComponent != null)
                    gameComponent.Visible = true;
            }
        }

        public virtual void Hide()
        {
            Enabled = false;
            Visible = false;

            foreach (var component in Components)
            {
                component.Enabled = false;

                var gameComponent = component as DrawableGameComponent;
                if (gameComponent != null)
                    gameComponent.Visible = false;
            }

        }
    }
}