
using RedPanda.Project.Interfaces;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RedPanda.Project.Assets.Project.Scripts.UI.PromoView
{
    public class ProductViewSection : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private ProductIconView _iconViewPrefab;
        [SerializeField] private TMP_Text _sectionTitle;

        public string Title
        {
            set { _sectionTitle.text = value; }
        }



        public void SetupIcons(List<IPromoModel> productModels, Action<IPromoModel> onBuyAction)
        {
            foreach (var product in productModels)
            {
                var icon = Instantiate(_iconViewPrefab, _content);
                icon.Init(product);
                icon.OnBuyClick += onBuyAction;
            }
        }
    }
}
