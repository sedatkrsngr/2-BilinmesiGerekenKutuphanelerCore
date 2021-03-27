using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireApp.Web.Services
{
    public class MailSender : IEmailSender
    {
      
        public async Task Sender(string userId, string message)
        {//Dilersek send grid adlı bir site var videoda var o şekilde ücretsiz mail atabiliriz ama gerek yok şuan
            string  başarılı =  userId + '-' + message;
        }

        public  static void SenderStatic(string userId, string message)//
        {
            string başarılı = userId + '-' + message;
        }
    }
}
