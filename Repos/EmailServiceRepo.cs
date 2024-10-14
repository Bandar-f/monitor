using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;


namespace api.Repos
{
    public class EmailServiceRepo:IEmailService
    {

        private readonly SmtpClient _smtpClient;
        private readonly string _senderEmail;

        public EmailServiceRepo(IConfiguration config)
        {
            var emailSettings=config.GetSection("EmailSettings");
            _smtpClient=new SmtpClient(emailSettings["SmtpServer"],int.Parse(emailSettings["Port"])){
                Credentials = new NetworkCredential(emailSettings["SenderEmail"], emailSettings["Password"]),
                EnableSsl = true

            };

            _senderEmail=emailSettings["SenderEmail"];

            
        }


        public async Task SendEmailAsync(string recipient,string subject,string bdy){

            using var mailMessage=new MailMessage(_senderEmail,recipient,subject,bdy);
            try{
                await _smtpClient.SendMailAsync(mailMessage);
            }catch(Exception err){

                Console.WriteLine(err);
            }

        }



        
    }
}


 