using System;
using System.Collections.Generic;
using EvaArchitecture._Services.UiServices.UiService._Bases;
using UnityEngine.UI;

namespace EvaArchitecture.Core.UiServices.UiService
{
    public class EvaSlider : BaseUiEvaControl
    {
        private Slider _slider;

        public override string GetContainerLabel() => "Subscribe";

        public override void Subscribe(bool mustSubscribe)
        {
            base.Subscribe(mustSubscribe);
            _list.SubscribeOnEvaEvents(mustSubscribe, OnEvent);
        }

        protected override void Awake()
        {
            base.Awake();

            _slider = this.GetComponent<Slider>();
        }

        private void OnEvent(Delegate raiseEvent, object model, List<object> results)
        {
            if (model is int intValue)
            {
                _slider.value = intValue;
            }
            else if (model is float floatValue)
            {
                _slider.value = floatValue;
            }
            else if (model is ValueTuple<int, int> intValueTuple)
            {
                var (value, maxValue) = intValueTuple;
                _slider.value = value;
                _slider.maxValue = maxValue;
            }
            else if (model is ValueTuple<float, float> floatValueTuple)
            {
                var (value, maxValue) = floatValueTuple;
                _slider.value = value;
                _slider.maxValue = maxValue;
            }
        }
    }
}
