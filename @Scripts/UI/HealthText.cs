using BIS.Entities;
using BIS.Managers;
using System;
using TMPro;
using UnityEngine;

namespace BIS.UI
{
    public class HealthText : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void OnEnable()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            Manager.GameScene.Player.GetCompo<EntityHealth>().HelathChangeEvent += HandleChangeEvent;
        }

        private void HandleChangeEvent(float value)
        {
            _text.text = $"HP : {(int)value}";
        }

        private void OnDisable()
        {

            Manager.GameScene.Player.GetCompo<EntityHealth>().HelathChangeEvent -= HandleChangeEvent;
        }
    }
}

