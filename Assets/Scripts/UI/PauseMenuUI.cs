using System;
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
            Hide();
            resumeButton.onClick.AddListener(ResumeGame);
            restartButton.onClick.AddListener(RestartGame);
            menuButton.onClick.AddListener(EnterMenu);
        }

        private void ResumeGame()
        {
            stateMachine.EnterIn<GameplayState>();
        }

        private void RestartGame()
        {
            stateMachine.EnterIn<InitializeState>();
        }

        private void EnterMenu()
        {
            Debug.Log("Enter to Menu");
        }
    }
}