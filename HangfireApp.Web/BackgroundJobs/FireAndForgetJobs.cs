using HangfireApp.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireApp.Web.BackgroundJobs
{
    public class FireAndForgetJobs// bu job tek seferde çalışır. Örn Kullanıcı kayıt olduktan sonra hemen gönderir
    {
        public static  void EmailSendToUserJob(string userId,string message)
        {
            Hangfire.BackgroundJob.Enqueue<IEmailSender>(x => x.Sender(userId, message));//IEmailSender kullanan metodu getirir ama startuop ta tanımlama yapmamız gerekiyor

            //Hangfire.BackgroundJob.Enqueue(()=>MailSender.SenderStatic(userId, message));//Eğer interface kullanmasaydık ve sadece Static MailSender sınıfı kullansaydık böyle kullanırdık
        }
    }
}
