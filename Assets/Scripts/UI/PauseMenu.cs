using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button quitButton;

        public event Action OnContinueButtonPressed;
        public event Action OnRestartButtonPressed;
        public event Action OnQuitButtonPressed;

        public void Init()
        {
            continueButton.onClick.AddListener(() => OnContinueButtonPressed?.Invoke());
            restartButton.onClick.AddListener(() => OnRestartButtonPressed?.Invoke());
            quitButton.onClick.AddListener(() => OnQuitButtonPressed?.Invoke());
        }
        
        public void ShowPauseMenu()
        {
            gameObject.SetActive(true);
        }

        public void HidePauseMenu()
        {
            gameObject.SetActive(false);
        }
    }
}