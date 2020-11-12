using System;
using System.Runtime.Serialization;

namespace SpectoLogic.Blazor.MSTeams.Auth
{
    public class TokenErrorException : Exception
    {
        public TokenError TokenError { get; set; }

        public TokenErrorException()
        {
        }

        public TokenErrorException(TokenError error) : base(error.Error)
        {
            TokenError = error;
        }

        public TokenErrorException(TokenError error, Exception innerException) : base(error.Error, innerException)
        {
            TokenError = error;
        }

        protected TokenErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
