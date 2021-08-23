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

        public void CreateOrUpdateAutoBid(User user, float incrementPerUnit, double maxBidAmount)
        {
            var autoBid = AutoBids.FirstOrDefault(x => x.UserId == user.Id);

            if (autoBid is null)
            {
                autoBid = new AutoBid(this, user, incrementPerUnit, maxBidAmount);
                AutoBids.Add(autoBid);
            }
            else
                autoBid.Update(incrementPerUnit, maxBidAmount);

            if(maxBidAmount<LastBid)
                return;
            
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

        private void BidUsingHighestBidder(User currentUser, double amount,int skip = 0)
        {
            // Check in AutoBid section, if there exist high bidder than current user
            var maxBid = AutoBids.Where(x => x.UserId != currentUser.Id && x.IsActive)
                .OrderByDescending(x => x.MaxBidAmount).Skip(skip).Take(1).FirstOrDefault();

            if (maxBid is { } && maxBid.MaxBidAmount+maxBid.IncrementPerUnit > amount)
            {
                // store history of current user bidding 
                BidHistories.Add(new BidHistory(this, currentUser, amount));
                RaiseDomainEvent(new BidPlaced(Id, currentUser.Id, amount));
                try
                {
                    // Bid using highest bidder reserves
                    var incrementedValue = amount + maxBid.IncrementPerUnit;
                    DrawMoneyAndMakeHistory(maxBid.User, incrementedValue);
                }
                catch (UnprocessableException)
                {
                    // if reserves are not enough, we disable his auto-bidding and check for next user
                    maxBid.DisableAutoBidding();
                    BidUsingHighestBidder(currentUser,amount,++skip);
                }
            }
            else // if there is no highest bidder, we consider current user to be highest
                DrawMoneyAndMakeHistory(currentUser, amount);
        }

        private void DrawMoneyAndMakeHistory(User user, double amount)
        {
            user.DrawMoney(amount - LastBid);
            LastBid = amount;
            BidHistories.Add(new BidHistory(this, user, LastBid));
            RaiseDomainEvent(new BidPlaced(Id, user.Id, LastBid));
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

            if (amount <= LastBid || amount <= Price)
                throw new UnprocessableException(
                    "Cannot Bid because amount is equal or less than Last Bid Amount");
            
            // Check if last bidder is the user requesting for bidding
            var lastBidderHistory = BidHistories.OrderByDescending(x => x.CreatedOn).FirstOrDefault();
            if (lastBidderHistory is { } && lastBidderHistory.UserId == user.Id)
                throw new ConflictException(
                    "Cannot Bid because Highest bidder is same as person bidding right now");

        }


    }
}
