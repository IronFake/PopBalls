using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class SceneInitializer : MonoBehaviour
    {
        private void Start()
        {
            LoadMainMenu();
        }

        private void LoadMainMenu()
        {
            var operations = new Queue<ILoadingOperation>();
            operations.Enqueue(new MainMenuLoadingOperation());
            LoadingScreen.Instance.Load(operations);
        }
    }
}