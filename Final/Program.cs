using ConsoleApp;
using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Creating database and adding student...");

        // Create a new instance of the database context
        using (var context = new StudentContext())
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Create a new student object
            var student = new Student
            {
                FirstName = "Zeinab",
                LastName = "Mohamad",
                DateOfBirth = new DateTime(1990,3, 21)
            };

            // Add the student to the Students DbSet
            context.Students.Add(student);

            // Save changes to the database
            context.SaveChanges();

            Console.WriteLine("The Student is added successfully!");

            // Retrieve and display the student to verify
            var savedStudent = context.Students.FirstOrDefault(s => s.FirstName == "Zeinab");
            if (savedStudent != null)
            {
                Console.WriteLine($" Student: {savedStudent.FirstName} {savedStudent.LastName}");
            }
        }

    
    }
}