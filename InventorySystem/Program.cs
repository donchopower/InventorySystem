using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;

namespace InventorySystem
{
    class Program
    {

       


        static void Main(string[] args)
        {
            

            LoginPrompt();
        }


        static void LoginPrompt()
        {
            //var users = new Dictionary<string, string>();
            //users.Add("admin", "admin");
            //users.Add("agent", "agent");

            string connectionString = "server=localhost;user=root;database=inventorysystem;port=3306;password=MySQLPassword123";

            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlConnection conn2 = new MySqlConnection(connectionString);


            try
            {
                
                conn.Open();
                conn2.Open();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }


            Console.WriteLine("Please log in.");
            Console.Write("Username:");
            string username = Console.ReadLine();
            Console.Write("Password:");
            string password = Console.ReadLine();
            string sql = "SELECT COUNT(*) FROM users WHERE username = username";

            //CHECK FIRST FOR USERNAME AND THEN FOR PASSWORD SQL QUERRIES


            MySqlCommand check_User_Name = new MySqlCommand($"SELECT * FROM users WHERE username = @username", conn);
            check_User_Name.Parameters.AddWithValue("@username", username);
            MySqlDataReader usernameReader = check_User_Name.ExecuteReader();
            




            MySqlCommand check_User_Password = new MySqlCommand("SELECT * FROM users WHERE password = @password", conn2);
            check_User_Password.Parameters.AddWithValue("@password", password);
            MySqlDataReader passwordReader = check_User_Password.ExecuteReader();


            //usernameReader.
           // Console.WriteLine(userResult);
            if(usernameReader.HasRows == false)
            {
                Console.WriteLine("Username not found!");
                LoginPrompt();
            }
            else
            {
                //if (!passwordReader.HasRows)

                if(passwordReader.HasRows == false)
                {
                    Console.WriteLine("Incorrect password!");
                    LoginPrompt();
                }
                else
                {
                    Console.WriteLine($"Welcome {username}!");
                    conn.Close();
                    conn2.Close();
                    ShowOptions();

                }
            }
            

            //if (users.ContainsKey(username) && users.ContainsValue(password))
            //{

            //    Console.WriteLine($"Welcome {username}!");
            //    ShowOptions();
          

            //}

            //if(users.ContainsKey(username) == false)
            //{
            //    Console.WriteLine("Username not found");
            //    LoginPrompt();
            //}

            //if(users.ContainsKey(username) && users.ContainsValue(password) == false)
            //{
            //    Console.WriteLine("Incorrect password!");
            //    LoginPrompt();
            //}


        }

        static void ShowOptions()
        {
            Console.WriteLine("1 - Search for item.\n2 - Refund.");
            string input = Console.ReadLine();
            if(input == "1")
            {
                SearchForItem();
            }
            else if(input == "2")
            {
                Refund();
            }
            else
            {
                Console.WriteLine("Invalid option!");
                ShowOptions();
            }
           

        }

        static void SearchForItem()
        {
            string connectionString = "server=localhost;user=root;database=stores;port=3306;password=MySQLPassword123";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            string resultat = "";
            conn.Open();

            Console.Write("Please select store: ");
            string storeID =Console.ReadLine();
            Console.Write("Please enter product ID:");
            string productID = Console.ReadLine();


            //var checkStoreValidity = new MySqlCommand("SELECT * FROM storelist WHERE storeID = storeID AND productID = productID", conn);
            //checkStoreValidity.Parameters.AddWithValue("storeID", storeID);
            //checkStoreValidity.Parameters.AddWithValue("productID", productID);


           var readInput = new MySqlCommand($"SELECT * FROM storelist WHERE storeid = {storeID} AND productID = {productID}", conn);
            MySqlDataReader result = readInput.ExecuteReader();

            if (result.HasRows)
            {
                result.Read();
                Console.WriteLine(result.GetValue(2));
            }



           //// var storeValidityReader = checkStoreValidity.ExecuteReader();

           // if (storeValidityReader.HasRows == false)
           // {
           //     Console.WriteLine("Invalid store ID!");
           // }
           // else
           // {
           //     storeValidityReader.Read();
           //     Console.WriteLine(storeValidityReader.GetInt32(2));
           // }

            



            //Console.Write("Please enter item code:");
            //string input = Console.ReadLine();
            //if (items.ContainsKey(input))
            //{
            //    Console.WriteLine($"{input}\nQuantity:{items[input]}");
            //    ChooseOptionAfterSearch();
                
            //}
            //else
            //{
            //    Console.WriteLine($"Item {input} not found!");
            //    ChooseOptionAfterSearch();
            //}





        }


        static void ChooseOptionAfterSearch()
        {
            Console.WriteLine("\n\nChoose option:\n1 - Search again.\n2 - Back.");
            string option = Console.ReadLine();

            if (option == "1")
            {
                SearchForItem();
            }
            else if (option == "2")
            {
                ShowOptions();
            }
            else
            {
                Console.WriteLine("Invalid option! Choose again!");
                ChooseOptionAfterSearch();
            }
        }


        static void Refund()
        {

            Console.Write("Please enter refund password: ");
            string password = Console.ReadLine();

            if(password != "refundpassword")
            {
                Console.WriteLine("Incorrect password!");
                ShowOptions();
            }
            else
            {
                Console.Write("Enter item ID to refund: ");
                string input = Console.ReadLine();
                if(input != "S01")
                {
                    Console.WriteLine("Invalid item ID!");
                    ShowRefundOptions();
                }
                else
                {
                    Console.WriteLine($"Item {input} has been successfully refunded!");
                    ShowRefundOptions();
                }
            }

        }



        static void ShowRefundOptions()
        {
            Console.WriteLine("1 - Refund again.\n2 - Back.");
            string input = Console.ReadLine();
            if(input == "1")
            {
                RefundAgain();
            }
            else if(input == "2")
            {
                ShowOptions();
            }
            else
            {
                Console.WriteLine("Invalid option!");
                ShowRefundOptions();
            }
        }


        static void RefundAgain()
        {
            Console.Write("Enter item ID to refund: ");
            string input = Console.ReadLine();
            if (input != "S01")
            {
                Console.WriteLine("Invalid item ID!");
                ShowRefundOptions();
            }
            else
            {
                Console.WriteLine($"Item {input} has been successfully refunded!");
                ShowRefundOptions();
            }
        }
    }


































    }

