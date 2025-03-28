using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BIS
{
    public class GameOverSceneUI : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI _socreText;
        [SerializeField] private Button _retryBut;
        [SerializeField] private Button _exitBtn;
        [SerializeField] private string _retryGameSceneName;

        private void Awake()
        {
            _socreText.text = $"최고 기록 : {PlayerPrefs.GetInt("Score", 0)}점";
            _retryBut.onClick.AddListener(HandleRetry);
            _exitBtn.onClick.AddListener(HandleExit);
        }

        private void HandleExit()
        {
            SceneControlManager.FadeOut(() =>
            {
                Application.Quit();
            });
        }

        private void HandleRetry()
        {
            SceneControlManager.FadeOut(() =>
            {
                SceneManager.LoadScene(_retryGameSceneName);
            });
        }

        private void OnDestroy()
        {
            _retryBut.onClick.RemoveListener(HandleRetry);
            _exitBtn.onClick.RemoveListener(HandleExit);
        }
    }
}
