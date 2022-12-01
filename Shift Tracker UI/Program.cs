using ConsoleTableExt;
using System.Net.Http.Json;
using System.Text.Json;



namespace Shift_Tracker_UI
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            // Menu setup
            string[] menu = { "Shift Tracker", "1: View Shifts", "2: Enter New Shift Information", "0: Exit Program" };
            List<string> menuList = new(menu);

            // Variable declarations
            int menuChoice = 0;
            bool closeProgram = false;
            bool shiftStarted = false;
            DateTime start = DateTime.Now,end;
            decimal pay, minutes;
            string location = "";
            //MAIN MENU
            while (!closeProgram)                                                     
            {
                Console.Clear();
                ConsoleTableBuilder.From(menuList).ExportAndWriteLine(TableAligntment.Center);
                menuChoice = CountCheck(2, 0);
                Console.Clear();
                switch (menuChoice)
                {
                    // Exit Program
                    default:                            
                        closeProgram = true;
                        break;
                    // Read
                    case 1:                             
                        List<Shift> shiftList = await ProcessJSONRequest("https://localhost:5001/api/shifts");
                        Console.ReadKey();
                        break;
                    // Create
                    case 2:                             
                        if (!shiftStarted)
                        {
                            start = DateTime.Now;
                            Console.WriteLine($"Your shift has started at {start.ToLocalTime()}");
                            Console.ReadKey();
                            shiftStarted = true;
                            break;
                        }
                        end = DateTime.Now;
                        Console.WriteLine($"Your shift has ended at {end.ToLocalTime()}");
                        Console.WriteLine("Please enter your pay per hour amount: (Example 29.41 or 35.00)");
                        pay = PayCheck();
                        minutes = ShiftCalc(start, end);
                        Console.WriteLine("Please enter the location the work was performed.");
                        location = EmptyStringCheck();
                        Shift tmp = new(start, end, pay, minutes, location);
                        HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:5001/api/shifts", tmp);
                        if (response.IsSuccessStatusCode)
                            Console.WriteLine("Your shift was entered successfully!");
                        else
                            Console.WriteLine("Internal server error, please try again.");
                        Console.ReadKey();
                        shiftStarted = false;
                        break;
                }
            }
        }



            // Data Validation
            public static int CountCheck(int count, int menuOrList)
            {
                int a;
                while (!Int32.TryParse(Console.ReadLine(), out a) || a < menuOrList || a > count)
                    Console.WriteLine("Invalid input, enter again:");
                return a;
            }

            public static string EmptyStringCheck()
            {
                string a = Console.ReadLine();
                while (String.IsNullOrWhiteSpace(a))
                {
                    Console.WriteLine("Invalid input, enter again:");
                    a = Console.ReadLine();
                }
                return a;
            }

            public static decimal PayCheck()
            {
            decimal pay = 0;
                while (!Decimal.TryParse(Console.ReadLine(), out pay) || pay < 0)
                    Console.WriteLine("Invalid input, enter again:");
            return pay;
            }

        // Data Processing

        public static decimal ShiftCalc(DateTime start, DateTime end)
            {
                decimal minutes = Convert.ToDecimal((end - start).TotalMinutes);
                return minutes;
            }

            //JSON Object Handling
            private static async Task<List<Shift>> ProcessJSONRequest(string URL)
            {
                var streamTask = client.GetStreamAsync(URL);
                var shifts = await JsonSerializer.DeserializeAsync<List<Shift>>(await streamTask);
                List<Shift> list = new List<Shift>();
                foreach (var x in shifts)
                {
                    list.Add(x);
                }
                ConsoleTableBuilder.From(list).WithTitle("Shifts").ExportAndWriteLine(TableAligntment.Center);
                return list;
            }
        }
    }