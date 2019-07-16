using System;
using System.Collections.Generic;

namespace DynamicProgramming.SubSets {
    public class CountGenerator {
        public static List<int> Create (int count) {
            var ids = new List<int> (count);
            Random rd = new Random ((int) DateTime.Now.Ticks);
            for (int i = 0; i < count; i++) {
                ids.Add (rd.Next (1, 10000));
            }
            return ids;
        }
    }
}