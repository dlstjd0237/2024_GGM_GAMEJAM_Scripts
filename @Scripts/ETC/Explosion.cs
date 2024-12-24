using BIS.Entities;
using UnityEngine;
using DG.Tweening;
using BIS.Managers;

namespace BIS
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private float _defualtScale = 0.25f;
        private void OnEnable()
        {
            transform.localScale = new Vector2(_defualtScale, _defualtScale);
            transform.DOScale(1.25f, 0.5f).SetEase(Ease.InOutBack);
            Manager.Camera.ShakeCamera(new Vector3(3, 3, 3), 4, 4, 0.2f);
        }

        public void AnimationEnd()
        {
            gameObject.SetActive(false);
        }


    }
}
