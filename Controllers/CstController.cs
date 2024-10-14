using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;


namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CstController : ControllerBase
    {
        private readonly ICSTRepo _cst;
        private readonly IEmailService _emailService;

        public CstController(ICSTRepo cst,IEmailService emailService)
        {

            _cst=cst;
            _emailService=emailService;
            
        }

        [EnableCors("AllowAll")]
        [HttpGet]
        [Route("/api/[controller]/getall")]
        public async Task<IActionResult> GetAll(){

            var cst= await _cst.GetAllAsync();
             


            return Ok(cst);

        }



        [EnableCors("AllowAll")]
        [HttpGet]
        [Route("/api/[controller]/last")]
         public async Task<IActionResult> getLast(){

            var cst=await _cst.GetLastAsync();

            return Ok(cst);
        }
        
    }
}


 
