using CyberOctoTrain.StateManager;

namespace CyberOctoTrain.GameState
{
    public interface IGamePlayState : IGameState
    {
        void SetUpNewGame();
        void StartGame();
        void LoadExistingGame();
    }
}