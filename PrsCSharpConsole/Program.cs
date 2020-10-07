using PrsDbLib;
using PrsDbLib.Controllers;
using PrsDbLib.Models;

using System;

namespace PrsCSharpConsole {
    class Program {
        static void Main(string[] args) {

            var prsconn = new PrsConnection("localhost", "PRS0");
            prsconn.Connect();

            var user = new User {
                Id = 0, Username = "zz", Password = "zz", Firstname = "zz", Lastname = "zz",
                Phone = "zz", Email = "zz", IsReviewer = true, IsAdmin = true
            };

            var usersCtrl = new UsersController(prsconn);

            //var recsAffected = usersCtrl.Insert(user);

            var users = usersCtrl.GetUsers();

            var user1 = usersCtrl.GetUser(9);
            user1.Firstname = "Noah";
            user1.Lastname = "Phence";

            var recsAffected = usersCtrl.Update(user1);

            prsconn.Disconnect();
        }
    }
}
