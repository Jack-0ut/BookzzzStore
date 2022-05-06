using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    public class Autorization
    {
        public static int logUser
        {
            get;
            set;
        }
        public int logCheck(string loginText,string passwordText)
        {
            logUser = 0;
            if ((loginText == "Solovey" || loginText == "Ashenafi") && (passwordText == "12345"))
            {
                logUser = 1;
            }
            return logUser;
        }

    }
}
