using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class PauseMenuUI : MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button menuButton;

        private void Start()
        {
            resumeButton.onClick.AddListener(ResumeGame);
            restartButton.onClick.AddListener(RestartGame);
            menuButton.onClick.AddListener(EnterMenu);
        }

        private void ResumeGame()
        {
            Debug.Log("Resume Game");
        }

        private void RestartGame()
        {
            Debug.Log("Restart Game");
        }

        private void EnterMenu()
        {
            Debug.Log("Enter to Menu");
        }
    }
}