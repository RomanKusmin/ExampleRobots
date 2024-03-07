using System;
using System.Collections.Generic;
using EvaArchitecture._Services.UiServices.UiService._Bases;
using EvaArchitecture.EvaHelpers;
using TMPro;
using UnityEngine.UI;

namespace EvaArchitecture.Core.UiServices.UiService
{
    public class EvaText : BaseUiEvaControl
    {
        private Text _text;
        private TextMeshProUGUI _textMeshProUGUI;
        
        public override string GetContainerLabel() => "Subscribe";

        public override void Subscribe(bool mustSubscribe)
        {
            base.Subscribe(mustSubscribe);
            _list.SubscribeOnEvaEvents(mustSubscribe, OnEvent);
        }

        protected override void Awake()
        {
            base.Awake();

            _text = this.GetComponent<Text>();
            _textMeshProUGUI = this.GetComponent<TextMeshProUGUI>();
        }

        private void SetText(string text)
        {
            if (!_text.IsNull())
            {
                _text.text = text;
                return;
            }
            
            if (!_textMeshProUGUI.IsNull())
            {
                _textMeshProUGUI.text = text;
                return;
            }
        }
        
        private void OnEvent(Delegate raiseEvent, object model, List<object> results)
        {
            var str = model.IsNull() ? null : model.ToString();
            SetText(str);
        }
    }
}
