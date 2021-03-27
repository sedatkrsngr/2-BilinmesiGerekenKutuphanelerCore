using Hangfire;
using HangfireApp.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireApp.Web.BackgroundJobs
{
    public class RecurringJobs
    {
        public static void EmailSendToUserJob(string userId, string message)
        {
              Hangfire.RecurringJob.AddOrUpdate<IEmailSender>("Mailgondermejob", x => x.Sender(userId, message),Cron.Minutely );//dakikada bir çalışacak cron yapısının inceleyebiliriz daha öğrenmek için

              //Hangfire.RecurringJob.AddOrUpdate( "Mailgondermejob",() =>MailSender.SenderStatic(userId, message), Cron.Minutely);//eğer static metoduuz olsaydı
        }
    }
}
