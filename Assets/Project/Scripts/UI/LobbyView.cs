using RedPanda.Project.Assets.Project.Scripts.UI;
using RedPanda.Project.Services;
using RedPanda.Project.Services.Interfaces;
using RedPanda.Project.Services.UI;
using UnityEngine;
using UnityEngine.UI;

namespace RedPanda.Project.UI
{
    public sealed class LobbyView : View
    {
        [SerializeField] private Button _startButton;
        private void Awake()
        {
            _startButton.onClick.AddListener(OnStartClick);
        }

        private void OnStartClick()
        {
            var uiService = Container.Locate<IUIService>();
            uiService.Close(gameObject.name);
            uiService.Show(GameViews.PROMO_VIEW);
        }
    }
}