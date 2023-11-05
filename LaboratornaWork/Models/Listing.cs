namespace LaboratornaWork.Models
{
    public class Listing
    {
        public int ListingID { get; set; }
        public string? HLCN { get; set; }
        public string? HousePrice { get; set; }
        public string? Currency { get; set; }
        public int Bathrooms { get; set; }
        public int Bedrooms { get; set; }
        public string? HomeLocation { get; set; }
        public string? ContactName { get; set; }
        public string? EmailContact { get; set; }
        public string? PhoneContact { get; set; }
        public string? Address { get; set; }
        public string? SquareFeet { get; set; }
        public string? YearBuilt { get; set; }
        public string? LotSize { get; set; }
        public string? Garage { get; set; }
        public string? Notes { get; set; }
    }
}
