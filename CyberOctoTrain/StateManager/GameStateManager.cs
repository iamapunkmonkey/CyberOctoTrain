using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CyberOctoTrain.StateManager
{
    public class GameStateManager : GameComponent, IStateManager
    {
        public IGameState CurrentState => GameStates.Peek();

        public Stack<IGameState> GameStates { get; } = new Stack<IGameState>();

        private const int StartDrawOrder = 5000;
        private const int DrawOrderInc = 50;
        private int _drawOrder;

        public event EventHandler StateChanged;

        public GameStateManager(Game game) : base(game)
        {
            Game.Services.AddService(typeof(IStateManager), this);
        }

        public void PushState(IGameState state, PlayerIndex? index)
        {
            _drawOrder += DrawOrderInc;
            AddState(state, index);
            OnStateChanged();
        }

        private void AddState(IGameState state, PlayerIndex? index)
        {
            GameStates.Push(state);
            state.PlayerIndexInControl = index;
            Game.Components.Add(state);
            StateChanged += state.StateChanged;
        }

        public void ChangeState(IGameState state, PlayerIndex? index)
        {
            while (GameStates.Count > 0)
            {
                RemoveState();
            }

            _drawOrder = StartDrawOrder;
            var gameState = state as GameState;
            if(gameState == null)
                return;
            gameState.DrawOrder = _drawOrder;
            _drawOrder += DrawOrderInc;
            AddState(state, index);

            OnStateChanged();
        }

        public void PopState()
        {
            if (GameStates.Count == 0) return;
            RemoveState();
            _drawOrder -= DrawOrderInc;
            OnStateChanged();
        }

        private void RemoveState()
        {
            var state = GameStates.Peek();

            StateChanged -= state.StateChanged;
            Game.Components.Remove(state);
            GameStates.Pop();
        }

        public bool ContainsState(IGameState state)
        {
            return GameStates.Contains(state);
        }

        protected internal virtual void OnStateChanged()
        {
            StateChanged?.Invoke(this, null);
        }
    }
}