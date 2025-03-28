using BIS.Init;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BIS.Managers;
using BIS.Core;
using static BIS.Utility.Util;
using UnityEngine.SceneManagement;
using System;
namespace BIS.Rewinds
{
    /// <summary>
    /// Rewind 기능을 사용하기 위해서는 상속받아야 한다.
    /// </summary>
    public abstract class Rewind : InitBase
    {
        [SerializeField] protected Animator _animator;
        private List<KeyValuePair<float, Vector2>> _reWindPosList;
        private List<KeyValuePair<float, Quaternion>> _reWindFlipList;



        private bool _isRewindObject = true;
        public bool IsRewindObject
        {
            get => _isRewindObject;
            set => _isRewindObject = value;
        }

        private bool _isRewind;
        public bool IsRewind
        {
            get => _isRewind;
            set => _isRewind = value;
        }



        private float _startTime;
        private float _targetTime;

        public override bool Init()
        {
            if (base.Init() == false)
                return false;

            _reWindPosList = new List<KeyValuePair<float, Vector2>>();
            _reWindFlipList = new List<KeyValuePair<float, Quaternion>>();


            Manager.Rewind.AddRewind((Rewind)this);
            return true;
        }


        public virtual void StartRewind()
        {
            _isRewind = true;
            _startTime = Time.time;
            _targetTime = _startTime - (Define.RecoredTime / 2);
            _reWindPosList.Reverse();
            _reWindFlipList.Reverse();

            StartCoroutine(StartRewindCoroutine());
        }
        public virtual void RewindEnd()
        {
            _reWindPosList.Clear();
            _reWindFlipList.Clear();
        }

        protected virtual void Update()
        {
            if (_isRewind == true)
            {
                return;
            }
        }

        protected virtual void OnDisable()
        {
            StopAllCoroutines();
        }

        protected virtual void FixedUpdate()
        {
            if (_isRewind == true && gameObject.activeSelf)
            {
                Rewinding();
                return;
            }

            Recode();
        }
        private void Recode()
        {
            _reWindPosList.Add(new KeyValuePair<float, Vector2>(Time.time, gameObject.transform.position));
            _reWindFlipList.Add(new KeyValuePair<float, Quaternion>(Time.time, gameObject.transform.rotation));
        }
        private void Rewinding()
        {
            if (_reWindPosList.Count > 1 && Mathf.Approximately(_targetTime, _reWindPosList[0].Key) == false) //키값과 타겟시간이 비슷하지 않다면
            {
                transform.rotation = _reWindFlipList[0].Value;
                transform.position = _reWindPosList[0].Value;
                _reWindPosList.RemoveAt(0);
                _reWindPosList.RemoveAt(0);
                _reWindFlipList.RemoveAt(0);
                _reWindFlipList.RemoveAt(0);
            }
            else
            {
                Debug.Log("되돌리기 끝");
                _isRewind = false;
            }
        }

        private IEnumerator StartRewindCoroutine()
        {
            yield return new WaitForSeconds(3);
            RewindEnd();
        }

        public void SlowObejct()
        {
            UseSlow();
        }
        protected abstract void UseSlow();

    }
}
