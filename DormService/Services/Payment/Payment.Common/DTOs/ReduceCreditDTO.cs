using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Common.DTOs
{
    public class ReduceCreditDTO
    {
        public decimal credit { get; set; }

        public bool reduceCredit(decimal amount)
        {
            if (amount < 0 || amount > credit)
            {
                return false;
            }
            credit -= amount;
            return true;
        }
    }
}
