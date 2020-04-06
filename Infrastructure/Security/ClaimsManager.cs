using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Tracker.Infrastructure.Security
{
    public class ClaimsManager
    {
        private readonly Dictionary<string, string> _claimsDict;

        public ClaimsManager(System.Security.Claims.ClaimsPrincipal user)
        {
            _claimsDict = new Dictionary<string, string>();

            user.Claims.ToList()
                .ForEach(_ => _claimsDict.Add(_.Type, _.Value));
        }

        public int GetUserIdClaim() => int.Parse(_claimsDict["UserId"]);
    }
}
