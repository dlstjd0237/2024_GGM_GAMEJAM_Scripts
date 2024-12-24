using TMPro;
using UnityEngine;

namespace BIS
{
    public class Score : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _text.text = $"Score : {(int)PlayerPrefs.GetFloat("Score", 0)}";
            float score = PlayerPrefs.GetFloat("Score", 0) + Time.deltaTime;
            PlayerPrefs.SetFloat("Score", score);
        }
    }
}
