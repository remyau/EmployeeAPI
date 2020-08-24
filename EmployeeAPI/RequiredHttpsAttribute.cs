﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace EmployeeAPI
{
    public class RequiredHttpsAttribute: AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Found);
                actionContext.Response.Content = new StringContent("<p>use Https instead of Http</p>");
                UriBuilder urib = new UriBuilder(actionContext.Request.RequestUri);
                urib.Scheme = Uri.UriSchemeHttps;
                urib.Port = 44315;

                actionContext.Response.Headers.Location = urib.Uri;
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
        }
    }
}