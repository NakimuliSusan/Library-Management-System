using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System
{
    public class Linq
    {

        static void LinqBasics()
        {
            // Specify the data source.
            int[] scores = [97, 92, 81, 60];

            int[] numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20];

            // Define the query expression.
            IEnumerable<int> scoreQuery =
                from score in scores
                where score > 80
                select score;

            // Execute the query.
            foreach (var i in scoreQuery)
            {
                Console.Write(i + " ");
            }

            // Immediate Execution -> he data source is read and the operation is performed once
            var evenNumQuery = from num in numbers
                               where (num % 2) == 0
                               select num;

            int evenNumCount = evenNumQuery.Count();

            // Force immediate exceution and cache the results by calling the TOList or ToArray methods
            List<int> numQuery2 = (from num in numbers
                                   where (num % 2) == 0
                                   select num).ToList();

            // or like this:
            // numQuery3 is still an int[]

            var numQuery3 = (from num in numbers
                             where (num % 2) == 0
                             select num).ToArray();


            IEnumerable<int> queryFactorsOfFour = from num in numbers
                                                  where num % 4 == 0
                                                  select num;

            // Store the results in a new variable
            // without executing a foreach loop.
            var factorsofFourList = queryFactorsOfFour.ToList();

            // Read and write from the newly created list to demonstrate that it holds data.
            Console.WriteLine(factorsofFourList[2]);
            factorsofFourList[2] = 0;
            Console.WriteLine(factorsofFourList[2]);


            string[] names = ["Svetlana Omelchenko", "Claire O'Donnell", "Sven Mortensen", "Cesar Garcia"];
            IEnumerable<string> queryFirstNames =
                from name in names
                let firstName = name.Split(' ')[0]
                select firstName;

            foreach (var s in queryFirstNames)
            {
                Console.Write(s + " ");
            }

            //Output: Svetlana Claire Sven Cesar


            //Query syntax:
            IEnumerable<int> numQuery1 =
                from num in numbers
                where num % 2 == 0
                orderby num
                select num;

            //Method syntax:
            IEnumerable<int> numQuery = numbers
                .Where(num => num % 2 == 0)
                .OrderBy(n => n);

            foreach (int i in numQuery1)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine(System.Environment.NewLine);
            foreach (int i in numQuery)
            {
                Console.Write(i + " ");
            }


            List<int> values = [5, 4, 1, 3, 9, 8, 6, 7, 2, 0];

            // The query variables can also be implicitly typed by using var

            // Query #1.
            IEnumerable<int> filteringQuery =
                from value in values
                where value is < 3 or > 7
                select value;

            // Query #2.
            IEnumerable<int> orderingQuery =
                from value in values
                where value is < 3 or > 7
                orderby value ascending
                select value;

            // Query #3.
            string[] groupingQuery = ["carrots", "cabbage", "broccoli", "beans", "barley"];
            IEnumerable<IGrouping<char, string>> queryFoodGroups =
                from item in groupingQuery
                group item by item[0];




           /* int[] ids = [111, 114, 112];

            string[] students = ["Susan", "Mary", "Nakimuli"];

            var queryNames = from student in students
                             where ids.Contains(student.ID)
                             select new
                             {
                                 student.LastName,
                                 student.ID
                             };

            foreach (var name in queryNames)
            {
                Console.WriteLine($"{name.LastName}: {name.ID}");
            }*/

            /* Output:
                Garcia: 114
                O'Donnell: 112
                Omelchenko: 111
             */

           

            /* Output:
                Adams: 120
                Feng: 117
                Garcia: 115
                Tucker: 122
             */

        }
    }
}
