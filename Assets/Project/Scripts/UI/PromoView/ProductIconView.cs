using UnityEngine;
using UnityEngine.UI;
using RedPanda.Project.Interfaces;
using TMPro;
using UnityEngine.EventSystems;
using System;
using System.Collections;

namespace RedPanda.Project.Assets.Project.Scripts.UI.PromoView
{
    public class ProductIconView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<IPromoModel> OnBuyClick;

        [SerializeField] private Image _productBack;
        [SerializeField] private Image _productIcon;

        [SerializeField] private TMP_Text _cost;
        [SerializeField] private TMP_Text _productName;

        [SerializeField] private float _clickScale = 1.04f;
        [SerializeField] private float _scaleDuaration = 0.2f;

        private IPromoModel _productModel;

        public void Init(IPromoModel model, string spritesFolder)
        {
            _productModel = model;

            _productBack.sprite = Resources.Load<Sprite>(spritesFolder + $"background_{model.Rarity.ToString().ToLower()}");
            _productIcon.sprite = Resources.Load<Sprite>(spritesFolder + model.GetIcon());
            _productName.text = model.Title.ToUpper();
            _cost.text = "x" + model.Cost.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            StartCoroutine(PlayScale());
            OnBuyClick?.Invoke(_productModel);
        }

        private IEnumerator PlayScale()
        {
            float targetScale = 1.04f;
            float halfTime = _scaleDuaration / 2;
            float elapsed = 0;

            while (elapsed < halfTime)
            {
                elapsed += Time.deltaTime;
                var scale = Mathf.Lerp(1, targetScale, elapsed / halfTime);
                transform.localScale = new Vector3(scale, scale, 1);
                yield return null;
            }

            elapsed = 0;
            while (elapsed < halfTime)
            {
                elapsed += Time.deltaTime;
                var scale = Mathf.Lerp(targetScale, 1, elapsed / halfTime);
                transform.localScale = new Vector3(scale, scale, 1);
                yield return null;
            }

            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
