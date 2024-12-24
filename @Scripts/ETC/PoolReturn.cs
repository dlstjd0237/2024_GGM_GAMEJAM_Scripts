using UnityEngine;
using UnityEngine.SceneManagement;

namespace BIS.Pool
{
    public class PoolReturn : MonoBehaviour
    {
        private void OnEnable()
        {
            SceneManager.sceneLoaded += HandleSceneLoad;

        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= HandleSceneLoad;
            PoolManager.ReturnToPool(gameObject);
        }

        private void HandleSceneLoad(UnityEngine.SceneManagement.Scene arg0, LoadSceneMode arg1)
        {
            gameObject.SetActive(false);
        }
    }
}

