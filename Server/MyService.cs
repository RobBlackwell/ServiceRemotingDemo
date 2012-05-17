using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Server
{
    //the service class implements the interface
    public class MyService : IMyContract
    {
        public int Add(int i, int j)
        {
            int k = i + j;
            Console.WriteLine("{0} + {1} = {2}", i, j, k);
            return k; 
        }
    }
}
