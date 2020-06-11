using System;
using System.Collections.Generic;
using TGIT.ACME.Protocol.Model;

namespace TGIT.ACME.Protocol.Services
{
    public class DefaultAuthorizationFactory : IAuthorizationFactory
    {
        public void CreateAuthorizations(Order order)
        {
            if (order is null)
                throw new System.ArgumentNullException(nameof(order));

            foreach(var identifier in order.Identifiers)
            {
                var authorization = new Authorization(order, identifier, DateTimeOffset.UtcNow.AddDays(2)); //TODO : set useful expiry;
                CreateChallenges(authorization);
            }
        }

        private void CreateChallenges(Authorization authorization)
        {
            _ = new Challenge(authorization, ChallengeTypes.Dns01, "abcdefg"); //TODO: Generate Token
            if (!authorization.IsWildcard)
                _ = new Challenge(authorization, ChallengeTypes.Http01, "acfsdfsd"); //TODO: Generate Token
        }
    }
}
