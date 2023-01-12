using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaterialsApp;
using MaterialsApp.Logic;
using MaterialsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaterialsController : ControllerBase
    {

        public Manager Manager { get; set; }


        [HttpGet]
        [Route("{username}")]
        public WorkflowResponse CheckResources(string username)
        {

            var managerFactory = new ManagerFactory();
            var manager = managerFactory.GetManager();

            WorkflowResponse response = manager.CheckResources(username);

            return response;

        }

        [HttpPost]
        [Route("{username}/{amount}")]
        public WorkflowResponse DepositResources([FromBody]ResourceType resource, string username, int amount)
        {

            var managerFactory = new ManagerFactory();
            var manager = managerFactory.GetManager();
            var user = manager.IDataSource.Authenticate(username);

            WorkflowResponse response = manager.DepositResource(user, resource, amount);

            return response;


        }
    }
}
