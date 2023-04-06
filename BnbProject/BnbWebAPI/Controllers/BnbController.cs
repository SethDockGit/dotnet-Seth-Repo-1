using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BnbProject.Logic;
using BnbProject.Models;
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
        public WorkflowResponse GetListings()
        {
            WorkflowResponse response = Manager.GetListings();

            return response;
        }
        [HttpGet]
        [Route("listing/{id}")]
        public WorkflowResponse GetListing(int id)
        {
            WorkflowResponse response = Manager.GetListingById(id);

            return response;
        }
        [HttpGet]
        [Route("user/{id}")]     
        public WorkflowResponse GetUser(int id)
        {
            WorkflowResponse response = Manager.GetUserById(id);

            return response;
        }
        [HttpGet]
        [Route("amenities")]
        public WorkflowResponse GetAmenities()
        {
            WorkflowResponse response = Manager.GetAmenities();

            return response;
        }
        [HttpPost]
        [Route("addlisting")]
        public WorkflowResponse AddListing([FromBody] ListingTransfer transfer)
        {

            WorkflowResponse response = Manager.AddListing(transfer);

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
        public WorkflowResponse CreateAccount([FromBody] CreateAccountRequest request)
        {
            WorkflowResponse response = Manager.CreateAccount(request);

            return response;
        }
        [HttpPost]
        [Route("authenticate")]
        public WorkflowResponse Authenticate([FromBody] AuthenticationRequest request)
        {
            WorkflowResponse response = Manager.Authenticate(request);

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
