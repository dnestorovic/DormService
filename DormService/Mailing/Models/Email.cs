using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailing.Data
{
    public class Email
    {
        public Email(string to, string body, string subject)
        {
            To = to;
            Body = body;
            Subject = subject;
        }

        public string To { get; set; }

        public string Body { get; set; }
        public string Subject { get; set; }
    }
}
