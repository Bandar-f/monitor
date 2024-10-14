using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Mutasil;
using api.Interfaces;
using api.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MutasilController : ControllerBase
    {

        private readonly IMutasilRepo _mutasil;

        public MutasilController(IMutasilRepo mutasil)
        {
            _mutasil=mutasil;

            
        }

        [EnableCors("AllowAll")]
        [HttpGet]
        [Route("/api/[controller]/getall")]
        public async Task<IActionResult> GetAll(){

            var mutasil= await _mutasil.GetAllAsync();

            Console.WriteLine("From controller"+mutasil);

            return Ok(mutasil);

        }

        [EnableCors("AllowAll")]
        [HttpGet]
        [Route("/api/[controller]/last")]
        public async Task<IActionResult> getLast(){

            var mutasil=await _mutasil.GetLastAsync();

            return Ok(mutasil);
        }

        // [HttpPost]
        // public async Task<IActionResult> CreateAsync([FromBody] MutasilReq mutasil){

        //     var myMutasil= new Mutasil(
                

        //     );



        // }
        
    }
}