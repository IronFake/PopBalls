using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class MainMenuLoadingOperation : ILoadingOperation
    {
        public string Description => "Game loading...";
        
        public async Task Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            var loadOp = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
            while (loadOp.isDone == false)
            {
                await Task.Delay(1);
            }
            onProgress?.Invoke(1f);
        }
    }
}