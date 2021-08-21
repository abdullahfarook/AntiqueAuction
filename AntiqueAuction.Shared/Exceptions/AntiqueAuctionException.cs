using System;

namespace AntiqueAuction.Shared.Exceptions
{
    public class AntiqueAuctionException:Exception
    {
        // In order to differentiate between client exception and system exception 
        public readonly int Code;

        public AntiqueAuctionException(string message, int code) : base(message)
        {
            Code = code;
        }
        
    }
}
