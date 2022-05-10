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
        public bool isAdmin(string loginText,string passwordText)
        {
            logUser = 0;
            if ((loginText == "s" || loginText == "Ashenafi") && (passwordText == "1"))
            {
                return true;
            }
            return false;
        }
    }
}
