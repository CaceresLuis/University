namespace LinqSnippets
{
    public class Snippets
    {
        static public void BasicLinq()
        {
            string[] cars =
{
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Zeat leon"
            };

            //1 SELECT * of cars
            IEnumerable<string> carlist = from car in cars select car;

            foreach (string? car in carlist)
            {
                Console.WriteLine(car);
            }

            //2 SELECT WHERE car is Audi (SELECT AUDIs)
            IEnumerable<string> audiList = from car in cars where car.Contains("Audi") select car;
            foreach (string? audi in audiList)
            {
                Console.WriteLine(audi);
            }
        }

        //Number Examples
        static public void LinqNUmber()
        {

            List<int> numbers = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //Each Number multiplied by 3
            //Take all numbers, but 9
            //Order numbers by ascending value

            IOrderedEnumerable<int> processedNUmberList =
                numbers.Select(num => num * 3) //{3, 6, 9, etc}
                .Where(num => num != 9) //{all but the 9}
                .OrderBy(num => num); // at the end, we order ascendig
        }

        static public void SearchExamples()
        {
            List<string> textList = new()
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "f",
                "cj",
                "f",
                "c"
            };

            //1. First of all elemets
            string first = textList.First();

            //2. First element that id "c"
            string cText = textList.First(text => text.Equals("c"));

            //3. First elemet that containd "j"
            string jText = textList.First(text => text.Contains('j'));

            //4. First element that contains Z or defoult
            string? firstOrDefoultText = textList.FirstOrDefault(text => text.Contains('z')); //"" or first element that contains "z"

            //5. Last element that contains Z or defoult
            string? lastOrDefoultText = textList.LastOrDefault(text => text.Contains('z')); //"" or last element that contains "z"

            //6. single values
            string uniqueTexts = textList.Single();
            string? uniqueorDefoultTexts = textList.SingleOrDefault();


            int[] eventNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEventNumbers = { 0, 2, 6 };

            //Obtain {4, 8}
            IEnumerable<int> myEventNumbers = eventNumbers.Except(otherEventNumbers); // {4, 8}
        }

        static public void MultipleSelects()
        {
            //SELECT MANY
            string[] myOpinions =
            {
                "Opinion 1, text 1",
                "Opinion 2, text 2",
                "Opinion 3, text 3"
            };

            IEnumerable<string> myOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

            Enterprise[] enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Maranquito 1",
                    Employees = new Employee[]
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "marinita",
                            Email = "mari@nita.com",
                            Salary = 2500
                        },new Employee
                        {
                            Id = 2,
                            Name = "Pepe",
                            Email = "pepe@nita.com",
                            Salary = 2100
                        },new Employee
                        {
                            Id = 3,
                            Name = "Loreno",
                            Email = "lore@nita.com",
                            Salary = 2000
                        },new Employee
                        {
                            Id = 4,
                            Name = "Luz",
                            Email = "Luce@nita.com",
                            Salary = 1700
                        }
                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "Maranquito 3",
                    Employees = new Employee[]
                    {
                        new Employee
                        {
                            Id = 5,
                            Name = "Steven",
                            Email = "tutu@yito.com",
                            Salary = 2590
                        },new Employee
                        {
                            Id = 6,
                            Name = "Marco",
                            Email = "polo@nita.com",
                            Salary = 1100
                        },new Employee
                        {
                            Id = 7,
                            Name = "Sandra",
                            Email = "sandra@nita.com",
                            Salary = 700
                        },new Employee
                        {
                            Id = 8,
                            Name = "Mariano",
                            Email = "maria@nita.com",
                            Salary = 600
                        }
                    }
                }
            };

            //Obtain all employees all Enterprises
            IEnumerable<Employee> employeeList = enterprises.SelectMany(enterprises => enterprises.Employees);

            //Know if any list is empty
            bool hasEnterprises = enterprises.Any();

            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            //all enterprises at least employee with at leas $1000 of salary
            bool hasEmployeeWithSalaryMoreThanOrEqual1000 =
                enterprises.Any(enterprise => enterprise.Employees.Any(employee => employee.Salary > 1000));
        }

        static public void LinqCollection()
        {
            List<string> firstList = new() { "a", "b", "c" };
            List<string> secondList = new() { "a", "c", "d" };

            //INNER JOIN
            var commonResult = from element in firstList
                               join secondElemnt in secondList
                               on element equals secondElemnt
                               select new { element, secondElemnt };

            var commonResult2 = firstList.Join(
                secondList,
                element => element,
                secondElement => secondElement,
                (element, secondElement) => new { element, secondElement });

            //OUTER JOIN - LEFT
            var leftOuterJoin = from element in firstList
                                join secondelement in secondList
                                on element equals secondelement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };

            var leftOuterJoin2 = from element in firstList
                                 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElemnet = secondElement };

            //OUTER JOIN - RIGHT
            var rightOuterJoin = from secondElement in secondList
                                 join element in firstList
                                 on secondElement equals element
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where secondElement != temporalElement
                                 select new { Element = secondElement };

            //UNION
            var unionList = leftOuterJoin.Union(rightOuterJoin);
        }

        static public void SkipTakeLinq()
        {
            int[] myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };

            //SKIP
            IEnumerable<int> skipTwoFirstValues = myList.Skip(2); //{ 3,4,5,6,7,8,9,10 }
            IEnumerable<int> skipLastTwoValues = myList.SkipLast(2); //{ 1,2,3,4,5,6,7,8 }
            IEnumerable<int> skipWhileSmallerThan4 = myList.SkipWhile(num => num < 4); //{ 4,5,6,7,8 }

            //TAKE
            IEnumerable<int> takeTwoFirstValues = myList.Take(2); //{ 1,2 }
            IEnumerable<int> takeLastTwoValues = myList.SkipLast(2); //{ 9,10 }
            IEnumerable<int> takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4); //{ 1,2,3 }
        }
    }
}
