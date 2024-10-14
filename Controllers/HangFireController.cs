using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hangfire;
using api.Helper;
using Microsoft.AspNetCore.Cors;


namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HangFireController : ControllerBase
    {
        private readonly HttpCall _client;


        public HangFireController(HttpCall client)
        {

            _client=client;
            
        }

        [EnableCors("AllowAll")]
        [HttpGet]
        [Route("/api/[controller]/invokeHangfire")]
        public IActionResult InvokeHangfire(){

        RecurringJob.AddOrUpdate("CheckMutasil",()=>_client.GetMutasil(),"* * * * *");
        RecurringJob.AddOrUpdate("CheckCst",()=>_client.GetCst(),"* * * * *");




        return Ok();




        }





        
    }
}