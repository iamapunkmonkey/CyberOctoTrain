using System;
using Microsoft.Xna.Framework;

namespace CyberOctoTrain.StateManager
{
    public interface IGameState : IGameComponent
    {

        IGameState Tag { get; }
        PlayerIndex? PlayerIndexInControl { get; set; }
        void StateChanged(object sender, EventArgs e);
    }
}