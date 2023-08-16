using RedPanda.Project.Assets.Project.Scripts.UI.PromoView;
using RedPanda.Project.Interfaces;
using RedPanda.Project.Services;
using RedPanda.Project.Services.Interfaces;
using System.Linq;
using UnityEngine;
using TMPro;

namespace RedPanda.Project.UI
{
    public sealed class PromoView : View
    {
        [SerializeField] private ProductViewSection _iconView;
        [SerializeField] private Transform _sectionsParent;

        [SerializeField] private TMP_Text _currencyCounter;

        public override void OnShow()
        {
            Setup();
        }
        private void Setup()
        {
            var user = Container.Locate<IUserService>();
            user.OnUpdateCurrency += OnCurrencyUpdate;

            _currencyCounter.text = user.Currency.ToString();

            var promoService = Container.Locate<IPromoService>();
            var promosData = promoService.GetPromos();

            var chestData = promosData.Where(p => p.Type == Data.PromoType.Chest).OrderByDescending(p => p.Rarity).ThenByDescending(p => p.Cost).ToList();
            var section = Instantiate(_iconView, _sectionsParent);
            section.Title = "CHESTS";
            section.SetupIcons(chestData, OnBuyProduct);

            var inAppData = promosData.Where(p => p.Type == Data.PromoType.InApp).OrderByDescending(p => p.Rarity).ThenByDescending(p => p.Cost).ToList();
            section = Instantiate(_iconView, _sectionsParent);
            section.Title = "CRYSTALS";
            section.SetupIcons(inAppData, OnBuyProduct);

            var specialsData = promosData.Where(p => p.Type == Data.PromoType.Special).OrderByDescending(p => p.Rarity).ThenByDescending(p => p.Cost).ToList();
            section = Instantiate(_iconView, _sectionsParent);
            section.Title = "SPECIALS";
            section.SetupIcons(specialsData, OnBuyProduct);
        }

        private void OnCurrencyUpdate(int value)
        {
            _currencyCounter.text = value.ToString();
        }

        private void OnBuyProduct(IPromoModel model)
        {
            var user  = Container.Locate<IUserService>();
            if (user.HasCurrency(model.Cost))
            {
                user.ReduceCurrency(model.Cost);
                Debug.Log($"BUY SUCCSESS {model.Title}");
            }
            else
            {
                Debug.Log($"NOT ENOUGH CURRENCY TO BUY {model.Title}");
            }
        }

        private void OnDisable()
        {
            var user = Container.Locate<IUserService>();
            user.OnUpdateCurrency -= OnCurrencyUpdate;
        }
    }
}