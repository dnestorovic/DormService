using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailing.Data
{
    public class Email
    {
        public string To { get; internal set; }

        public string Body { get; internal set; }
        public string Subject { get; internal set; }
    }
}
