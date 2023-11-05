namespace LaboratornaWork.Models
{
    public class Image
    {
        public int ImageID { get; set; }
        public int ListingID { get; set; }
        public string? ImagePath { get; set; }
        public string? ImageAlt { get; set; }
    }
}
