using UnityEngine;
using BIS.Core;
using UnityEngine.Rendering;
using BIS.Managers;
namespace BIS.Volumes
{
    public class SceneVolume : MonoBehaviour
    {
        [SerializeField] private Define.ESceneType _sceneType;
        private VolumeProfile _volumeProfile;

        private void Awake()
        {
            _volumeProfile = GetComponent<Volume>().profile;
            Manager.Volume.AddVolumeDictionary(_sceneType, _volumeProfile);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Manager.Volume.SetLensDistortion(_sceneType, Random.Range(-1f, 2f), 0.3f, true);
                Manager.Volume.SetChromaticAberration(_sceneType, Random.Range(0f, 2f), 0.3f, true);
            }
        }
    }
}
