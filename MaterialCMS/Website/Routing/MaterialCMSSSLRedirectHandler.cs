﻿using System.Web.Routing;
using MaterialCMS.Entities.Documents.Web;
using MaterialCMS.Helpers;
using MaterialCMS.Settings;

namespace MaterialCMS.Website.Routing
{
    public class MaterialCMSSSLRedirectHandler : IMaterialCMSRouteHandler
    {
        private readonly IGetWebpageForRequest _getWebpageForRequest;
        private readonly SiteSettings _siteSettings;

        public MaterialCMSSSLRedirectHandler(IGetWebpageForRequest getWebpageForRequest, SiteSettings siteSettings)
        {
            _getWebpageForRequest = getWebpageForRequest;
            _siteSettings = siteSettings;
        }

        public int Priority { get { return 9750; } }
        public bool Handle(RequestContext context)
        {
            var url = context.HttpContext.Request.Url;
            var scheme = url.Scheme;

            Webpage webpage = _getWebpageForRequest.Get(context);
            if (webpage == null)
                return false;
            if (webpage.RequiresSSL(context.HttpContext.Request, _siteSettings) && scheme != "https")
            {
                var redirectUrl = url.ToString().Replace(scheme + "://", "https://");
                context.HttpContext.Response.RedirectPermanent(redirectUrl);
                return true;
            }
            if (!webpage.RequiresSSL(context.HttpContext.Request, _siteSettings) && scheme != "http")
            {
                var redirectUrl = url.ToString().Replace(scheme + "://", "http://");
                context.HttpContext.Response.RedirectPermanent(redirectUrl);
                return true;
            }
            return false;
        }
    }
}