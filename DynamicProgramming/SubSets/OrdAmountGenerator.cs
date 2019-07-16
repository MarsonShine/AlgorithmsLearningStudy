using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicProgramming
{
    public class OrdAmountGenerator
    {
        public static List<OrdAmount> Create(int count)
        {
            var ordamounts = new List<OrdAmount>(count);
            Random rd = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < count; i++)
            {
                ordamounts.Add(new OrdAmount
                {
                    OrdID = i + 1,
                    Amount = rd.Next(1, 10000) * 1.33M
                });
            }
            return ordamounts;
        }
    }
}