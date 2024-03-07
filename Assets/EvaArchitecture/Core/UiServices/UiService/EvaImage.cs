using System;
using System.Collections.Generic;
using EvaArchitecture._Services.UiServices.UiService._Bases;
using EvaArchitecture.EvaHelpers;
using UnityEngine;
using UnityEngine.UI;

namespace EvaArchitecture.Core.UiServices.UiService
{
    public class EvaImage : BaseUiEvaControl
    {
        public override string GetContainerLabel() => "Subscribe";
        
        private Action<Image, Image> _onReceiveImage; // (_image, receivedImage)

        public Action<Image, Image> OnReceivedImage
        {
            get => _onReceiveImage;
            set => _onReceiveImage = value;
        }

        public override void Subscribe(bool mustSubscribe)
        {
            base.Subscribe(mustSubscribe);
            _list.SubscribeOnEvaEvents(mustSubscribe, OnEvent);
        }
        
        private void OnEvent(Delegate raiseEvent, object model, List<object> results)
        {
            var image = this.GetComponent<Image>();
            if (image.IsNull())
                return;

            if (model is Texture2D texture2D)
            {
                image.sprite = texture2D.ToSprite();
            }
            else if (model is Sprite sprite)
            {
                image.sprite = sprite;
            }
            else if (model is Image receivedImage)
            {
                image.sprite = receivedImage.sprite;
                _onReceiveImage?.Invoke(image, receivedImage);
            }
            else if (model is Color color)
            {
                image.color = color;
            }
        }
    }
}
