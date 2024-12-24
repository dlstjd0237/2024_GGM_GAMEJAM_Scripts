using UnityEngine;
using DG.Tweening;
using Unity.Cinemachine;
using System.Collections;

namespace BIS.Objects
{
    public class CameraBitShake : MonoBehaviour
    {
        private CinemachineCamera _cam;
        private void Awake()
        {
            _cam = GetComponent<CinemachineCamera>();
            StartCoroutine(CamSizeCoroutine());
        }

        private IEnumerator CamSizeCoroutine()
        {
            yield return new WaitForSeconds(0.22f);
            _cam.Lens.OrthographicSize = 20.5f;
            yield return new WaitForSeconds(0.22f);
            _cam.Lens.OrthographicSize = 20;
            StartCoroutine(CamSizeCoroutine());
        }

    }
}
