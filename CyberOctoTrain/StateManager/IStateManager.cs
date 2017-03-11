using System;
using Microsoft.Xna.Framework;

namespace CyberOctoTrain.StateManager
{
    public interface IStateManager
    {
        IGameState CurrentState { get; }

        event EventHandler StateChanged;

        void PushState(IGameState state, PlayerIndex? index);
        void ChangeState(IGameState state, PlayerIndex? index);
        void PopState();
        bool ContainsState(IGameState state);
    }
}