using System.Data.SqlClient;
using Spectre.Console;

namespace Day1_Task2
{
    public class Tracker
    {
        public void Add_Transaction()
        {
            SqlConnection con = new SqlConnection("server=IN-333K9S3;database=DemoDB;Integrated Security = true");
            con.Open();
            SqlCommand cmd = new SqlCommand($"insert into Expense_Tracker values(@title,@description,@amount,@date)", con);

            string title = AnsiConsole.Ask<string>("[rgb(220,20,60)]Enter Title:[/]");
            string description = AnsiConsole.Ask<string>("[rgb(220,20,60)]Enter Description:[/]");
            decimal amount = AnsiConsole.Ask<decimal>("[rgb(220,20,60)]Enter Amount (+ve value for income , -ve value for expence):[/]");
            DateTime date = AnsiConsole.Ask<DateTime>("[rgb(220,20,60)]Enter Date(DD/MM/YYYY):[/]");

            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@date", date);

            cmd.ExecuteNonQuery();

            AnsiConsole.MarkupLine("[rgb(124,211,76)]Transaction Added Sucessfully [/]");

            con.Close();
        }

        public void View_Expenses()
        {
            //AnsiConsole.MarkupLine("[underline rgb(124,211,76)]Expense Details:[/]");

            SqlConnection con = new SqlConnection("server=IN-333K9S3;database=DemoDB;Integrated Security = true");
            con.Open();

            string query = $"select * from Expense_Tracker where Amount < 0";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            var table = new Table();
            table.Border = TableBorder.Rounded;
            //Adding Columns
            table.AddColumn("Sl.No");
            table.AddColumn("Title");
            table.AddColumn("Description");
            table.AddColumn("Amount");
            table.AddColumn("Date");
            table.Title("[blue]EXPENSE DETAILS[/]");
            table.BorderColor(Color.Blue);
            foreach (var column in table.Columns)
            {
                column.Centered();
            }

            while (reader.Read())
            {
                table.AddRow(reader["sl_no"].ToString(), reader["Title"].ToString(), reader["Description"].ToString(), reader["Amount"].ToString(), reader["Date"].ToString());
            }

            AnsiConsole.Write(table);
            con.Close();

        }
        public void View_Income()
        {
            //AnsiConsole.MarkupLine("[underline rgb(124,211,76)]Income Details:[/]");

            SqlConnection con = new SqlConnection("server=IN-333K9S3;database=DemoDB;Integrated Security = true");
            con.Open();
            string query = $"select * from Expense_Tracker where Amount >= 0";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            var table = new Table();
            //Adding Columns
            table.AddColumn("Sl.No");
            table.AddColumn("Title");
            table.AddColumn("Description");
            table.AddColumn("Amount");
            table.AddColumn("Date");
            //table.HideHeaders();
            table.Border(TableBorder.Rounded);
            table.Title("[blue]INCOME DETAILS[/]");
            //To add color to table border
            table.BorderColor(Color.Blue);

            //To center the text in all columns
            foreach (var column in table.Columns)
            {
                column.Centered();
            }


            while (reader.Read())
            {
                table.AddRow(reader["sl_no"].ToString(), reader["Title"].ToString(), reader["Description"].ToString(), reader["Amount"].ToString(), reader["Date"].ToString());
            }

            AnsiConsole.Write(table);
            
            con.Close();
        }
        public void update_Expense_Tracker()
        {
            SqlConnection con = new SqlConnection("server=IN-333K9S3;database=DemoDB;Integrated Security = true");
            con.Open();

            string slno = AnsiConsole.Ask<string>("Enter the sl no you want to update");
            string query = $"update Expense_Tracker set title=@title,description=@description,amount=@amount,date=@date where sl_no = {slno}";
            SqlCommand cmd = new SqlCommand(query, con);

            string title = AnsiConsole.Ask<string>("[rgb(220,20,60)]Enter Updated Title:[/]");
            string description = AnsiConsole.Ask<string>("[rgb(220,20,60)]Enter Updated Description:[/]");
            decimal amount = AnsiConsole.Ask<decimal>("[rgb(220,20,60)]Enter Updated Amount (+ve value for income , -ve value for expence):[/]");
            DateTime date = AnsiConsole.Ask<DateTime>("[rgb(220,20,60)]Enter Updated Date(DD/MM/YYYY):[/]");

            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@date", date);

            cmd.ExecuteNonQuery();

            AnsiConsole.MarkupLine("[rgb(124,211,76)]Transaction Updated Sucessfully [/]");
            con.Close();
        }

