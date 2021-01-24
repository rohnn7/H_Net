using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using H_Net.Data_Contract;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

namespace H_Net.Data_Access
{
    public class Data_Access
    {

        private string userID = "root";
        private string password = "cpr5063";
        private string server = "localhost";
        private string database = "h_net";
        private string connString = "";

        public Data_Access()
        {
            connString = "Persist Security Info=True;User Id=" + userID + "; password=" + password + ";server=" + server + ";database=" + database;
        }
        #region Authentication
        public bool authenticateUser(Param p)
        {
            return true;
        }
        #endregion
        #region Process Photo
        public string Processphoto(Param p, out int o_status, out string o_statusMessage, int in_transaction)
        {
            #region Process Photo
            bool success = false;
            int clientID = 0;
            string approved = "ATTENTION : Someone, in your area needs help, please look into this";
            string email = "";
            try
            {
                string pAddress = SavePhoto(p.Photo.PhotoString);
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("user_processphoto", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        MySqlParameter param_result = cmd.Parameters.Add("in_result", MySqlDbType.Int16);
                        MySqlParameter param_photoaddress = cmd.Parameters.Add("in_photoaddress", MySqlDbType.String);
                        MySqlParameter param_latitude = cmd.Parameters.Add("in_latitude", MySqlDbType.Decimal);
                        MySqlParameter param_latdirection = cmd.Parameters.Add("in_latdirection", MySqlDbType.String);
                        MySqlParameter param_longitude = cmd.Parameters.Add("in_longiude", MySqlDbType.Decimal);
                        MySqlParameter param_longdirection = cmd.Parameters.Add("in_longdirection", MySqlDbType.String);
                        MySqlParameter param_transaction = cmd.Parameters.Add("in_transaction", MySqlDbType.Int32);
                        MySqlParameter param_status = cmd.Parameters.Add("o_status", MySqlDbType.Int32);
                        MySqlParameter param_statusMessage = cmd.Parameters.Add("o_statusMessage", MySqlDbType.String);
                        param_photoaddress.Value = pAddress;
                        param_latitude.Value = p.Photo.Latitude;
                        param_latdirection.Value = p.Photo.Latdirection;
                        param_longitude.Value = p.Photo.Longitude;
                        param_longdirection.Value = p.Photo.Longdirection;
                        param_result.Value = p.Photo.Result;
                        param_status.Direction = System.Data.ParameterDirection.InputOutput;
                        param_statusMessage.Direction = System.Data.ParameterDirection.InputOutput;
                        param_transaction.Value = 1;
                        string messageTobeMailed = "";
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            o_status = Convert.ToInt32(param_status.Value);
                            o_statusMessage = Convert.ToString(param_statusMessage.Value);
                            if (o_status == 0)
                            {
                                success = true;
                                while (dr.Read())
                                {                                    
                                    email = dr.GetString(0);
                                    if (p.Photo.Result == 0)
                                        messageTobeMailed = "Photo not approved";
                                    else
                                        messageTobeMailed = "Photo approved";
                                    try
                                    {
                                        MailMessage message = new MailMessage();
                                        SmtpClient smtp = new SmtpClient();
                                        message.From = new MailAddress("vrohanrv7@gmail.com");
                                        message.To.Add(new MailAddress(email));
                                        message.Subject = "HELP!";
                                        message.IsBodyHtml = true; //to make message body as html  
                                        message.Body = messageTobeMailed;
                                        smtp.Port = 587;
                                        smtp.Host = "smtp.gmail.com"; //for gmail host  
                                        smtp.EnableSsl = true;
                                        smtp.UseDefaultCredentials = false;
                                        smtp.Credentials = new NetworkCredential("vrohanrv7@gmail.com", "cpr5063#");
                                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                        smtp.Send(message);
                                    }
                                    catch (Exception) { }
                                }
                                dr.Close();
                            }
                            else
                            {
                                success = false;
                                email = "";
                                messageTobeMailed = "Could not be processed";
                                try
                                {
                                    MailMessage message = new MailMessage();
                                    SmtpClient smtp = new SmtpClient();
                                    message.From = new MailAddress("vrohanrv7@gmail.com");
                                    message.To.Add(new MailAddress(p.Contact.Email));
                                    message.Subject = "HELP!";
                                    message.IsBodyHtml = true; //to make message body as html  
                                    message.Body = approved;
                                    smtp.Port = 587;
                                    smtp.Host = "smtp.gmail.com"; //for gmail host  
                                    smtp.EnableSsl = true;
                                    smtp.UseDefaultCredentials = false;
                                    smtp.Credentials = new NetworkCredential("vrohanrv7@gmail.com", "cpr5063#");
                                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    smtp.Send(message);
                                }
                                catch (Exception) { }
                            }
                        }
                    }
                    conn.Close();

                }
            }
            catch (MySqlException ex)
            {
                o_status = 1;
                o_statusMessage = ex.Message;
                success = false;
                clientID = 0;
            }
            return email;
            #endregion
        }
        #endregion
        #region Register User 
        public int RegisterUser (Param p, out int o_status, out string o_statusMessage, int in_transaction)
        {
            bool success = false;
            int clientID = 0;
            try
            {
             
               using(MySqlConnection conn = new MySqlConnection(connString))
               {
                    conn.Open();
                    using(MySqlCommand cmd = new MySqlCommand("user_insertuserdetails", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        MySqlParameter param_name  = cmd.Parameters.Add("in_name", MySqlDbType.VarChar);
                        MySqlParameter param_age = cmd.Parameters.Add("in_age", MySqlDbType.Int32);
                        MySqlParameter param_volunteer = cmd.Parameters.Add("in_volunteer", MySqlDbType.Int32);
                        MySqlParameter param_email = cmd.Parameters.Add("in_email", MySqlDbType.VarChar);
                        MySqlParameter param_mobile = cmd.Parameters.Add("in_mobile", MySqlDbType.VarChar);
                        MySqlParameter param_username = cmd.Parameters.Add("in_username", MySqlDbType.VarChar);
                        MySqlParameter param_password = cmd.Parameters.Add("in_password", MySqlDbType.VarChar);
                        MySqlParameter param_add1 = cmd.Parameters.Add("in_add1", MySqlDbType.VarChar);
                        MySqlParameter param_add2 = cmd.Parameters.Add("in_add2", MySqlDbType.VarChar);
                        MySqlParameter param_city = cmd.Parameters.Add("in_cityname", MySqlDbType.VarChar);
                        MySqlParameter param_transaction = cmd.Parameters.Add("in_transaction", MySqlDbType.Int32);
                        MySqlParameter param_status = cmd.Parameters.Add("o_status", MySqlDbType.Int32);
                        MySqlParameter param_statusMessage = cmd.Parameters.Add("o_statusMessage", MySqlDbType.String);
                        param_name.Value = p.User.Name;
                        param_age.Value = p.User.Age;
                        param_volunteer.Value = p.User.Volunteer;
                        param_email.Value = p.Contact.Email;
                        param_mobile.Value = p.Contact.Mobile;
                        param_username.Value = p.User.Username;
                        param_password.Value = p.User.Password;
                        param_add1.Value = p.Address.Add1;
                        param_add2.Value = p.Address.Add2;
                        param_city.Value = p.Address.Cityname;
                        param_status.Direction = System.Data.ParameterDirection.InputOutput;
                        param_statusMessage.Direction = System.Data.ParameterDirection.InputOutput;
                        param_transaction.Value = 1;
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {

                            o_status = Convert.ToInt32(param_status.Value);
                            o_statusMessage = Convert.ToString(param_statusMessage.Value);
                            if (o_status == 0)
                            {
                                success = true;
                                while (dr.Read())
                                {
                                    clientID = dr.GetInt32(0);
                                }
                                dr.Close();
                            }
                            else
                            {
                                success = false;
                                clientID = 0;
                            }
                        }

                    }
                    conn.Close();
               }

            }
            catch (MySqlException ex)
            {
                o_status = 1;
                o_statusMessage = ex.Message;
                success = false;
                clientID = 0;
            }
            return clientID;
        }
        #endregion
        #region check User
        public bool checkUser(Param uc, out int o_status, out string o_statusMessage, int in_transaction)
        {
            #region check User
            bool success = false;
            int clientID = 0;
            try
            {
                //IN in_weight int, INOUT o_status int, INOUT o_statusMessage varchar(255),IN in_transaction INT)
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("user_checklogin", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        MySqlParameter param_userid = cmd.Parameters.Add("in_userid", MySqlDbType.Int32);
                        MySqlParameter param_loginname = cmd.Parameters.Add("in_username", MySqlDbType.String);
                        MySqlParameter param_password = cmd.Parameters.Add("in_password", MySqlDbType.String);
                        MySqlParameter param_transaction = cmd.Parameters.Add("in_transaction", MySqlDbType.Int32);
                        MySqlParameter param_status = cmd.Parameters.Add("o_status", MySqlDbType.Int32);
                        MySqlParameter param_statusMessage = cmd.Parameters.Add("o_statusMessage", MySqlDbType.String);
                        param_userid.Value = uc.User.UserId;
                        param_loginname.Value = uc.User.Username;
                        param_password.Value = uc.User.Password;
                        param_status.Direction = System.Data.ParameterDirection.InputOutput;
                        param_statusMessage.Direction = System.Data.ParameterDirection.InputOutput;
                        param_transaction.Value = 1;
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            o_status = Convert.ToInt32(param_status.Value);
                            o_statusMessage = Convert.ToString(param_statusMessage.Value);
                            if (o_status == 0)
                            {
                                success = true;
                                while (dr.Read())
                                {
                                    clientID = dr.GetInt32(0);
                                }
                                if (clientID == 0)
                                    return true;
                                else
                                    return false;
                            }
                            else
                            {
                                success = false;

                            }
                        }
                    }
                    conn.Close();

                }
            }
            catch (MySqlException ex)
            {
                o_status = 1;
                o_statusMessage = ex.Message;
                success = false;
            }
            if (clientID == 0)
                return true;
            else
                return false;

            #endregion
        }
        #endregion
        #region Get User Details
        public List<Param> GetUserDetails(Param p, out int o_status, out string o_statusMessage, int in_transaction)
        {
            #region Get the List of Country
            List<Param> qList = new List<Param>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("user_getuserdetails", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        MySqlParameter param_userid = cmd.Parameters.Add("in_userid", MySqlDbType.Int32);
                        MySqlParameter param_contactid = cmd.Parameters.Add("in_contactid", MySqlDbType.Int32);
                        MySqlParameter param_transaction = cmd.Parameters.Add("in_transaction", MySqlDbType.Int32);
                        MySqlParameter param_status = cmd.Parameters.Add("o_status", MySqlDbType.Int32);
                        MySqlParameter param_statusMessage = cmd.Parameters.Add("o_statusMessage", MySqlDbType.String);
                        param_userid.Value = p.User.UserId;
                        param_contactid.Value = p.Contact.ContactId;
                        param_status.Direction = System.Data.ParameterDirection.InputOutput;
                        param_statusMessage.Direction = System.Data.ParameterDirection.InputOutput;
                        param_transaction.Value = in_transaction;
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            o_status = Convert.ToInt32(param_status.Value);
                            o_statusMessage = Convert.ToString(param_statusMessage.Value);
                            while (dr.Read())
                            {
                                Param pr = new Param();
                                pr.User.UserId = dr.GetInt32(0);
                                pr.User.Name = dr.GetString(1);
                                pr.User.Age = dr.GetInt32(2);
                                pr.User.Volunteer = dr.GetInt32(3);
                                pr.User.SocialPoint = dr.GetInt32(4);
                                pr.Contact.Email = dr.GetString(5);
                                pr.Contact.Mobile = dr.GetString(6);
                                pr.User.Username = dr.GetString(7);
                                pr.User.Password = dr.GetString(8);
                                qList.Add(pr);
                            }
                            dr.Close();
                        }
                    }
                    conn.Close();

                }
            }
            catch (MySqlException ex)
            {
                o_status = 1;
                o_statusMessage = ex.Message;
                qList = new List<Param>();
            }
            return qList;
            #endregion
        }
        #endregion
        #region Porfile Details
        public List<ProfileDetails> ProfileDetails(Param p, out int o_status, out string o_statusMessage, int in_transaction)
        {
            #region Get the List of Country
            List<ProfileDetails> qList = new List<ProfileDetails>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("user_ProfileDetails", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        MySqlParameter param_transaction = cmd.Parameters.Add("in_transaction", MySqlDbType.Int32);
                        MySqlParameter param_status = cmd.Parameters.Add("o_status", MySqlDbType.Int32);
                        MySqlParameter param_statusMessage = cmd.Parameters.Add("o_statusMessage", MySqlDbType.String);
                        param_status.Direction = System.Data.ParameterDirection.InputOutput;
                        param_statusMessage.Direction = System.Data.ParameterDirection.InputOutput;
                        param_transaction.Value = in_transaction;
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            o_status = Convert.ToInt32(param_status.Value);
                            o_statusMessage = Convert.ToString(param_statusMessage.Value);
                            while (dr.Read())
                            {
                                ProfileDetails pr = new ProfileDetails();
                                pr.Name = dr.GetString(0);
                                pr.SocialPoints = dr.GetInt32(1);
                                pr.Address1 = dr.GetString(3);
                                pr.Address2 = dr.GetString(4);
                                pr.City = dr.GetString(5);
                                pr.State = dr.GetString(6);
                                pr.Country = dr.GetString(7);
                                qList.Add(pr);
                            }
                            dr.Close();
                        }
                    }
                    conn.Close();

                }
            }
            catch (MySqlException ex)
            {
                o_status = 1;
                o_statusMessage = ex.Message;
                qList = new List<ProfileDetails>();
            }
            return qList;
            #endregion
        }
        #endregion
        #region Get Country
        public List<Address> getCountries(out int o_status, out string o_statusMessage, int in_transaction)
        {
            #region Get the List of Country
            List<Address> qList = new List<Address>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("gen_getcountry", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        MySqlParameter param_transaction = cmd.Parameters.Add("in_transaction", MySqlDbType.Int32);
                        MySqlParameter param_status = cmd.Parameters.Add("o_status", MySqlDbType.Int32);
                        MySqlParameter param_statusMessage = cmd.Parameters.Add("o_statusMessage", MySqlDbType.String);
                        param_status.Direction = System.Data.ParameterDirection.InputOutput;
                        param_statusMessage.Direction = System.Data.ParameterDirection.InputOutput;
                        param_transaction.Value = in_transaction;
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            o_status = Convert.ToInt32(param_status.Value);
                            o_statusMessage = Convert.ToString(param_statusMessage.Value);
                            while (dr.Read())
                            {
                                Address ct1 = new Address();
                                ct1.Countryid = dr.GetInt32(0);
                                ct1.CountryName = dr.GetString(1);
                                qList.Add(ct1);
                            }
                            dr.Close();
                        }
                    }
                    conn.Close();

                }
            }
            catch (MySqlException ex)
            {
                o_status = 1;
                o_statusMessage = ex.Message;
                qList = new List<Address>();
            }
            return qList;
            #endregion
        }
        #endregion
        #region Insert/Update User  Address
        public int InsertUserAddress(Param p, out int o_status, out string o_statusMessage, int in_transaction)
        {
            bool success = false;
            int clientID = 0;
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("user_insertuserdetails", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        MySqlParameter param_userid = cmd.Parameters.Add("in_userid", MySqlDbType.Int32);
                        MySqlParameter param_addressid = cmd.Parameters.Add("in_addressid", MySqlDbType.Int32);
                        MySqlParameter param_cityid = cmd.Parameters.Add("in_cityid", MySqlDbType.Int32);
                        MySqlParameter param_add1 = cmd.Parameters.Add("in_add1", MySqlDbType.VarChar);
                        MySqlParameter param_add2 = cmd.Parameters.Add("in_add2", MySqlDbType.VarChar);
                        MySqlParameter param_transaction = cmd.Parameters.Add("in_transaction", MySqlDbType.Int32);
                        MySqlParameter param_status = cmd.Parameters.Add("o_status", MySqlDbType.Int32);
                        MySqlParameter param_statusMessage = cmd.Parameters.Add("o_statusMessage", MySqlDbType.String);
                        param_userid.Value = p.User.UserId;
                        param_addressid.Value = p.Address.Addressid;
                        param_cityid.Value = p.Address.Cityid;
                        param_add1.Value = p.Address.Add1;
                        param_add2.Value = p.Address.Add2;
                        param_status.Direction = System.Data.ParameterDirection.InputOutput;
                        param_statusMessage.Direction = System.Data.ParameterDirection.InputOutput;
                        param_transaction.Value = 1;
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {

                            o_status = Convert.ToInt32(param_status.Value);
                            o_statusMessage = Convert.ToString(param_statusMessage.Value);
                            if (o_status == 0)
                            {
                                success = true;
                                while (dr.Read())
                                {
                                    clientID = dr.GetInt32(0);
                                }
                                dr.Close();
                            }
                            else
                            {
                                success = false;
                                clientID = 0;
                            }
                        }

                    }
                    conn.Close();
                }

            }
            catch (MySqlException ex)
            {
                o_status = 1;
                o_statusMessage = ex.Message;
                success = false;
                clientID = 0;
            }
            return clientID;
        }
        #endregion
        #region Get User Address
        public List<Address> GetUserAddress(Param p,out int o_status, out string o_statusMessage, int in_transaction)
        {
            #region Get user Address
            List<Address> qList = new List<Address>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("user_getuseraddress", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        MySqlParameter param_userid = cmd.Parameters.Add("in_userid", MySqlDbType.Int32);
                        MySqlParameter param_addressid = cmd.Parameters.Add("in_addressid", MySqlDbType.Int32);
                        MySqlParameter param_transaction = cmd.Parameters.Add("in_transaction", MySqlDbType.Int32);
                        MySqlParameter param_status = cmd.Parameters.Add("o_status", MySqlDbType.Int32);
                        MySqlParameter param_statusMessage = cmd.Parameters.Add("o_statusMessage", MySqlDbType.String);
                        param_userid.Value = p.User.UserId;
                        param_addressid.Value = p.Address.Addressid;
                        param_status.Direction = System.Data.ParameterDirection.InputOutput;
                        param_statusMessage.Direction = System.Data.ParameterDirection.InputOutput;
                        param_transaction.Value = in_transaction;
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            o_status = Convert.ToInt32(param_status.Value);
                            o_statusMessage = Convert.ToString(param_statusMessage.Value);
                            while (dr.Read())
                            {
                                Address ct1 = new Address();
                                ct1.UserId = dr.GetInt32(0);
                                ct1.Add1 = dr.GetString(1);
                                ct1.Add2 = dr.GetString(2);
                                ct1.Cityname = dr.GetString(3);
                                ct1.Statename = dr.GetString(4);
                                ct1.CountryName = dr.GetString(5);
                                qList.Add(ct1);
                            }
                            dr.Close();
                        }
                    }
                    conn.Close();

                }
            }
            catch (MySqlException ex)
            {
                o_status = 1;
                o_statusMessage = ex.Message;
                qList = new List<Address>();
            }
            return qList;
            #endregion
        }
        #endregion
        #region Delete User 
        public int DeleteUser(Param p, out int o_status, out string o_statusMessage, int in_transaction)
        {
            bool success = false;
            int clientID = 0;
            try
            {

                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("user_deleteuser", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        MySqlParameter param_userid = cmd.Parameters.Add("in_userid", MySqlDbType.Int32);
                        MySqlParameter param_transaction = cmd.Parameters.Add("in_transaction", MySqlDbType.Int32);
                        MySqlParameter param_status = cmd.Parameters.Add("o_status", MySqlDbType.Int32);
                        MySqlParameter param_statusMessage = cmd.Parameters.Add("o_statusMessage", MySqlDbType.String);
                        param_userid.Value = p.User.UserId;
                        param_status.Direction = System.Data.ParameterDirection.InputOutput;
                        param_statusMessage.Direction = System.Data.ParameterDirection.InputOutput;
                        param_transaction.Value = 1;
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {

                            o_status = Convert.ToInt32(param_status.Value);
                            o_statusMessage = Convert.ToString(param_statusMessage.Value);
                            if (o_status == 0)
                            {
                                success = true;
                                while (dr.Read())
                                {
                                    clientID = dr.GetInt32(0);
                                }
                                dr.Close();
                            }
                            else
                            {
                                success = false;
                                clientID = 0;
                            }
                        }

                    }
                    conn.Close();
                }

            }
            catch (MySqlException ex)
            {
                o_status = 1;
                o_statusMessage = ex.Message;
                success = false;
                clientID = 0;
            }
            return clientID;
        }
        #endregion
        #region Save Photo
        private string SavePhoto(string photo)
        {
            #region Save Image
            string address = "";
            try
            {
                var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
                DirectoryInfo di = new FileInfo(location.AbsolutePath).Directory;
                string DataDirectory = di.FullName;
                DataDirectory = DataDirectory + "\\ImageData";
                if (!Directory.Exists(DataDirectory))
                    Directory.CreateDirectory(DataDirectory);
                Random rd = new Random();
                string fName = DateTime.Now.Ticks.ToString() + ".fa";
                string fullFileName = DataDirectory + "\\" + fName;
                using (StreamWriter file = File.CreateText(fullFileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.MissingMemberHandling = MissingMemberHandling.Ignore;
                    serializer.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                    serializer.NullValueHandling = NullValueHandling.Include;
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(file, photo, typeof(string));
                }
                address = fullFileName;
            }
            catch (Exception ex) { }
            return address;




            #endregion
        }



        #endregion
    }
}