namespace AntiqueAuction.Shared.Exceptions
{
    public class UnprocessableException:AntiqueAuctionException
    {
        public UnprocessableException(string message) : base(message, 422)
        {
        }
    }
}
