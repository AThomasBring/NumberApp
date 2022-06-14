using System.Diagnostics;
using System.Linq;

Console.WriteLine("Please input any int and press enter inbetween each number.");
Console.WriteLine("When the list is done press enter with no input to get options");

List<int> numbers = new List<int>();
var timeToSortAsc = 0;
var timeToSortDesc = 0;


while (true)
{
    var input = Console.ReadLine();
    int value;
    if(input == null || input == "")
    {
        break;
    }
    else if(!string.IsNullOrEmpty(input) && int.TryParse(input, out value))
    {
        numbers.Add(value);
    }    
    else
    {
        Console.WriteLine("That was not a valid number please try again");
    }

}
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
        Console.WriteLine("Here is your list in ascending order: ");
        ascList.ForEach(num => { Console.Write(num + " "); });
        Console.WriteLine();
        Console.WriteLine("It took: " + timeToSortAsc + " milliSeconds");
        break;
    case "2":
        Console.WriteLine("Here is your list in descending order: ");
        descList.ForEach(num => { Console.Write(num + " "); });
        Console.WriteLine();
        Console.WriteLine("It took: " + timeToSortDesc + " milliSeconds");
        break;
}

//Save to database


Console.WriteLine("Choose what to do next:");
Console.WriteLine("\t1 - Convert Lists to JSON");
Console.WriteLine("\t2 - Exit");

switch (Console.ReadLine())
{
    case "1":
        Console.WriteLine("Here is your list in ascending order: ");
        ascList.ForEach(num => { Console.Write(num + " "); });
        break;
    case "2":
        Console.WriteLine("Here is your list in descending order: ");
        descList.ForEach(num => { Console.Write(num + " "); });
        break;

}


