using System.Collections.Generic;
using UnityEngine.SceneManagement;
using BIS.Rewinds;
using UnityEngine;

namespace BIS.Managers
{
    public class RewindManager
    {
        private List<Rewind> _rewindList = new List<Rewind>();


        public RewindManager()
        {
            SceneManager.sceneLoaded += HandleSceneChange;
        }
        ~RewindManager()
        {
            SceneManager.sceneLoaded -= HandleSceneChange;
        }
        private void HandleSceneChange(Scene arg0, LoadSceneMode arg1)
        {
            //_rewindList.Clear();
        }


        public void AddRewind(Rewind rewind)
        {
            _rewindList.Add(rewind);
        }


        public void AllRewind()
        {
            for (int i = 0; i < _rewindList.Count; ++i)
            {
                if (_rewindList[i].IsRewindObject == false)
                    continue;
                _rewindList[i].StartRewind();
            }
        }

        public void AllSlow()
        {
            for (int i = 0; i < _rewindList.Count; ++i)
            {
                if (_rewindList[i].IsRewindObject == false)
                    continue;
                _rewindList[i].SlowObejct();
            }
        }
    }
}
