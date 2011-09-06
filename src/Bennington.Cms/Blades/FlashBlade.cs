using System.Web;
using MvcTurbine;
using MvcTurbine.Blades;

namespace Bennington.Cms.Blades
{
    public class FlashBlade : Blade
    {
        public override void Spin(IRotorContext context)
        {
            Flash.SetNotificationFunction(value => HttpContext.Current.Session["Flash_Notification"] = value);
        }
    }
}