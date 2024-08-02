using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLAdmin.Areas.Admin.Helpers
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedRoles;

        public CustomAuthorizeAttribute(params string[] roles)
        {
            this.allowedRoles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var userRole = httpContext.Session["Vaitro"] as string;
                if (allowedRoles.Contains(userRole))
                {
                    return true;
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/Dangnhap/Unauthorized"); // Đổi đường dẫn đến trang thông báo không được phép truy cập
            }
            else
            {
                //filterContext.Result = new RedirectResult("~/Dangnhap/Login");
            }
        }
    }
}
