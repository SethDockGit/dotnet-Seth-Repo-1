using System;
using System.Collections.Generic;
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
        [HttpPost]
        [Route("addlisting")]
        public WorkflowResponse AddListing([FromBody] Listing listing)
        {
            WorkflowResponse response = Manager.AddListing(listing);

            return response;
        }

    }
}
