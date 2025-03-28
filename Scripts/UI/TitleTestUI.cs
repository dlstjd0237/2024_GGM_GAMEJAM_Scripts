using BIS.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace BIS
{

    public class TitleTestUI : MonoBehaviour
    {
        private UIDocument _doc;
        private Dictionary<ETitleButtonType, ButtonModelView> _titleButtons = new();
        private enum ETitleButtonType
        {
            start = 0,
            setting,
            exit
        }

        private void Awake()
        {
            _doc = GetComponent<UIDocument>();
            foreach (ETitleButtonType item in Enum.GetValues(typeof(ETitleButtonType)))
            {
                ButtonModelView button = new ButtonModelView(new UI.Data.ButtonData(_doc.rootVisualElement.Q<Button>($"title_{item}_btn")));
                button.RegisterEvent(Core.Define.EUIEventType.CLICK, HandleTitleButtonHide);
                _titleButtons.Add(item, button);
            }
        }

        private void HandleTitleButtonHide()
        {
            StartCoroutine(TitleButtonToggleCorutine());
        }

        private IEnumerator TitleButtonToggleCorutine()
        {

            for (int i = 0; i < 3; i++)
            {
                ButtonModelView button = _titleButtons[(ETitleButtonType)i];
                float defualtTop = button.defualtPos.y;
                _titleButtons[(ETitleButtonType)i].AddToClass("hide");
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
