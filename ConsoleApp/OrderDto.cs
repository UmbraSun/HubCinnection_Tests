namespace ConsoleApp
{
    internal class OrderDto
    {
        public string Id { get; set; }

        public OrderStatus Status { get; set; }

        public string RKId { get; set; }

        public long Amount { get; set; }

        public string VenueId { get; set; }

        public string CurrencyId { get; set; }

        public string StationId { get; set; }

        public string ReasonId { get; set; }
    }
}
