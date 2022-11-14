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

        //Paging with Skip & Take
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            int startIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(startIndex).Take(resultsPerPage);
        }

        //Variables
        static public void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 6, 7, 8, 9, 10 };

            IEnumerable<int> aboveAverage = from number in numbers
                                            let average = numbers.Average()
                                            let nSquared = Math.Pow(number, 2)
                                            where nSquared > average
                                            select number;

            Console.WriteLine("Average: {0}", numbers.Average());

            foreach (int number in aboveAverage)
            {
                Console.WriteLine("Number: {0} Square {1}", number, Math.Pow(number, 2));
            }
        }

        //ZIP
        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6 };
            string[] stringNumbers = { "one", "two", "three", "four", "five", "six" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + "=" + word);
        }

        //Repeat
        static public void RepeatRangeLinq()
        {
            //Generate collention from 1 - 1000 --> RANGE
            IEnumerable<int> first1000 = Enumerable.Range(0, 1000);

            //Repeat a value N times
            IEnumerable<string> fiveXs = Enumerable.Repeat("X", 5); // { "X","X","X","X","X" }
        }

        static public void StudentsLinq()
        {
            Student[] classRoom = new[]
            {
                new Student
                {
                    Id= 1,
                    Name= "Martin",
                    Certified= true,
                    Grade = 90
                },
                new Student
                {
                    Id= 2,
                    Name= "Jueana",
                    Certified= true,
                    Grade = 70
                },
                new Student
                {
                    Id= 3,
                    Name= "Pepe",
                    Certified= false,
                    Grade = 45
                },
                new Student
                {
                    Id= 4,
                    Name= "Rocio",
                    Certified= true,
                    Grade = 95
                },
                new Student
                {
                    Id= 5,
                    Name= "Carlos",
                    Certified= false,
                    Grade = 35
                },
            };

            IEnumerable<Student> certifiedStudents = from student in classRoom
                                                     where student.Certified
                                                     select student;

            IEnumerable<Student> noCertificatedStudents = from student in classRoom
                                                          where student.Certified == false
                                                          select student;

            IEnumerable<string> approvedStudentsName = from student in classRoom
                                                       where student.Grade >= 50 && student.Certified == true
                                                       select student.Name;
        }

        //ALL
        static public void AllLinq()
        {
            List<int> numbers = new() { 1, 2, 3, 4, 5, 6 };

            bool allAreSmallerThan10 = numbers.All(x => x < 10); //True
            bool allAreBiggerOrEqualthan2 = numbers.All(x => x >= 2); //False

            List<int> emptyList = new();
            bool allNumbersAreGreaterThan0 = emptyList.All(x => x >= 0);
        }

        //Aggregate
        static public void AggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //Sum all numbers
            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);
            //0, 1 => 1
            //1, 2 => 3
            //3, 3 => 6
            //etc

            string[] words = { "Hello,", "my", "name", "is", "Inmortal" };
            string greeting = words.Aggregate((prevGreeting, current) => prevGreeting + current);
            //"", "Hello," => Hello,
            //"Hello,", "my" => Hello, my
            //"Hello, my", "name" => Hello, my name
            //etc

        }

        //Distinct
        static public void DistinctValues()
        {
            int[] numbers = { 1, 2, 3, 1, 2, 6, 7, 1 };
            IEnumerable<int> values = numbers.Distinct();
        }

        //GroupBy
        static public void GroupByExalples()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Obtain only even numbers and generate two groups
            IEnumerable<IGrouping<bool, int>> grouped = numbers.GroupBy(x => x % 2 == 0);

            //We will have two groups:
            //1. The group that doesnt fit the condition (odd numbers)
            //2. The group that fits the condition (even numbers)

            foreach (IGrouping<bool, int> group in grouped)
            {
                foreach (int value in group)
                {
                    Console.WriteLine(value);// 1,3,5,7,9 ... 2,4,6,8,10 (first the odds and then the even)
                }
            }

            //Another example
            Student[] classRoom = new[]
{
                new Student
                {
                    Id= 1,
                    Name= "Martin",
                    Certified= true,
                    Grade = 90
                },
                new Student
                {
                    Id= 2,
                    Name= "Jueana",
                    Certified= true,
                    Grade = 70
                },
                new Student
                {
                    Id= 3,
                    Name= "Pepe",
                    Certified= false,
                    Grade = 45
                },
                new Student
                {
                    Id= 4,
                    Name= "Rocio",
                    Certified= true,
                    Grade = 95
                },
                new Student
                {
                    Id= 5,
                    Name= "Carlos",
                    Certified= false,
                    Grade = 35
                },
            };

            IEnumerable<IGrouping<bool, Student>> certifiedQuery = classRoom.GroupBy(student => student.Certified);

            //We obtain two groups
            //1. Not certified students
            //2. Certified students

            foreach (IGrouping<bool, Student> items in certifiedQuery)
            {
                Console.WriteLine("-------- {0} -------", items.Key);
                foreach (Student? student in items)
                {
                    Console.WriteLine(student);
                }
            }

        }

        static public void RelationsLinq()
        {
            List<Post> posts = new()
            {
                new Post()
                {
                    Id= 1,
                    Title = "My first title",
                    Content = "My first content",
                    Created = DateTime.Now,
                    Comments= new List<Comment>()
                    {
                        new Comment()
                        {
                            Id= 1,
                            Title = "My first comment",
                            Content = "my first content",
                            Created = DateTime.Now
                        },
                        new Comment()
                        {
                            Id= 2,
                            Title = "My secont comment",
                            Content = "my secont content",
                            Created = DateTime.Now
                        },
                        new Comment()
                        {
                            Id= 3,
                            Title = "My third comment",
                            Content = "my third content",
                            Created = DateTime.Now
                        },
                    }
                },
                new Post()
                {
                    Id= 2,
                    Title = "My secont title",
                    Content = "My secont content",
                    Created = DateTime.Now,
                    Comments= new List<Comment>()
                    {
                        new Comment()
                        {
                            Id= 4,
                            Title = "My first comment",
                            Content = "my first content",
                            Created = DateTime.Now
                        },
                        new Comment()
                        {
                            Id= 5,
                            Title = "My secont comment",
                            Content = "my secont content",
                            Created = DateTime.Now
                        },
                        new Comment()
                        {
                            Id= 6,
                            Title = "My third comment",
                            Content = "my third content",
                            Created = DateTime.Now
                        },
                    }
                },
            };

            var commentsContent = posts.SelectMany(post => post.Comments,
                                                (post, comment) => new { PostId = post.Id, CommentContent = comment.Content });
        }
    }
}
