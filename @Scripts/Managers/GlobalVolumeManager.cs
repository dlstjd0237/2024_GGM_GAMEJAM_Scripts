using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static BIS.Core.Define;
using System;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

namespace BIS.Managers
{
    public class GlobalVolumeManager
    {
        private Dictionary<ESceneType, VolumeProfile> _volumeProfileDctionary = new Dictionary<ESceneType, VolumeProfile>();


        public void AddVolumeDictionary(ESceneType currentSceneType, VolumeProfile volume)
        {
            _volumeProfileDctionary.Add(currentSceneType, volume);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentScene"></param>
        /// <param name="intensity">min = 0, max = 1</param>
        /// <param name="duration"></param>
        /// <param name="ease"></param>
        public void SetChromaticAberration(ESceneType currentScene, float intensity, float duration = 0, bool returnToValue = false, Ease ease = Ease.Linear)
        {
            float defualtValue;
            if (_volumeProfileDctionary[currentScene].TryGet<ChromaticAberration>(out var volume))
            {
                defualtValue = volume.intensity.value;

                if (duration == 0)
                    volume.intensity.value = intensity;
                else
                {
                    DOTween.To(() => volume.intensity.value,
                     x => volume.intensity.value = x,
                     intensity,
                     duration)
                 .SetEase(ease).OnComplete(() =>
                 {
                     if (returnToValue == true)
                         DOTween.To(() => volume.intensity.value,
                    x => volume.intensity.value = x,
                    defualtValue,
                    duration);
                 });
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentScene"></param>
        /// <param name="intensity">min = -1, max 1</param>
        /// <param name="duration"></param>
        /// <param name="ease"></param>
        public void SetLensDistortion(ESceneType currentScene, float intensity, float duration = 0, bool returnToValue = false, Ease ease = Ease.Linear)
        {
            float defualtValue;
            if (_volumeProfileDctionary[currentScene].TryGet<LensDistortion>(out var volume))
            {
                defualtValue = volume.intensity.value;
                if (duration == 0)
                    volume.intensity.value = intensity;
                else
                {
                    DOTween.To(() => volume.intensity.value,
                     x => volume.intensity.value = x,
                     intensity,
                     duration)
                 .SetEase(ease).OnComplete(() =>
                 {
                     if (returnToValue == true)
                         DOTween.To(() => volume.intensity.value,
                    x => volume.intensity.value = x,
                    defualtValue,
                    duration);
                 });
                }
            }
        }




    }

}
