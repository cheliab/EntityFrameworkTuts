using System.Collections.Generic;
using SortTut.Models;

namespace SortTut.Data
{
    public static class InitData
    {
        public static void Start(UsersContext context)
        {
            Company oracle = new Company { Name = "Oracle"};
            Company google = new Company { Name = "Google"};
            Company microsoft = new Company { Name = "Microsoft"};
            Company apple = new Company { Name = "apple"};

            var companies = new List<Company>
            {
                oracle,
                google,
                microsoft,
                apple
            };
            
            var users = new List<User>
            {
                new User{ Name = "Павел Березкин", Company = apple, Age = 30},
                new User{ Name = "Олег Васильев", Company = oracle, Age = 25},
                new User{ Name = "Алексей Шнуров", Company = microsoft, Age = 20},
                new User{ Name = "Сергей Светлов", Company = google, Age = 23}
            };

            context.Companies.AddRange(companies);
            context.Users.AddRange(users);

            context.SaveChanges();
        }
    }
}