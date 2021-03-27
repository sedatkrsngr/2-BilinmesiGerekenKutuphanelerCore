using HangfireApp.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireApp.Web.BackgroundJobs
{
    public class DelayedJobs//belirttiğini< süre içerisinde çalıştırır
    {
        public static string  EmailSendToUserJob(string userId, string message)
        {
           return  Hangfire.BackgroundJob.Schedule<IEmailSender>(x => x.Sender(userId, message),TimeSpan.FromSeconds(20));//20 saniye sonra çalışacak geriye unique string bir ıd döner

          // return Hangfire.BackgroundJob.Schedule(() =>MailSender.SenderStatic(userId, message), TimeSpan.FromSeconds(20));//eğer static metoduuz olsaydı
        }
    }
}
