using PrsDbLib;
using PrsDbLib.Controllers;

using System;

namespace PrsCSharpConsole {
    class Program {
        static void Main(string[] args) {

            var prsconn = new PrsConnection("localhost", "PRS0");
            prsconn.Connect();

            var usersCtrl = new UsersController(prsconn);
            var users = usersCtrl.GetUsers();

            prsconn.Disconnect();
        }
    }
}
