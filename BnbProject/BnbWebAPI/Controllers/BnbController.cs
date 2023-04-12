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
        public ListingResponse AddListing([FromBody] ListingTransfer transfer)
        {
            ListingResponse response = Manager.AddListing(transfer);

            return response;
        }
        [HttpPost]
        [Route("file")]
        public WorkflowResponse AddFilesToListing([FromForm] IFormFile file)
        {

            WorkflowResponse response = Manager.AddFileToListing(file);
     
            return response;
        }
        [HttpPost]
        [Route("editfile/{id}")]
        public WorkflowResponse EditListingFile([FromBody] ImageTransfer transfer, int id)
        {
            WorkflowResponse response = Manager.EditListingFile(transfer);

            return response;
        }
        [HttpPost]
        [Route("editlisting")]
        public WorkflowResponse EditListing([FromBody] ListingTransfer transfer)
        {

            WorkflowResponse response = Manager.EditListing(transfer);

            return response;
        }
        [HttpPost]
        [Route("addstay")]
        public WorkflowResponse AddStay([FromBody] StayTransfer transfer)
        {
            WorkflowResponse response = Manager.AddStay(transfer);

            return response;
        }
        [HttpPost]
        [Route("review")]
        public WorkflowResponse AddReview([FromBody] Review review)
        {
            WorkflowResponse response = Manager.AddReview(review);

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


    }
}
