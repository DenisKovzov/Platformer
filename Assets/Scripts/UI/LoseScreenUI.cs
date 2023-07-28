using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class LoseScreenUI : BaseScreenUI
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button menuButton;

        private GameStateMachine stateMachine;

        public void Construct(GameStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        private void Start()
        {
            restartButton.onClick.AddListener(RestartGame);
            menuButton.onClick.AddListener(EnterMenu);
        }

        private void RestartGame()
        {
            stateMachine.EnterIn<StartGameState>();
        }

        private void EnterMenu()
        {
            Debug.Log("Enter to Menu");
            Application.Quit();
        }
    }
}
