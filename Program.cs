using Newtonsoft.Json;
using NumberApp;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

SqlConnection connection = new SqlConnection(@"Data Source=(local)\SQLExpress;Initial Catalog=NumbersLists;Integrated Security=SSPI;");

List<int> numbers = new List<int>();
var timeToSortAsc = 0;
var timeToSortDesc = 0;

ReadNumbers();

void ReadNumbers()
{
    Console.Clear();
    Console.WriteLine("Please input any int and press enter inbetween each number.");
    Console.WriteLine("When the list is done press enter with no input to get options");

    while (true)
    {
        var input = Console.ReadLine();
        int value;
        if (input == null || input == "")
        {
            break;
        }
        else if (!string.IsNullOrEmpty(input) && int.TryParse(input, out value))
        {
            numbers.Add(value);
        }
        else
        {
            Console.WriteLine("That was not a valid number please try again");
        }
    }
    OrderTheList();
}

void OrderTheList()
{
    var stopwatch = new Stopwatch();

    stopwatch.Start();
    var ascList = numbers.OrderBy(x => x).ToList();
    stopwatch.Stop();
    timeToSortAsc = Convert.ToInt32(stopwatch.ElapsedMilliseconds);

    stopwatch.Start();
    var descList = numbers.OrderByDescending(x => x).ToList();
    stopwatch.Stop();
    timeToSortDesc = Convert.ToInt32(stopwatch.ElapsedMilliseconds);

    Console.WriteLine("Choose an ordering option from the following list:");
    Console.WriteLine("\t1 - Ascending");
    Console.WriteLine("\t2 - Descending");

    switch (Console.ReadLine())
    {
        case "1":
            Console.Clear();
            Console.WriteLine("Here is your list in ascending order: ");
            ascList.ForEach(num => { Console.Write(num + " "); });
            Console.WriteLine();
            Console.WriteLine("It took: " + timeToSortAsc + " milliSeconds");
            SaveToDatabase(ascList, timeToSortAsc, "Ascending");
            break;
        case "2":
            Console.Clear();
            Console.WriteLine("Here is your list in descending order: ");
            descList.ForEach(num => { Console.Write(num + " "); });
            Console.WriteLine();
            Console.WriteLine("It took: " + timeToSortDesc + " milliSeconds");
            SaveToDatabase(descList, timeToSortDesc, "Descending");
            break;
    }
    Menu();
}



void Menu()
{
    Console.WriteLine();
    Console.WriteLine("Choose what to do next:");
    Console.WriteLine("\t1 - Convert Lists to JSON");
    Console.WriteLine("\t2 - Add another list");
    Console.WriteLine("\t3 - Exit");

    switch (Console.ReadLine())
    {
        case "1":
            ConvertToJson();
            break;
        case "2":
            ReadNumbers();
            break;
        case "3":
            Console.WriteLine("Thank you have a great day!");
            break;

    }
}





void ConvertToJson()
{
    var context = new Context();
    var listOfLists = context.NumberList.ToList();
    var jsonList = new List<string>();
    foreach (var listItem in listOfLists)
    {
        string jsonItem = JsonConvert.SerializeObject(listItem, Formatting.Indented);
        jsonList.Add(jsonItem);
    }
    Console.WriteLine();
    Console.WriteLine(jsonList);
    Menu();
}

void SaveToDatabase(List<int> list, int timeToSort, string direction)
{
    List<string> strings = list.ConvertAll<string>(x => x.ToString());
    var stringList = String.Join(", ", strings);

    NumberList numList = new NumberList()
    {
        NumList = stringList,
        MilliSecondsTaken = timeToSort,
        SortDirection = direction
    };

    var context = new Context();
    context.NumberList.Add(numList);
    context.SaveChanges();
}



