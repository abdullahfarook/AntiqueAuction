namespace AntiqueAuction.Shared.Exceptions
{
    public class ConflictException:AntiqueAuctionException
    {
        public ConflictException(string message) : base(message, 409)
        {
        }
    }
}
