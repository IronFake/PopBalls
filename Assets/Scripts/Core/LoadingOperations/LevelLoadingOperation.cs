using System;
using System.Threading.Tasks;
using DefaultNamespace.Utils;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class LevelLoadingOperation : ILoadingOperation
    {
        public string Description => "Game loading...";
        
        public async Task Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            var loadOp = SceneManager.LoadSceneAsync("Level", LoadSceneMode.Single);
            while (loadOp.isDone == false)
            {
                await Task.Delay(1);
            }
            onProgress?.Invoke(0.7f);
            
            var scene = SceneManager.GetSceneByName("Level");
            var game = scene.GetRoot<Game>();
            game.Init();
            game.StartGame();
            onProgress?.Invoke(1.0f);
        }
    }
}