        public void Delete_Expense_Tracker()
        {
            SqlConnection con = new SqlConnection("server=IN-333K9S3;database=DemoDB;Integrated Security = true");
            con.Open();

            string slno = AnsiConsole.Ask<string>("Enter the sl no you want to Delete");

            string query = $"delete from Expense_Tracker where sl_no = {slno}";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.ExecuteNonQuery();

            AnsiConsole.MarkupLine("[rgb(124,211,76)]Transaction Deleted Sucessfully [/]");
            con.Close();
        }

        public void View_Balance()
        {
            SqlConnection con = new SqlConnection("server=IN-333K9S3;database=DemoDB;Integrated Security = true");
            con.Open();

            string query = $"select sum(Amount) as TotalBalance from Expense_Tracker";

            SqlCommand cmd = new SqlCommand(query, con);
            var Total_Balance = cmd.ExecuteScalar();

            AnsiConsole.MarkupLine($"[red]Available Balance =[/] [underline rgb(124,211,76)]{(decimal)Total_Balance}[/]");

            con.Close();
        }

        public void View_Expense_Tracker()
        {
            //AnsiConsole.MarkupLine("[underline rgb(124,211,76)]Expense Tracker Table:[/]");

            SqlConnection con = new SqlConnection("server=IN-333K9S3;database=DemoDB;Integrated Security = true");
            con.Open();

            string query = $"select * from Expense_Tracker";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            var table = new Table();
            table.Border = TableBorder.Rounded;
            //Adding Columns
            table.AddColumn("Sl.No");
            table.AddColumn("Title");
            table.AddColumn("Description");
            table.AddColumn("Amount");
            table.AddColumn("Date");
            table.Title("[blue]EXPENSE TRACKER TABLE[/]");
            table.BorderColor(Color.Blue);

            foreach (var column in table.Columns)
            {
                column.Centered();
            }

            while (reader.Read())
            {
                table.AddRow(reader["sl_no"].ToString(), reader["Title"].ToString(), reader["Description"].ToString(), reader["Amount"].ToString(), reader["Date"].ToString());
            }

            AnsiConsole.Write(table);
            con.Close();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.Write(new FigletText("Expense Tracker").Centered().Color(Color.Red));
            Tracker tracker = new Tracker();

            while (true)
            {
               /* var rule = new Rule("[red]***[/]");
                rule.Style = Style.Parse("red dim"); // --> to style the rule
                AnsiConsole.Write(rule);*/

                Console.WriteLine();
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("[rgb(255,255,255)]Select your choice :[/]")
                    .AddChoices(new[] {
                        "Add Transaction", "View Expenses", "View Income",
                        "Update Data using sl no","Delete Data using sl no"
                        ,"View Balance","View Expense Tracker Table"
                    }));

                switch (choice)
                {
                    case "Add Transaction":
                        {
                            tracker.Add_Transaction();
                            break;
                        }
                    case "View Expenses":
                        {
                            tracker.View_Expenses();
                            break;
                        }
                    case "View Income":
                        {
                            tracker.View_Income();
                            break;
                        }
                    case "Update Data using sl no":
                        {
                            tracker.update_Expense_Tracker();
                            break;
                        }
                    case "Delete Data using sl no":
                        {
                            tracker.Delete_Expense_Tracker();
                            break;
                        }
                    case "View Balance":
                        {
                            tracker.View_Balance();
                            break;
                        }
                    case "View Expense Tracker Table":
                        {
                            tracker.View_Expense_Tracker();
                            break;
                        }
                }
            }
        }
    }
}