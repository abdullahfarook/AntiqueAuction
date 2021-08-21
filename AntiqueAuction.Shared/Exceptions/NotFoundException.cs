namespace AntiqueAuction.Shared.Exceptions
{
    public class NotFoundException:AntiqueAuctionException
    {
        private NotFoundException(string message,int code) : base(message,code) { }

        public static NotFoundException ForClient(string message) => new NotFoundException(message, 400);
        public static NotFoundException ForSystem(string message) => new NotFoundException(message,500);
    }
}
