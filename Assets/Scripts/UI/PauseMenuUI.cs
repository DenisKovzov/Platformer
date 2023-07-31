using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class PauseMenuUI : BaseScreenUI
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button menuButton;

        private GameStateMachine stateMachine;

        public void Construct(GameStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        private void Start()
        {
            resumeButton.onClick.AddListener(ResumeGame);
            restartButton.onClick.AddListener(RestartGame);
            menuButton.onClick.AddListener(EnterMenu);
        }

        private void OnDestroy()
        {
            resumeButton.onClick.RemoveListener(ResumeGame);
            restartButton.onClick.RemoveListener(RestartGame);
            menuButton.onClick.RemoveListener(EnterMenu);
        }

        private void ResumeGame()
        {
            stateMachine.EnterIn<GameplayState>();
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