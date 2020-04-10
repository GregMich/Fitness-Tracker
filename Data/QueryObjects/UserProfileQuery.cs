using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness_Tracker.Data.Contexts;
using Fitness_Tracker.Data.Entities;
using Fitness_Tracker.Data.DataTransferObjects;

namespace Fitness_Tracker.Data.QueryObjects
{
    public static class UserProfileQuery
    {
        public static IQueryable<UserProfileDTO> GetUserProfile(
            this IQueryable<User> users) =>
                users
                .Select(user => new UserProfileDTO
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Email = user.Email,
                    BirthDate = user.BirthDate
                });
    }
}
