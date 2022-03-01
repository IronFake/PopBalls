using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class LoadingScreen : MonoBehaviour
    {
        public static LoadingScreen Instance;

        [SerializeField] private Canvas loadingCanvas;
        
        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        public async void Load(Queue<ILoadingOperation> loadingOperations)
        {
            loadingCanvas.enabled = true;
            
            foreach (var operation in loadingOperations)
            {
                await operation.Load(OnProgress);
            }

            loadingCanvas.enabled = false;
        }

        private void OnProgress(float progress)
        {
            
        }
    }
}