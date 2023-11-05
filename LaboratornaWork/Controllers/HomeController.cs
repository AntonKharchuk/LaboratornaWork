using LaboratornaWork.Data;
using LaboratornaWork.Models;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace LaboratornaWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private DataAccess _dataAccess;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;

            var connectionString = configuration["ConnectionStrings:DefaultConection"];

            ArgumentNullException.ThrowIfNull(connectionString);

            _dataAccess = new DataAccess(connectionString);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listings()
        {
            List<Listing> listings = _dataAccess.GetListings();

            return View(listings);
        }

        public IActionResult IndexWithIdSearch()
        {
            return View();
        }
      


        [HttpPost]
        public IActionResult SearchById(int id)
        {
            List<Listing> listings = _dataAccess.GetListings();

            foreach (var listing in listings)
            {
                if (listing.ListingID == id)
                {
                    var images = new List<Image> { };

                    foreach (var image in _dataAccess.GetImages())
                    {
                        if (image.ListingID == listing.ListingID)
                        {
                            images.Add(image);
                        }
                    }
                    var dataToView = new Tuple<Listing, List<Image>>(listing, images);

                    return View("ListingDetails", dataToView);
                }
            }
            return NotFound($"404 not Found \nThere is no Listing with id:{id}");
        }


        [HttpGet]
        public IActionResult SearchPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchByParameters(SearchParameters searchParams)
        {

            var results = SelectListingsMatchParams(searchParams);

            switch (searchParams.SortBy)
            {
                case "priceLowToHigh":
                    results = SortByPrice(results);
                    break;
                case "priceHighToLow":
                    results = SortByPrice(results);
                    results.Reverse();
                    break;
                case "index":
                    results = SortById(results);
                    break;

                default:
                    break;
            }
            List<Listing> SortByPrice(List<Listing> listings)
            {
                return listings.OrderBy(listing => GetPrice(listing.HousePrice!)).ToList();
            }
            List<Listing> SortById(List<Listing> listings)
            {
                return listings.OrderBy(listing => listing.ListingID).ToList();
            }
            return View("SearchResults", results);
        }


        private List<Listing> SelectListingsMatchParams(SearchParameters searchParams)
        {
            var result = new List<Listing>();   

            foreach (var listing in _dataAccess.GetListings())
            {
                int price = GetPrice(listing.HousePrice!);
                if (searchParams.MinPrice<= price
                    && searchParams.MaxPrice>=price
                    && searchParams.NumBathrooms <= listing.Bathrooms
                    && searchParams.NumBedrooms <= listing.Bedrooms)
                {
                    if ((searchParams.District == "district1" && listing.ListingID<5) |
                        (searchParams.District == "district2" && listing.ListingID>=5))
                    {
                        result.Add(listing);
                    }
                }
            }
           
            return result;
        }
        private int GetPrice(string price)
        {
            price = price.Replace(",", "");
            return int.Parse(price);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}