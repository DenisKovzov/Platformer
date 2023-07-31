using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class WonScreenUI : BaseScreenUI
    {
        [SerializeField] private Button menuButton;

        private void Start()
        {
            menuButton.onClick.AddListener(EnterMenu);
        }

        private void OnDestroy()
        {
            menuButton.onClick.RemoveListener(EnterMenu);
        }

        private void EnterMenu()
        {
            Debug.Log("Enter to Menu");
            Application.Quit();
        }
    }

}