using System;
using System.Web;
using Saturn72.Core;
using Saturn72.Core.Domain.Users;
using Saturn72.Core.Services.Authentication;
using Saturn72.Core.Services.Users;
using Saturn72.Extensions;

namespace Saturn72.Web.Framework
{
    public class WebWorkContext : IWorkContext
    {
        #region Const

        private const string UserCookieName = "Saturn72.user";

        #endregion

        #region Fields

        private readonly IAuthenticationService _authenticationService;
        private readonly HttpContextBase _httpContext;
        private readonly IUserService _userService;


        private User _cachedUser;

        #endregion

        #region ctor

        public WebWorkContext(IAuthenticationService authenticationService, HttpContextBase httpContext,
            IUserService userService)
        {
            _authenticationService = authenticationService;
            _httpContext = httpContext;
            _userService = userService;
        }

        #endregion

        /// <summary>
        /// Gets or sets the current customer
        /// </summary>
        public virtual User CurrentUser
        {
            get
            {
                if (_cachedUser != null)
                    return _cachedUser;

                User user = null;
                Func<User, bool> userNotExistFunc = u => u != null && !u.Deleted && u.Active;

                Guard.MustFollow(userNotExistFunc(user), () => { user = _authenticationService.GetAuthenticatedUser(); });
                Guard.MustFollow(userNotExistFunc(user), () => { user = GetUserFromCookie(); });
                Guard.MustFollow(userNotExistFunc(user), () => { user = user = _userService.InsertGuestUser(); });


                //validation
                if (!user.Deleted && user.Active)
                {
                    SetUserCookie(user.UserGuid);
                    _cachedUser = user;
                }

                return _cachedUser;
            }
            set
            {
                SetUserCookie(value.UserGuid);
                _cachedUser = value;
            }
        }

        protected virtual void SetUserCookie(Guid userGuid)
        {
            if (_httpContext == null || _httpContext.Response == null) return;

            var cookie = new HttpCookie(UserCookieName)
            {
                HttpOnly = true,
                Value = userGuid.ToString()
            };

            if (userGuid == Guid.Empty)
            {
                cookie.Expires = DateTime.Now.AddMonths(-1);
            }
            else
            {
                int cookieExpires = 24*365; //TODO make configurable
                cookie.Expires = DateTime.Now.AddHours(cookieExpires);
            }

            _httpContext.Response.Cookies.Remove(UserCookieName);
            _httpContext.Response.Cookies.Add(cookie);
        }

        private User GetUserFromCookie()
        {
            User result = null;
            var userCookie = GetUserCookie();
            if (userCookie != null && !String.IsNullOrEmpty(userCookie.Value))
            {
                Guid customerGuid;
                if (Guid.TryParse(userCookie.Value, out customerGuid))
                {
                    var userByCookie = _userService.GetUserByGuid(customerGuid);
                    if (userByCookie != null &&
                        //this customer (from cookie) should not be registered
                        !userByCookie.IsRegistered())
                        result = userByCookie;
                }
            }
            return result;
        }

        protected virtual HttpCookie GetUserCookie()
        {
            if (_httpContext == null || _httpContext.Request == null)
                return null;

            return _httpContext.Request.Cookies[UserCookieName];
        }

        public virtual bool IsAdmin { get; set; }
    }
}