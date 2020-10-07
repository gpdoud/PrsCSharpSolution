using Microsoft.Data.SqlClient;

using PrsDbLib.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace PrsDbLib.Controllers {
    
    public class UsersController {

        public PrsConnection prsConnection { get; set; } = null;

        public UsersController(PrsConnection prsConnection) {
            this.prsConnection = prsConnection;
        }

        public List<User> GetUsers() {
            var sql = "SELECT * From Users;";
            var cmd = new SqlCommand(sql, prsConnection.sqlConnection);
            var users = new List<User>();
            var sqlDataReader = cmd.ExecuteReader();
            while(sqlDataReader.Read()) {
                var user = new User();
                
                user.Id = Convert.ToInt32(sqlDataReader["Id"]);
                user.Username = Convert.ToString(sqlDataReader["Username"]);
                user.Password = Convert.ToString(sqlDataReader["Password"]);
                user.Firstname = Convert.ToString(sqlDataReader["Firstname"]);
                user.Lastname = Convert.ToString(sqlDataReader["Lastname"]);
                user.Phone = Convert.ToString(sqlDataReader["PhoneNumber"]);
                user.Email = Convert.ToString(sqlDataReader["Email"]);
                user.IsReviewer = Convert.ToBoolean(sqlDataReader["IsReviewer"]);
                user.IsAdmin = Convert.ToBoolean(sqlDataReader["IsAdmin"]);
                
                users.Add(user);
            }
            sqlDataReader.Close();
            return users;
        }
    }
}
