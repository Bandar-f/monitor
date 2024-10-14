using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Model;


namespace api.Helper
{
    public  class HttpCall
    {

        private readonly IMutasilRepo _mutasilRepo;
        private readonly ICSTRepo _cstRepo;
        private readonly IEmailService _emailService;

        public HttpCall(IMutasilRepo mutasilRepo, ICSTRepo cstRepo,IEmailService emailService)
        {

            _mutasilRepo=mutasilRepo;
            _cstRepo=cstRepo;
            _emailService=emailService;

            
        }

       

        public  async Task<HttpResponseMessage> GetMutasil(){

            HttpResponseMessage data;

             
             try{
             var client= new HttpClient();
            client.BaseAddress=new Uri("https://mutasil.cst.gov.sa/");

             data=client.GetAsync("").Result;


            }catch(Exception e){

                  await _mutasilRepo.CreateAsync(new Mutasil{
                    StatusCode="Down",
                    timestamps=DateTime.Now

                });

                    await _emailService.SendEmailAsync("bbalawi@cst.gov.sa","Mutasil might be down","Mutasil Might be down");



                return null;


            }


            if("OK"==data.StatusCode.ToString()){

                await _mutasilRepo.CreateAsync(new Mutasil{
                    StatusCode="UP",
                    timestamps=DateTime.Now

                });
                

            }else{

                await _mutasilRepo.CreateAsync(new Mutasil{
                    StatusCode="Down",
                    timestamps=DateTime.Now

                });
                    await _emailService.SendEmailAsync("bbalawi@cst.gov.sa","Mutasil might be down","Mutasil might be down");


            }
    

             return data ;


        }

          public  async Task<HttpResponseMessage> GetCst(){


            HttpResponseMessage data;


          try{

             var client= new HttpClient();
            client.BaseAddress=new Uri("https://www.cst.gov.sa/en/Pages/default.aspx");

             data= client.GetAsync("").Result;

                 }catch(Exception err){

                    await _cstRepo.CreateAsync(new Cst{
                    StatusCode="Down",
                    timestamps=DateTime.Now

                });
                    await _emailService.SendEmailAsync("bbalawi@cst.gov.sa","CST Website might be down","CST Website might be down ");

                return null;

                 }

              if("OK"==data.StatusCode.ToString()){

                await _cstRepo.CreateAsync(new Cst{
                    StatusCode="UP",
                    timestamps=DateTime.Now

                });
                

            }else{

                await _cstRepo.CreateAsync(new Cst{
                    StatusCode="Down",
                    timestamps=DateTime.Now

                });
                await _emailService.SendEmailAsync("bbalawi@cst.gov.sa","CST Website might be down","CST Website might be down");


            }
    

             return data ;


        }
        
    }
}



//   public async Task<Stock> CreateAsync(Stock stockModel)
//         {

//             await _context.Stock.AddAsync(stockModel);
//             await _context.SaveChangesAsync();

//             return stockModel;

//         }