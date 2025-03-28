using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace BIS
{
    public class BossStageSlider : MonoBehaviour
    {
        private Slider _slider;
        private float _elapsedTime;
        private const float Duration = 180f;
        [SerializeField] private UnityEvent _monsterSpawnEvent;

        private bool _isGameStart = false;
        private void Awake()
        {
            StartCoroutine(StartSpawn());
            _slider = GetComponent<Slider>();
            _slider.value = 0;
        }

        private IEnumerator StartSpawn()
        {
            yield return new WaitForSeconds(3);
            _isGameStart = true;
            _monsterSpawnEvent?.Invoke();
            yield return new WaitForSeconds(30); //30��
            _monsterSpawnEvent?.Invoke();
            yield return new WaitForSeconds(30); //60��
            _monsterSpawnEvent?.Invoke();
            yield return new WaitForSeconds(30); //90��
            _monsterSpawnEvent?.Invoke();
            yield return new WaitForSeconds(30); //120��
            SceneControlManager.FadeOut(() =>
            {
                SceneManager.LoadScene("BossScene");
            });
        }



        private void Update()
        {


            if (_isGameStart == false)
                return;


            // ��� �ð� ����
            _elapsedTime += Time.deltaTime;
            _slider.value = Mathf.Clamp01(_elapsedTime / Duration);


            // �����̴� �� ������Ʈ (0���� 1�� ��ȭ)
        }
    }
}
