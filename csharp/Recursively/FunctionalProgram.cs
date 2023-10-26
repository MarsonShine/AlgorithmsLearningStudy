using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recursively {
    public class FunctionalProgram {
        public delegate TFunction RecursiveLambda<TFunction>(Func<TFunction> self);
        public delegate TFunction RecursiveOperator<TFunction>(RecursiveOperator<TFunction> self);

        public static TFunction Y<TFunction>(RecursiveLambda<TFunction> lambda) {
            RecursiveOperator<TFunction> f = self => lambda(() => self(self));
            return f(f);
        }

        public static void Call() {
            var fib = Y<Func<int, int>>(self => i => {
                    if (i <= 2) {
                        return 1;
                    } else {
                        return self() (i - 1) + self() (i - 2);
                    }
                });
            
            Console.WriteLine(fib(10));
        }
    }
}