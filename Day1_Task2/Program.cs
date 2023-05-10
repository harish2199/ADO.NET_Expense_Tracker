using System.Data.SqlClient;
using System.Transactions;

namespace Day1_Task2
{
    public class Tracker
    {
        public void Add_Transaction()
        {
            SqlConnection con = new SqlConnection("server=IN-333K9S3;database=DemoDB;Integrated Security = true");
            con.Open();

            SqlCommand cmd = new SqlCommand($"insert into Expense_Tracker values(@title,@description,@amount,@date)",con);

            Console.WriteLine("Enter Title");
            string title = Console.ReadLine();

            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter Amount (+ve value for income , -ve value for expence: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Date(DD/MM/YYYY): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@date", date);

            cmd.ExecuteNonQuery();

           
            Console.WriteLine("Transaction Added Sucessfully");

            con.Close();
        }

            public void View_Expenses()
            {
                Console.WriteLine("Expenses:");
                Console.WriteLine("sl_no\tTitle\tDescription\tAmount\tDate");


            SqlConnection con = new SqlConnection("server=IN-333K9S3;database=DemoDB;Integrated Security = true");
                con.Open();

                string query = $"select * from Expense_Tracker where Amount < 0";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write($"{reader[i]}\t");
                    }
                    Console.WriteLine();
                }

            
                con.Close();
            }

        public void View_Income()
        {
            Console.WriteLine("Income:");
            Console.WriteLine("sl_no\tTitle\tDescription\tAmount\tDate");


            SqlConnection con = new SqlConnection("server=IN-333K9S3;database=DemoDB;Integrated Security = true");
            con.Open();

            string query = $"select * from Expense_Tracker where Amount >= 0";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($"{reader[i]}\t");
                }
                Console.WriteLine();
            }

            
            con.Close();

        }

        public void update_Expense_Tracker()
        {
            SqlConnection con = new SqlConnection("server=IN-333K9S3;database=DemoDB;Integrated Security = true");
            con.Open();

            Console.WriteLine("Enter the sl no you want to update");
            int slno = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter new Title");
            string title = Console.ReadLine();

            Console.Write("Enter new Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter new Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Console.Write("Enter new Date(DD/MM/YYYY): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

           
            string query = $"update Expense_Tracker set title=@title,description=@description,amount=@amount,date=@date where sl_no = {slno}";

            SqlCommand cmd = new SqlCommand(query, con);
            
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@date", date);

            cmd.ExecuteNonQuery();


            Console.WriteLine("Transaction Updated Sucessfully");

            con.Close();

        }

        public void Delete_Expense_Tracker()
        {
            SqlConnection con = new SqlConnection("server=IN-333K9S3;database=DemoDB;Integrated Security = true");
            con.Open();

            Console.WriteLine("Enter the sl no you want to update");
            int slno = int.Parse(Console.ReadLine());
            
            string query = $"delete from Expense_Tracker where sl_no = {slno}";

            SqlCommand cmd = new SqlCommand(query, con);
            
            cmd.ExecuteNonQuery();


            Console.WriteLine("Transaction Deleted Sucessfully");

            con.Close();

        }

        public void View_Balance()
        {
            SqlConnection con = new SqlConnection("server=IN-333K9S3;database=DemoDB;Integrated Security = true");
            con.Open();

            string query = $"select sum(Amount) as TotalBalance from Expense_Tracker";

            SqlCommand cmd = new SqlCommand(query, con);
            var Total_Balance = cmd.ExecuteScalar();

            Console.WriteLine($"Total Balance = {(decimal)Total_Balance}");

            con.Close(); 
        }

        public void View_Expense_Tracker()
        {
            Console.WriteLine("sl_no\tTitle\tDescription\tAmount\tDate");

            SqlConnection con = new SqlConnection("server=IN-333K9S3;database=DemoDB;Integrated Security = true");
            con.Open();

            string query = $"select * from Expense_Tracker";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($"{reader[i]}\t");
                }
                Console.WriteLine();
            }


            con.Close();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Tracker tracker = new Tracker();

            while (true)
            {
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("1. Add Transaction");
                Console.WriteLine("2. View Expenses");
                Console.WriteLine("3. View Income");
                Console.WriteLine("4. Update Data using sl no");
                Console.WriteLine("4. Delete Data using sl no");
                Console.WriteLine("6. View Balance");
                Console.WriteLine("7. View Expense Tracker Table");
                int choice = 0;
                try
                {
                    Console.WriteLine("Enter Your choice: ");
                    choice = Convert.ToInt16(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Enter only Numbers");
                }
                Console.WriteLine("---------------------------------------------------------");
                switch (choice)
                {
                    case 1:
                        {
                            tracker.Add_Transaction();
                            break;
                        }
                    case 2:
                        {
                            tracker.View_Expenses();
                            break;
                        }
                    case 3:
                        {
                            tracker.View_Income();
                            break;
                        }
                    case 4:
                        {
                            tracker.update_Expense_Tracker();
                            break;
                        }
                    case 5:
                        {
                            tracker.Delete_Expense_Tracker();
                            break;
                        }
                    case 6:
                        {
                            tracker.View_Balance();
                            break;
                        }
                    case 7:
                        {
                            tracker.View_Expense_Tracker();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong Choice Entered");
                            break;
                        }
                }
            }
        }
    }
}