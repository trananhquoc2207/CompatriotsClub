//using System.Security.Claims;


//namespace CompatriotsClub.Application.Extensions
//{
//    public static class AccessTokenExtensions
//    {
//        public static Guid? GetUserId(this ClaimsPrincipal user)
//        {
//            if (user.HasClaim(c => c.Type.Equals(AuthConstants.ID)))
//            {
//                try
//                {
//                    var claim = user.Claims.FirstOrDefault(c => c.Type.Equals(AuthConstants.ID)).Value;
//                    return Guid.Parse(claim);
//                }
//                catch (Exception e)
//                {
//                    return null;
//                }
//            }
//            return null;
//        }

//        public static string? GetRole(this ClaimsPrincipal user)
//        {
//            if (user.HasClaim(c => c.Type.Equals(AuthConstants.ROLE)))
//            {
//                try
//                {
//                    var claim = user.Claims.FirstOrDefault(c => c.Type.Equals(AuthConstants.ROLE)).Value;
//                    return claim;
//                }
//                catch (Exception e)
//                {
//                    return null;
//                }
//            }
//            return null;
//        }
//    }
//}