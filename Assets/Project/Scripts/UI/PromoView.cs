using RedPanda.Project.Services.Interfaces;

namespace RedPanda.Project.UI
{
    public sealed class PromoView : View
    {

        public override void OnShow()
        {
            SetUp();
        }
        private void SetUp()
        {
            var promoService = Container.Locate<IPromoService>();
            var promosData  = promoService.GetPromos();
        }
    }
}