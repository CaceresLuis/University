using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Models
{
    public class Services
    {
        //1. Search Users by email
        public static void SearchUsersByemail(string email)
        {
            User[] users = new User[]
            {
                new User()
                {
                    Id = 1,
                    CreatedAt= DateTime.Now,
                    DeletedAt= DateTime.Now,
                    UpdatedAt= DateTime.Now,
                    CreatedBy = "Lucho",
                    DeletedBy = "",
                    Email = "Maria@mail.com",
                    IsDeleted= false,
                    LastName = "Lopez",
                    Name = "Maria",
                    Password = "123456",
                    UpdatedBy = ""
                },
                new User()
                {
                    Id = 2,
                    CreatedAt= DateTime.Now,
                    DeletedAt= DateTime.Now,
                    UpdatedAt= DateTime.Now,
                    CreatedBy = "Lucha",
                    DeletedBy = "",
                    Email = "Carola@mail.com",
                    IsDeleted= false,
                    LastName = "Gimenez",
                    Name = "Carola",
                    Password = "123458",
                    UpdatedBy = ""
                },
                new User()
                {
                    Id = 3,
                    CreatedAt= DateTime.Now,
                    DeletedAt= DateTime.Now,
                    UpdatedAt= DateTime.Now,
                    CreatedBy = "Yoni",
                    DeletedBy = "",
                    Email = "Cucha@mail.com",
                    IsDeleted= false,
                    LastName = "Luna",
                    Name = "Cuchita",
                    Password = "78564232",
                    UpdatedBy = ""
                },
            };

            User userByEmail = users.First(u => u.Email == email);
        }

        //2. Search for older students
        public static void SearchForOlderStudent()
        {
            Student[] getStudentList = Students(1);
            int year = DateTime.Now.Year;

            List<Student> getStudent = getStudentList.Where(s => (s.Dob.Year - year) >= 18).ToList();
        }

        //3. Find students who have at least one course
        public static void FindStudentHaveCourse()
        {
            Student[] StudentList = Students(1);

            List<Student> students = StudentList.Where(c => c.Courses.Count >= 1).ToList();
        }



        private static Student[] Students(int numList)
        {
            var getCourse = Courses();
            //var courses = getCourse.Where(c => c.Categories.Contains("programacion"));

            Student[] students;
            if (numList == 1)
            {
                students = new[]
          {
                new Student()
                {
                    Id= 1,
                    CreatedAt= DateTime.Now,
                    DeletedAt= DateTime.Now,
                    UpdatedAt= DateTime.Now,
                    CreatedBy = "Yoni",
                    DeletedBy = "",
                    Dob = new DateTime(1999, 10, 25),
                    FirstName = "Maraquita",
                    LastName = "Rodrigez"
                },
                new Student()
                {
                    Id= 2,
                    CreatedAt= DateTime.Now,
                    DeletedAt= DateTime.Now,
                    UpdatedAt= DateTime.Now,
                    CreatedBy = "Yoni",
                    DeletedBy = "",
                    Dob = new DateTime(2005, 5, 5),
                    FirstName = "Pepe",
                    LastName = "Mendez",
                    Courses = Courses()
                },
                new Student()
                {
                    Id= 3,
                    CreatedAt= DateTime.Now,
                    DeletedAt= DateTime.Now,
                    UpdatedAt= DateTime.Now,
                    CreatedBy = "Yoni",
                    DeletedBy = "",
                    Dob = new DateTime(2001, 3, 2),
                    FirstName = "Lorena",
                    LastName = "Zanches",
                    Courses = Courses()
                }
            };
            }
            else
            {
                students = new[]
                {
                    new Student()
                    {
                        Id= 4,
                        CreatedAt= DateTime.Now,
                        DeletedAt= DateTime.Now,
                        UpdatedAt= DateTime.Now,
                        CreatedBy = "Yoni",
                        DeletedBy = "",
                        Dob = new DateTime(1985, 7, 12),
                        FirstName = "Sandra",
                        LastName = "Piedra",
                    }
                };
            }

            return students;
        }

        private static Course[] Courses()
        {
            Category[] categories = new[]
            {
                new Category()
                {
                    Id= 1,
                    CreatedAt= DateTime.Now,
                    DeletedAt= DateTime.Now,
                    UpdatedAt= DateTime.Now,
                    CreatedBy = "Yoni",
                    DeletedBy = "",
                    IsDeleted= false,
                    Name = "Informatica"
                },
                new Category()
                {
                    Id= 2,
                    CreatedAt= DateTime.Now,
                    DeletedAt= DateTime.Now,
                    UpdatedAt= DateTime.Now,
                    CreatedBy = "Yoni",
                    DeletedBy = "",
                    IsDeleted= false,
                    Name = "Programacion"
                },
            };

            var studentList = Students(1).Skip(1);

            Course[] courses = new[]
            {
                new Course()
                {
                    Id= 1,
                    Name = "Progra bonito",
                    CreatedAt= DateTime.Now,
                    DeletedAt= DateTime.Now,
                    UpdatedAt= DateTime.Now,
                    CreatedBy = "Yoni",
                    DeletedBy = "",
                    TargetAudiences= "",
                    Categories= new Category[]
                    {
                        categories[0]
                    },
                    Chapter = new Chapter()
                    {
                        Id= 1,
                        CreatedAt= DateTime.Now,
                        DeletedAt= DateTime.Now,
                        UpdatedAt= DateTime.Now,
                        CreatedBy = "Yoni",
                        DeletedBy = "",
                        List = "a, b, c",
                        CourseId= 1,
                    },
                    IsDeleted= false,
                    Level = Enums.Level.Intermediate,
                    LongDescription = "jeiofjeoifjewojfoewjofew",
                    Objetives = "",
                    Requirements = "",
                    ShortDescription = "asfsafgewge",
                    Students = Students(1)
                },
                new Course()
                {
                    Id= 2,
                    Name = "Progra bonito",
                    CreatedAt= DateTime.Now,
                    DeletedAt= DateTime.Now,
                    UpdatedAt= DateTime.Now,
                    CreatedBy = "Yoni",
                    DeletedBy = "",
                    TargetAudiences= "",
                    Categories= categories,
                    Chapter = new Chapter()
                    {
                        Id= 1,
                        CreatedAt= DateTime.Now,
                        DeletedAt= DateTime.Now,
                        UpdatedAt= DateTime.Now,
                        CreatedBy = "Yoni",
                        DeletedBy = "",
                        List = "a, b, c",
                        CourseId= 1,
                    },
                    IsDeleted= false,
                    Level = Enums.Level.Intermediate,
                    LongDescription = "jeiofjeoifjewojfoewjofew",
                    Objetives = "",
                    Requirements = "",
                    ShortDescription = "asfsafgewge",
                    Students = Students(1)
                },
            };

            return courses;
        }



    }
}
