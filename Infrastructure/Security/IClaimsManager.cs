using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Tracker.Infrastructure.Security
{
    public interface IClaimsManager
    {
        public int GetUserIdClaim();

        public void Init(System.Security.Claims.ClaimsPrincipal user);

        public bool VerifyUserId(int userId);
    }
}
