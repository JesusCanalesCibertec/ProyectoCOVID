using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace net.royal.spring.framework.correo
{
    public class AuthMessageSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {

            Execute(email, subject, message).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(string email, string subject, string message)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                //do something here
            }
        }
    }
}
