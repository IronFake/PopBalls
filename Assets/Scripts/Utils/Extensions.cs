using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace.Utils
{
    public static class Extensions
    {
        public static T GetRoot<T>(this Scene scene) where T : MonoBehaviour
        {
            var rootObjects = scene.GetRootGameObjects();
            
            T result = default;
            foreach (var go in rootObjects)
            {
                if (go.TryGetComponent(out result))
                {
                    break;
                }
            }

            return result;
        }
    }
}