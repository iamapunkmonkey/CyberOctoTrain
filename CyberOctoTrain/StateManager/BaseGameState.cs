using System;
using Microsoft.Xna.Framework;

namespace CyberOctoTrain.StateManager
{
    public class BaseGameState : GameState
    {
        protected static Random random = new Random();
        protected Game1 GameRef;

        public BaseGameState(Game game) : base(game)
        {
            GameRef = (Game1) game;
        }
    }
}