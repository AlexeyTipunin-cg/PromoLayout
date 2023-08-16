using UnityEngine;
using UnityEngine.UI;
using RedPanda.Project.Interfaces;
using TMPro;
using UnityEngine.EventSystems;
using System;

namespace RedPanda.Project.Assets.Project.Scripts.UI.PromoView
{
    public class ProductIconView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<IPromoModel> OnBuyClick;
        
        [SerializeField] private Image _productBack;
        [SerializeField] private Image _productIcon;

        [SerializeField] private TMP_Text _cost;
        [SerializeField] private TMP_Text _productName;

        private IPromoModel _productModel;


        public void Init(IPromoModel model)
        {
            _productModel = model;

            _productBack.sprite = Resources.Load<Sprite>("Sprites/background_" + model.Rarity.ToString().ToLower());
            _productIcon.sprite = Resources.Load<Sprite>("Sprites/" + model.GetIcon());
            _productName.text = model.Title.ToUpper();
            _cost.text = "x" + model.Cost.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnBuyClick?.Invoke(_productModel);
        }
    }
}
