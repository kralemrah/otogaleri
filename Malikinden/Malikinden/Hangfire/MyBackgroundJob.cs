using Data;
using Malikinden.Utility;
using Microsoft.EntityFrameworkCore;

namespace Malikinden.Hangfire
{
    public class MyBackgroundJob
    {
        private readonly MalikindenDbContext _context;

        public MyBackgroundJob(MalikindenDbContext context)
        {
            _context = context;
        }

        private static readonly object _lockObject = new object();

        public async Task ProcessDataAsync()
        {
            lock (_lockObject)
            {
                //Thread.Sleep(1_000_000);
                var OpsiyonMailAtilacakEpostalar = _context.Opsiyonlar.Include(p => p.Musteri).Where(p =>
                (p.EpostaGonderildiMi == null || p.EpostaGonderildiMi == false) &&


                 DateTime.Now.AddHours(12) > p.OpsiyonBitisTarihi).ToList();

                foreach (var item in OpsiyonMailAtilacakEpostalar)
                {

                    //epostagonder
                    // Hotmail credentials
                    string email = "zortingero@gmail.com";
                    
                    string password = "dwlh bqvn lbzf ggzf";

                    // Email details
                    string recipient = item.Musteri.Email;
                    string subject = "Test Email from .NET 7 - " + DateTime.Now;
                    string body = "<h1>Hello, this is a test email! " + DateTime.Now + "</h1>";

                    // Create an instance of EpostaHelper and send email
                    var epostaHelper = new EpostaHelper(email, password);
                    epostaHelper.SendEmail(recipient, subject, body);

                    Console.WriteLine("Process completed.");

                    item.EpostaGonderildiMi = true;
                    _context.SaveChanges();

                }
            }

        }
    }
}