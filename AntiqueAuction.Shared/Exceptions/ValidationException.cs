using System.Collections.Generic;
using System.Linq;

namespace AntiqueAuction.Shared.Exceptions
{
    public class ValidationException:AntiqueAuctionException
    {
        public ValidationException(IEnumerable<string> msgList) : 
            base("Model is not valid because " + string.Join(", ", msgList.ToArray()), 400) { }
    }
}
