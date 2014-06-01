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
        public bool CheckLogin(string login, string pass)
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
                            return true;
                        }
                        else
                        {
                            Console.WriteLine(" autorization faild:  Name {0} \n Password {1}", login, pass);
                            return false;
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
                return false;
            }
            //finally
            //{
            //    result.Close();
            //    conn.Close();
            //}
            return true;
        }
        public bool AddLogin(string login, string pass)
        {
            try
            {
                SqlConnection conn = new SqlConnection("server=FARMBOOK\\GAMEDB;User Id ='gm'; Password = '12345'; database=AuthDB");
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
            return true;
        }
    }
}
