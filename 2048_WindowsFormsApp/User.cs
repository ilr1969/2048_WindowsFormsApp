using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_WindowsFormsApp
{
    class User
    {
        public string user = "NewUser";
        public int score;

        public User(string u, int s)
        {
            user = u;
            score = s;
        }
    }
}
