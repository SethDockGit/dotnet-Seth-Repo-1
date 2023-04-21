using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BnbProject.Logic;
using BnbProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BnbWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BnbController : ControllerBase
    {
        public Manager Manager { get; set; }
        public BnbController()
        {
            var managerFactory = new ManagerFactory();
            Manager = managerFactory.GetManager();
        }

        [HttpGet]
        [Route("listings")]
        public GetListingsResponse GetListings()
        {
            GetListingsResponse response = Manager.GetListings();

            return response;
        }
        [HttpGet]
        [Route("listing/{id}")]
        public ListingResponse GetListing(int id)
        {
            ListingResponse response = Manager.GetListingById(id);

            return response;
        }
        [HttpGet]
        [Route("user/{id}")]     
        public UserResponse GetUser(int id)
        {
            UserResponse response = Manager.GetUserById(id);

            return response;
        }
        [HttpGet]
        [Route("amenities")]
        public AmenitiesResponse GetAmenities()
        {
            AmenitiesResponse response = Manager.GetAmenities();

            return response;
        }
        [HttpPost]
        [Route("addlisting")]
        public ListingResponse AddListing([FromBody] AddListingTransfer transfer)
        {
            ListingResponse response = Manager.AddListing(transfer);

            return response;
        }
        [HttpPost]
        [Route("file/{id}")]
        public WorkflowResponse AddFilesToListing([FromForm] IFormFile file, int id)
        {
            WorkflowResponse response = Manager.AddFileToListing(file, id);
     
            return response;
        }
        [HttpPost]
        [Route("editlisting")]
        public EditListingResponse EditListing([FromBody] EditListingTransfer transfer)
        {
            EditListingResponse response = Manager.EditListing(transfer);

            return response;
        }
        [HttpPost]
        [Route("deletePicIds")]
        public DeletePicsResponse DeletePicsById([FromBody]int[] deletePicIds)
        {
            DeletePicsResponse response = Manager.DeletePicsById(deletePicIds);

            return response;
        }
        [HttpDelete]
        [Route("deletelisting/{id}/{userId}")]
        public DeleteListingResponse DeleteListing(int id, int userId)
        {
            DeleteListingResponse response = Manager.DeleteListing(id, userId);
            
            return response;
        }
        [HttpPost]
        [Route("addstay")]
        public BookingResponse AddStay([FromBody] StayTransfer transfer)
        {
            BookingResponse response = Manager.AddStay(transfer);

            return response;
        }
        [HttpPost]
        [Route("review")]
        public AddReviewResponse AddReview([FromBody] Review review)
        {
            AddReviewResponse response = Manager.AddReview(review);

            return response;
        }
        [HttpPost]
        [Route("newaccount")]
        public UserResponse CreateAccount([FromBody] CreateAccountRequest request)
        {
            UserResponse response = Manager.CreateAccount(request);

            return response;
        }
        [HttpPost]
        [Route("authenticate")]
        public UserResponse Authenticate([FromBody] AuthenticationRequest request)
        {   
            UserResponse response = Manager.Authenticate(request);

            return response;
        }
        [HttpPost]
        [Route("favorite")]
        public WorkflowResponse AddFavorite([FromBody] UserListing ul)
        {
            WorkflowResponse response = Manager.AddFavorite(ul);

            return response;
        }
        [HttpPost]
        [Route("removefavorite")]
        public WorkflowResponse RemoveFavorite([FromBody] UserListing ul)
        {
            WorkflowResponse response = Manager.RemoveFavorite(ul);

            return response;
        }
    }
}
