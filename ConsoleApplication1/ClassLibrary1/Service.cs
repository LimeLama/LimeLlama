using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Service
{
    public class Service : Interface
    {
        public string test(string g)
        {
            string servg = "Test = " + g;
            return servg;
        }
        public int CheckLogin(string login, string pass)
        {
            try
            {
                string query = "SELECT * FROM Logins WHERE [Login]=@name";
                SqlConnection conn = new SqlConnection("server=FARMBOOK\\GAMEDB;User Id ='gm'; Password = '12345'; database=AuthDB");


                SqlCommand com = new SqlCommand(query, conn);
                com.Parameters.Add("@name", SqlDbType.NVarChar).Value = login;
                conn.Open();

                SqlDataReader result = com.ExecuteReader();
              if (result.HasRows)
                {
                        result.Read();
                            string val1 = result[1].ToString();
                            string val2 = result[2].ToString(); 
                          //  Console.WriteLine("Val1 = " + val1);
                          //  Console.WriteLine("Val2 = " + val2);
                            result.Read();    
                    result.Close();
                    conn.Close();

                        if (val2 == pass)
                        {
                            Console.WriteLine(" user autorized: \n Name {0} \n Password {1}",  val1, val2);
                            return 0; //Authorization succeed
                        }
                        else
                        {
                            Console.WriteLine(" autorization faild:  Name {0} \n Password {1}", login, pass);
                            return 2; // Wrong password
                        }
                }
                else
                {
                    Console.WriteLine("No such user in system: {0}  ", login);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("autorization error: {0}", e.ToString());
                return 999; //SQL error
            }
            //finally
            //{
            //    result.Close();
            //    conn.Close();
            //}
            return 0;
        }
        public int AddLogin(string login, string pass)
        {
            try
            {
                SqlConnection conn = new SqlConnection("server=FARMBOOK\\GAMEDB;User Id ='gm'; Password = '12345'; database=AuthDB");
                string query = "SELECT * FROM Logins WHERE [Login]=@name";


                SqlCommand CheckExistLogincom = new SqlCommand(query, conn);
                CheckExistLogincom.Parameters.Add("@name", SqlDbType.NVarChar).Value = login;
                conn.Open();
                SqlDataReader CheckReader = CheckExistLogincom.ExecuteReader();
                if (CheckReader.HasRows) { return 1; } //Login exists
                conn.Close();

                string querymaxid = "Select max(id) from Logins";
                SqlCommand com = new SqlCommand(querymaxid, conn);
                conn.Open();
                SqlDataReader result = com.ExecuteReader();
                result.Read();
                int maxid = Convert.ToInt32(result[0].ToString());
                result.Close();
                conn.Close();

                string queryinsert = "Insert into Logins Values (@id, @name, @pass)";
                com = new SqlCommand(queryinsert, conn);
                com.Parameters.Add("@id", SqlDbType.NVarChar).Value = maxid + 1;
                com.Parameters.Add("@name", SqlDbType.NVarChar).Value = login;
                com.Parameters.Add("@pass", SqlDbType.NVarChar).Value = pass;
                conn.Open();
                com.ExecuteNonQuery();
                result.Close();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("error: {0}", e.ToString());
            }
            return 0; //Registration succeed
        }

        public string ErrorDescription(int k)
        {
            switch (k)
            {
                case 0: return "Operation succeed"; break;
                case 1: return "Login exists"; break;
                case 2: return "Wrong password"; break;
                case 999: return "SQL error, check server console for more information"; break;
            }
            return "No error description";
 
        }
    }
}
