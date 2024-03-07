using EvaArchitecture._Services.UiServices.UiService._Bases;
using EvaArchitecture.EvaHelpers;
using EvaArchitecture.Logger;
using UnityEngine.UI;

namespace EvaArchitecture.Core.UiServices.UiService
{
    public class EvaButton : BaseUiEvaControl
    {
        public override string GetContainerLabel() => "Publish";

        protected override void OnEnable()
        {
            base.OnEnable();

            if (!EnableButton(true))
            {
                Log.Error(() => $"UiButton, EnableButton(true) FAILED");
                return;
            }
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();

            if (!EnableButton(false))
            {
                Log.Error(() => $"UiButton, EnableButton(false) FAILED");
                return;
            }
        }

        private bool EnableButton(bool mustEnable)
        {
            var button = this.GetComponent<Button>();
            if (button.IsNull())
                return (bool)Log.Error(() => $"Button not found");

            if (mustEnable)
                button.onClick.AddListener(OnClick);
            else
                button.onClick.RemoveListener(OnClick);

            return true;
        }

        private void OnClick() => 
            _list.PublishEvaEvents(this);
    }
}
