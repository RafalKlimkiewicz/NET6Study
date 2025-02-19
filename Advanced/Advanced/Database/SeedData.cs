using Advanced.Models;
using Microsoft.EntityFrameworkCore;

namespace Advanced.Database
{
    public static class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();
            if (context.People.Count() == 0 && context.Departments.Count() == 0 &&
                context.Locations.Count() == 0)
            {

                Department d1 = new Department { Name = "Sales" };
                Department d2 = new Department { Name = "Development" };
                Department d3 = new Department { Name = "Support" };
                Department d4 = new Department { Name = "Facilities" };

                context.Departments.AddRange(d1, d2, d3, d4);
                context.SaveChanges();

                Location l1 = new Location { City = "Oakland", State = "CA" };
                Location l2 = new Location { City = "San Jose", State = "CA" };
                Location l3 = new Location { City = "New York", State = "NY" };
                
                context.Locations.AddRange(l1, l2, l3);

                context.People.AddRange(
                    new Person
                    {
                        FirstName = "Francesca",
                        Surname = "Jacobs",
                        Department = d2,
                        Location = l1
                    },
                    new Person
                    {
                        FirstName = "Charles",
                        Surname = "Fuentes",
                        Department = d2,
                        Location = l3
                    },
                    new Person
                    {
                        FirstName = "Bright",
                        Surname = "Becker",
                        Department = d4,
                        Location = l1
                    },
                    new Person
                    {
                        FirstName = "Murphy",
                        Surname = "Lara",
                        Department = d1,
                        Location = l3
                    },
                    new Person
                    {
                        FirstName = "Beasley",
                        Surname = "Hoffman",
                        Department = d4,
                        Location = l3
                    },
                    new Person
                    {
                        FirstName = "Marks",
                        Surname = "Hays",
                        Department = d4,
                        Location = l1
                    },
                    new Person
                    {
                        FirstName = "Underwood",
                        Surname = "Trujillo",
                        Department = d2,
                        Location = l1
                    },
                    new Person
                    {
                        FirstName = "Randall",
                        Surname = "Lloyd",
                        Department = d3,
                        Location = l2
                    },
                    new Person
                    {
                        FirstName = "Guzman",
                        Surname = "Case",
                        Department = d2,
                        Location = l2
                    });
                context.SaveChanges();
            }
        }
    }
}