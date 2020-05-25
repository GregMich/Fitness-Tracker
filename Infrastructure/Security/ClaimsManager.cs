using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Tracker.Infrastructure.Security
{
    public class ClaimsManager: IClaimsManager
    {
        private Dictionary<string, string> _claimsDict;
        private bool isInitialized = false;

        public ClaimsManager()
        {
            _claimsDict = new Dictionary<string, string>();
        }

        // the constructor is accessed before a user claim can be supplied because the claims
        // manager is injected by the service provider. For this reason I used a seperate 
        // method to set the object state with a claims principal object
        public void Init(System.Security.Claims.ClaimsPrincipal user)
        {
            user.Claims.ToList()
                .ForEach(_ => _claimsDict.Add(_.Type, _.Value));

            isInitialized = true;
        }

        public int GetUserIdClaim() =>
           isInitialized
            ? int.Parse(_claimsDict["UserId"])
            : throw new InvalidOperationException(
                "The claims manager was not initialized with a user claims principal");

        // verify that the supplied userId matches the userId in the JWT
        public bool VerifyUserId(int userId) =>
            isInitialized
            ? this.GetUserIdClaim() == userId
            : throw new InvalidOperationException(
                "The claims manager was not initialized with a user claims principal");
    }
}
