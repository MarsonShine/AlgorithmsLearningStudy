using System;
using System.Text;

namespace Encryptions
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = "123456789qwertyu";
            var token = Encoding.UTF8.GetBytes(key);
            //byte[] key = new byte[] { }
            var code = TotpAuthenticationService.GenerateCode(token).ToString("D6");

            Console.WriteLine($"Hello World! {code}");
        }
    }
}
