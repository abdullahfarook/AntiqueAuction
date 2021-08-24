using System;
using System.Collections.Generic;
using System.Linq;
using AntiqueAuction.Core.Events;
using AntiqueAuction.Shared.Domain;
using AntiqueAuction.Shared.Exceptions;

namespace AntiqueAuction.Core.Models
{
    public class Item:AggregateRoot
    {
       

        public string Name { get; protected set; }
        public double Price { get; protected set; }
        public bool IsActive { get; protected set; }

        public Guid SellerId { get; protected set; }
        public DateTime AuctionStart { get; protected set; }
        public DateTime AuctionEnd { get; protected set; }
        public string Image { get; protected set; }
        public DateTime InspectionStart{ get; protected set; }
        public DateTime InspectionEnd{ get; protected set; }
        public double LastBid { get; protected set; }
        public string Description { get; protected set; }
        public int Quantity { get; protected set; }

        public virtual User Seller { get; protected set; }
        public virtual ICollection<AutoBid> AutoBids { get; protected set; }
        public virtual ICollection<BidHistory> BidHistories { get; protected set; }

        
        protected Item(){}
        public Item(string name, double price, bool isActive, Guid sellerId, DateTime auctionStart,
            DateTime auctionEnd, string image, string description)
        {
            Name = name;
            Price = price;
            IsActive = isActive;
            SellerId = sellerId;
            AuctionStart = auctionStart;
            AuctionEnd = auctionEnd;
            Image = image;
            LastBid = 0;
            Quantity = 1;
            Description = description;
        }
        // ubiquitous language on outside 
        public void EnableAutoBid(User user, float incrementPerUnit)
            => CreateOrUpdateAutoBid(user, incrementPerUnit); 
        public void CreateOrUpdateAutoBid(User user, float incrementPerUnit)
        {
            var autoBid = AutoBids.FirstOrDefault(x => x.UserId == user.Id);

            if (autoBid is null)
            {
                autoBid = new AutoBid(this, user);
                AutoBids.Add(autoBid);
            }
            else
            {
                autoBid.EnableAutoBidding();
            }

            if (user.MaxBidAmount <= LastBid)
            {
                autoBid.DisableAutoBidding();
                throw new UnprocessableException("Max bid amount must be greater than Current Bid");
            }
            
            var maxBid = BidHistories
                .OrderByDescending(x => x.Amount)
                .FirstOrDefault();
            if(maxBid is {} && maxBid.UserId == user.Id)
                return;

            var amount = maxBid is {}? maxBid!.Amount + incrementPerUnit: Price + incrementPerUnit;
            try
            {
                PlaceBid(user, amount);
            }
            catch (UnprocessableException)
            {
                autoBid.DisableAutoBidding();
            }
            catch (ConflictException){}
        }
        public void PlaceBid(User user,double amount)
        {
            ValidateBiddingEligibility(user, amount);
            BidUsingHighestBidder(user, amount);
        }

        private void BidUsingHighestBidder(User currentUser, double amount,int highBidderOrder = 0)
        {
            // Check in AutoBid section, if there exist high bidder than current user
            var maxAutoBid = AutoBids.Where(x => x.UserId != currentUser.Id && x.IsActive)
                .OrderByDescending(x => x.User.MaxBidAmount).Skip(highBidderOrder).Take(1).FirstOrDefault();

            // if max-bidder has auto-bidding enabled
            if (maxAutoBid is { } && maxAutoBid.User.MaxBidAmount > currentUser.MaxBidAmount)
            {
                // store history of current user bidding 
                DrawMoneyAndMakeHistory(currentUser, amount);
                var autoBid = AutoBids.FirstOrDefault(x => x.UserId == currentUser.Id);
                autoBid?.DisableAutoBidding(); // because current user max bid is too low to carry anymore compared to max-bidder
                try
                {
                    // Bid using highest bidder reserves
                    var maxBidder = maxAutoBid.User;
                    var incrementedValue = amount + 1;
                    DrawMoneyAndMakeHistory(maxBidder, incrementedValue);
                    LastBid = incrementedValue;
                    RaiseDomainEvent(new BidPlaced(Id, maxBidder.Id, LastBid));
                    return;
                }
                catch (UnprocessableException)
                {
                    // if reserves are not enough, we disable his auto-bidding and check for next high bidder
                    maxAutoBid.DisableAutoBidding();
                    // disseminate the effect
                    NegateDrawMoneyAndMakeHistory(currentUser, amount);
                    BidUsingHighestBidder(currentUser, amount, ++highBidderOrder);
                    return;
                }
            }
            // means current user becomes highest bidder;
            maxAutoBid?.DisableAutoBidding();
            amount = maxAutoBid?.User?.MaxBidAmount+1 ?? amount;
            DrawMoneyAndMakeHistory(currentUser, amount);
            LastBid = amount;
            RaiseDomainEvent(new BidPlaced(Id, currentUser.Id, LastBid));
        }

        private void DrawMoneyAndMakeHistory(User user, double amount)
        {
            user.DrawMoney(amount - LastBid);
            BidHistories.Add(new BidHistory(this, user, amount));
        }
        private void NegateDrawMoneyAndMakeHistory(User user, double amount)
        {
            user.AddMoney(amount - LastBid);
            var bidHistory = BidHistories.LastOrDefault(x => x.UserId == user.Id);
            if(bidHistory is {}) BidHistories.Remove(bidHistory);
        }

        public void ExpireNow()
        {
            IsActive = false;
            RaiseDomainEvent(new AuctionExpired(Id));
        }
        private void ValidateBiddingEligibility(User user, double amount)
        {
            // Check if Bid hasn't started yet.
            if (AuctionStart > DateTime.UtcNow)
                throw new UnprocessableException($"Auction hasn't started yet against Id: {Id}");

            // Check if Bidding has ended
            if (AuctionEnd <= DateTime.UtcNow)
                throw new UnprocessableException($"Auction has ended against Id: {Id}");

            // Check if user has enough amount to spend on bidding
            if(user.WalletAmount-amount<0)
                throw new UnprocessableException($"User does not have enough money to spent upon auction");

            if (amount <= LastBid )
                throw new UnprocessableException("Amount must be greater than Last Bid Amount");
            if(amount <= Price)
                throw new UnprocessableException("Amount must be greater than Price of Item");

            // Check if last bidder is the user requesting for bidding
            var lastBidderHistory = BidHistories.OrderByDescending(x => x.CreatedOn).FirstOrDefault();
            if (lastBidderHistory is { } && lastBidderHistory.UserId == user.Id)
                throw new ConflictException(
                    "Cannot Bid because Highest bidder is same as person bidding right now");

        }


    }
}
