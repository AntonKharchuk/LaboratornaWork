using LaboratornaWork.Models;

using System.Data.SqlClient;

namespace LaboratornaWork.Data
{
    public class DataAccess
    {
        private string connectionString;

        public DataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Image> GetImages()
        {
            List<Image> images = new List<Image>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Images";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        images.Add(new Image
                        {
                            ImageID = (int)reader["ImageID"],
                            ListingID = (int)reader["ListingID"],
                            ImagePath = reader["ImagePath"].ToString(),
                            ImageAlt = reader["ImageAlt"].ToString()
                        });
                    }
                }
            }

            return images;
        }

        public List<Listing> GetListings()
        {
            List<Listing> listings = new List<Listing>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Listings";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listings.Add(new Listing
                        {
                            ListingID = (int)reader["ListingID"],
                            HLCN = reader["HLCN"].ToString(),
                            HousePrice = reader["HousePrice"].ToString(),
                            Currency = reader["Currency"].ToString(),
                            Bathrooms = (int)reader["Bathrooms"],
                            Bedrooms = (int)reader["Bedrooms"],
                            HomeLocation = reader["HomeLocation"].ToString(),
                            ContactName = reader["ContactName"].ToString(),
                            EmailContact = reader["EmailContact"].ToString(),
                            PhoneContact = reader["PhoneContact"].ToString(),
                            Address = reader["Address"].ToString(),
                            SquareFeet = reader["SquareFeet"].ToString(),
                            YearBuilt = reader["YearBuilt"].ToString(),
                            LotSize = reader["LotSize"].ToString(),
                            Garage = reader["Garage"].ToString(),
                            Notes = reader["Notes"].ToString()
                        });
                    }
                }
            }

            return listings;
        }
    }

}
