using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace CMS.Models
{
    public static class IdentityExtensions
    {
        public static string GetFullName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("FullName");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetAvatar(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Avatar");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetAccountTypeName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("AccountType");
            return (claim != null) ? ((DomainClasses.AccountType)short.Parse(claim.Value)).GetAttribute<DisplayAttribute>().Name : string.Empty;
        }
        public static string GetExpireDate(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("ExpireDate");
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}