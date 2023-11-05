namespace LaboratornaWork.Models
{
    
    public class SearchParameters
    {
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int NumBathrooms { get; set; }
        public int NumBedrooms { get; set; }
        public string? District { get; set; }
        public string? SortBy { get; set; }
    }
}
