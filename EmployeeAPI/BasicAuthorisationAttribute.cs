using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace EmployeeAPI
{
    public class BasicAuthorisationAttribute:AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization==null) // This means client has not send credential yet
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                String AuthenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string DecodedAutheticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(AuthenticationToken));
                string[] userarray = DecodedAutheticationToken.Split(":");
                string username = userarray[0];
                string password = userarray[1];

                if (EmployeeSecurity.Login(username,password))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}