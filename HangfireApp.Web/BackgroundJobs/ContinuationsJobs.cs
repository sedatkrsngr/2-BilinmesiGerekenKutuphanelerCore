using HangfireApp.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireApp.Web.BackgroundJobs
{
    public class ContinuationsJobs
    {

        public static void EmailSendToUserJob(string JobID, string message)
        {
             Hangfire.BackgroundJob.ContinueJobWith<IEmailSender>(JobID, x => x.Sender(JobID, message));//Delayed Jobstan sonra kullanılır. Delayed jobs string unieId dönderir geri
          
            //Hangfire.BackgroundJob.ContinueJobWith(JobID,() => MailSender.SenderStatic(JobID, message));//eğer static metoduuz olsaydı
        }
    }
}
