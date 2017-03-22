using CYInfo.CMK.Resources.Constants;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CYInfo.CMK.Helper_Code.Common
{
    /// <summary>
    /// Authorization for web API class.
    /// </summary>
    public class AuthorizationHeaderHandler : DelegatingHandler
    {
        #region Send method.

        /// <summary>
        /// Send method.
        /// </summary>
        /// <param name="request">Request parameter</param>
        /// <param name="cancellationToken">Cancellation token parameter</param>
        /// <returns>Return HTTP response.</returns>
        //protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        //{
        //    // Initialization.
        //    IEnumerable<string> apiKeyHeaderValues = null;
        //    AuthenticationHeaderValue authorization = request.Headers.Authorization;
        //    string userName = null;
        //    string password = null;

        //    // Verification.
        //    if (request.Headers.TryGetValues(ApiInfo.API_KEY_HEADER, out apiKeyHeaderValues) &&
        //        !string.IsNullOrEmpty(authorization.Parameter))
        //    {
        //        var apiKeyHeaderValue = apiKeyHeaderValues.First();

        //        // Get the auth token
        //        string authToken = authorization.Parameter;

        //        // Decode the token from BASE64
        //        string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));

        //        // Extract username and password from decoded token
        //        userName = decodedToken.Substring(0, decodedToken.IndexOf(":"));
        //        password = decodedToken.Substring(decodedToken.IndexOf(":") + 1);

        //        // Verification.
        //        if (apiKeyHeaderValue.Equals(ApiInfo.API_KEY_VALUE) &&
        //            userName.Equals(ApiInfo.USERNAME_VALUE) &&
        //            password.Equals(ApiInfo.PASSWORD_VALUE))
        //        {
        //            // Setting
        //            var identity = new GenericIdentity(userName);
        //            SetPrincipal(new GenericPrincipal(identity, null));
        //        }
        //    }

        //    // Info.
        //    return base.SendAsync(request, cancellationToken);
        //}


        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Initialization.
            IEnumerable<string> apiKeyHeaderValues = null;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;
            string userName = null;
            string password = null;

            // Verification.
            if (request.Headers.TryGetValues("X-ApiKey", out apiKeyHeaderValues) &&
                !string.IsNullOrEmpty(authorization.Parameter))
            {
                var apiKeyHeaderValue = apiKeyHeaderValues.First().Split(':');

                if (apiKeyHeaderValue.Length == 2)
                {
                    var appID = apiKeyHeaderValue[0];
                    var AppKey = apiKeyHeaderValue[1];

                    if (IsValidatedAppKey(appID, AppKey))
                    {
                        // Get the auth token
                        string authToken = authorization.Parameter;

                        // Decode the token from BASE64
                        string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));

                        // Extract username and password from decoded token
                        userName = decodedToken.Substring(0, decodedToken.IndexOf(":"));
                        password = decodedToken.Substring(decodedToken.IndexOf(":") + 1);

                        // Verification.
                        //if (apiKeyHeaderValue.Equals(ApiInfo.API_KEY_VALUE) &&
                        //    userName.Equals(ApiInfo.USERNAME_VALUE) &&
                        //    password.Equals(ApiInfo.PASSWORD_VALUE))
                        if (IsPasswordMatched(appID, userName, password))  
                        {
                            // Setting
                            var identity = new GenericIdentity(userName);
                            SetPrincipal(new GenericPrincipal(identity, null));
                        }
                    }
                }
                else
                {
                    return requestCancel(request, cancellationToken, "InvalidToken");
                }
            }

            // Info.
            return base.SendAsync(request, cancellationToken);
        }


        private System.Threading.Tasks.Task<HttpResponseMessage> requestCancel(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken, string message)
        {
            CancellationTokenSource _tokenSource = new CancellationTokenSource();
            cancellationToken = _tokenSource.Token;
            _tokenSource.Cancel();
            HttpResponseMessage response = new HttpResponseMessage();

            response = request.CreateResponse(HttpStatusCode.BadRequest);
            response.Content = new StringContent(message);
            return base.SendAsync(request, cancellationToken).ContinueWith(task =>
            {
                return response;
            });
        }



        private bool IsValidatedAppKey(string appid, string appkey)
        {
            bool isValidated = false;

            try
            {
                DefaultMongoDb logDB = new DefaultMongoDb("Partners");
                var msmqRecordColl = logDB.database.GetCollection("ApiKeys");

                List<IMongoQuery> qryList = new List<IMongoQuery>();

                qryList.Add(Query.EQ("appid", appid));
                qryList.Add(Query.NE("status", 0));

                IMongoQuery query = Query.And(qryList);

                var entity = msmqRecordColl.FindOne(query);
                if (entity != null)
                {
                    if (entity["appkey"].ToString() == appkey)
                    {
                        isValidated = true;
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return isValidated;
        }

        private bool IsPasswordMatched(string appid, string username, string password)
        {
            bool isValidated = false;

            try
            {
                DefaultMongoDb logDB = new DefaultMongoDb("Partners");
                var msmqRecordColl = logDB.database.GetCollection("ApiUserInfo");

                List<IMongoQuery> qryList = new List<IMongoQuery>();

                qryList.Add(Query.EQ("appid", appid));
                qryList.Add(Query.NE("status", 0));

                IMongoQuery query = Query.And(qryList);

                var entity = msmqRecordColl.FindOne(query);
                if (entity != null)
                {
                    if (entity["username"].ToString() == username && entity["password"].ToString() == password)
                    {
                        isValidated = true;
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return isValidated;
        }


        #endregion

        #region Set principal method.

        /// <summary>
        /// Set principal method.
        /// </summary>
        /// <param name="principal">Principal parameter</param>
        private static void SetPrincipal(IPrincipal principal)
        {
            // setting.
            Thread.CurrentPrincipal = principal;

            // Verification.
            if (HttpContext.Current != null)
            {
                // Setting.
                HttpContext.Current.User = principal;
            }
        }

        #endregion
    }
}