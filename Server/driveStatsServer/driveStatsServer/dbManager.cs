using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;

namespace driveStatsServer
{
    public class dbManager
    {
        string databaseName = "temp";
        string password = "admin";
        string ip = "127.0.0.1";
        string port = "5432";
        string username = "postgres";
        NpgsqlConnection conn;

        public dbManager()
        {
            string connConfig = String.Format("Server={0};Port = {1};Database={2};User ID={3};Password={4};", ip, port, databaseName, username, password);
            conn = new NpgsqlConnection(connConfig);
        }

        private void copen()
        {
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                saveException(ex);
            }
        }

        private void cclose()
        {
            try
            {
                conn.Close();
            }
            catch (Exception ex)
            {
                saveException(ex);
            }
        }
        /**
         * @param the exception to save in a timestamped file
         **/
        private void saveException(Exception ex)
        {
            System.IO.File.WriteAllText("logFiles/"+DateTime.Now.ToLongTimeString(), ex.Message);
        }

        /**
         * @return average score of all users
         **/
        public double getAverageScore() 
        {
            copen();
            String sql = "SELECT sum(averagescore)/count(averagescore) as gAverage FROM users WHERE averagescore > 0";
            double num = -1;
            NpgsqlCommand com = new NpgsqlCommand(sql, conn);
            NpgsqlDataReader dr = com.ExecuteReader();
            dr.Read();
            num = float.Parse(dr[0].ToString());
            cclose();
            return num;
        }
        /**
         * @param takes a class of user to insert 
         **/
        public void addUser(User u)
        {
            copen();
            String sql = String.Format("INSERT INTO users(email, joindate) VALUES ('{0}','{1}')",u.email,u.joinDate);
            NpgsqlCommand com = new NpgsqlCommand(sql, conn);
            com.ExecuteNonQuery();
            cclose();
        }

        /**
         * @param takes a class of user with an id populated
         * @return returns the class that is populated
         **/
        public User getUser(User u)
        {
            copen();
            String sql = String.Format("SELECT id FROM users WHERE email = '{0}'",u.email);
            NpgsqlCommand com = new NpgsqlCommand(sql, conn);
            NpgsqlDataReader dr = com.ExecuteReader();
            dr.Read();
            int num = Int32.Parse(dr[0].ToString());
            u.ID = num;
            cclose();
            
            return u;
        }
        /**
         * @param takes a userTripclass to add to the datbase that includes a full list of tripData
         * 
         **/
        public void addUserTrip(userTrip ut)
        {
            copen();
            //store the main trip
            String sql = String.Format("INSERT INTO trips(userID,tripDate,startLatitude,startLongitude)VALUES ({0},'{1}','{2}','{3}')",ut.userID,ut.tripDate,ut.startLatitude,ut.stopLongitude);
            NpgsqlCommand com = new NpgsqlCommand(sql, conn);
            com.ExecuteNonQuery();
            // to do, get the id back
 
            // store the data recordings of the trip
            sql = String.Format("INSERT INTO trips(userID,tripDate,startLatitude,startLongitude)VALUES ({0},'{1}','{2}','{3}')", ut.userID, ut.tripDate, ut.startLatitude, ut.stopLongitude);
            com = new NpgsqlCommand(sql, conn);
            com.ExecuteNonQuery();
            cclose();
        }
    }
}