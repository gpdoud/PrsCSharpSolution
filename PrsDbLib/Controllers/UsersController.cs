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

        public int Update(User user) {
            var sql = "UPDATE Users SET " +
                " Username = @Username, " +
                " Password = @Password, " +
                " Firstname = @Firstname, " +
                " Lastname = @Lastname, " +
                " PhoneNumber = @Phone, " +
                " Email = @Email, " +
                " IsReviewer = @IsReviewer, " +
                " IsAdmin = @IsAdmin " +
                " WHERE Id = @Id;";
            var cmd = new SqlCommand(sql, prsConnection.sqlConnection);
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Firstname", user.Firstname);
            cmd.Parameters.AddWithValue("@Lastname", user.Lastname);
            cmd.Parameters.AddWithValue("@Phone", user.Phone);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@IsReviewer", user.IsReviewer);
            cmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
            cmd.Parameters.AddWithValue("@Id", user.Id);
            return cmd.ExecuteNonQuery();
        }

        public int Insert(User user) {
            var sql = "INSERT Users (Username, Password, Firstname, Lastname, PhoneNumber, Email, IsReviewer, IsAdmin)" +
                       " VALUES (@Username, @Password, @Firstname, @Lastname, @Phone, @Email, @IsReviewer, @IsAdmin);";
            var cmd = new SqlCommand(sql, prsConnection.sqlConnection);
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Firstname", user.Firstname);
            cmd.Parameters.AddWithValue("@Lastname", user.Lastname);
            cmd.Parameters.AddWithValue("@Phone", user.Phone);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@IsReviewer", user.IsReviewer);
            cmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
            return cmd.ExecuteNonQuery();
        }

        private void DataReaderToUserInstance(User user, SqlDataReader sqlDataReader) {
            user.Id = Convert.ToInt32(sqlDataReader["Id"]);
            user.Username = Convert.ToString(sqlDataReader["Username"]);
            user.Password = Convert.ToString(sqlDataReader["Password"]);
            user.Firstname = Convert.ToString(sqlDataReader["Firstname"]);
            user.Lastname = Convert.ToString(sqlDataReader["Lastname"]);
            user.Phone = Convert.ToString(sqlDataReader["PhoneNumber"]);
            user.Email = Convert.ToString(sqlDataReader["Email"]);
            user.IsReviewer = Convert.ToBoolean(sqlDataReader["IsReviewer"]);
            user.IsAdmin = Convert.ToBoolean(sqlDataReader["IsAdmin"]);
        }

        public User GetUser(int Id) {
            var sql = "SELECT * From Users Where Id = @Id;";
            var cmd = new SqlCommand(sql, prsConnection.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", Id);
            var sqlDataReader = cmd.ExecuteReader();
            if(!sqlDataReader.HasRows)
                return null;
            sqlDataReader.Read();
            var user = new User();
            // duplicate lines pulled from here
            DataReaderToUserInstance(user, sqlDataReader);

            sqlDataReader.Close();
            return user;
        }

        public List<User> GetUsers() {
            var sql = "SELECT * From Users;";
            var cmd = new SqlCommand(sql, prsConnection.sqlConnection);
            var users = new List<User>();
            var sqlDataReader = cmd.ExecuteReader();
            while(sqlDataReader.Read()) {
                var user = new User();
                DataReaderToUserInstance(user, sqlDataReader);
                
                users.Add(user);
            }
            sqlDataReader.Close();
            return users;
        }
    }
}
