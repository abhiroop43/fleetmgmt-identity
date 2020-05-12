using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FleetMgmt.Identity.Domain.Dto;
using FluentValidation.Results;

namespace FleetMgmt.Identity.Services.Helper
{
public static class Utility
    {
        public static ServiceResponse GetValidationResponse(ValidationResult validationResult)
        {
            ServiceResponse response = new ServiceResponse() { Success = validationResult.IsValid };
            if (!validationResult.IsValid)
            {
                response.ErrorList = new List<ErrorMessage>();
                foreach (var error in validationResult.Errors)
                {
                    response.ErrorList.Add(new ErrorMessage

                    {
                        Error = error.ErrorCode,
                        Value = error.ErrorMessage
                    });
                }

            }
            return response;
        }

        /// <summary>
        /// Method added to provide first letter of each word to be upper case
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string TitleCase(this string input) {
           return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input?.ToLower() ?? string.Empty);
        }
        /// <summary>
        /// Method added to provide upper case letter to the word
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string UpperCase(this string input)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToUpper(input?.ToLower() ?? string.Empty);
        }
        /// <summary>
        /// Method added to lower the string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string LowerCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            return CultureInfo.CurrentCulture.TextInfo.ToLower(input?.ToLower());
        }

        public static async Task<bool> ValidateEmail(string userEmail)
        {
            bool isValidEmail = false;
            if (!string.IsNullOrEmpty(userEmail))
                isValidEmail = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$").IsMatch(userEmail);
            return await Task.Run(() => isValidEmail);
        }

        //public static Exchange.ExchangeService ConnectToLDAP(LoginRequest request, string url)
        //{
        //    Exchange.ExchangeService exchange = new Exchange.ExchangeService(Exchange.ExchangeVersion.Exchange2016);
        //    exchange.Credentials = new Exchange.WebCredentials(request.UserName, request.Password, "ADP");
        //    exchange.Url = new Uri(url);
        //    return exchange;
        //}
        //public static async Task<Exchange.NameResolutionCollection> GetContactFromActiveDirectory(LoginRequest request, string url,string userName)
        //{
        //    var connection = ConnectToLDAP(request, url);
        //    return await connection.ResolveName(userName, Exchange.ResolveNameSearchLocation.DirectoryOnly, true);
        //}
    }
}