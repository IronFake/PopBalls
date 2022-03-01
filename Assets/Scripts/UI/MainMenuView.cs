using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button quitButton;

        private void Start()
        {
            startGameButton.onClick.AddListener(StartGame);
            quitButton.onClick.AddListener(QuitGame);
        }
        
        private void StartGame()
        {
            var operations = new Queue<ILoadingOperation>();
            operations.Enqueue(new LevelLoadingOperation());
            LoadingScreen.Instance.Load(operations);
        }
        
        private void QuitGame()
        {
            Application.Quit();
        }
    }
}