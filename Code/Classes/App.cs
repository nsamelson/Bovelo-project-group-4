using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Bovelo
{
    class App 
    {
        public List<User> userList;

        public App()
        {
            this.userList = new List<User>();
        }
        public void addNewUser(User user)
        {
            userList.Add(user);
        }
        public void addNewAdmin(User user)
        {
            user.isAdmin = true;
            userList.Add(user);
        }

    }
}