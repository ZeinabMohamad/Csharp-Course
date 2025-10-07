using System;

namespace ConsoleAppExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an Employee object
            Employee emp = new Employee
            {
                Id = 1,
                FirstName = "Zeinab",
                LastName = "Mohamad"
            };

            // Use polymorphism: create IQuittable object referencing Employee
            IQuittable quittableEmployee = emp;

            // Call the Quit method via the interface
            quittableEmployee.Quit();

            // Wait for user input before closing
            Console.ReadLine();
        }
    }

    // Interface with a void method Quit
    public interface IQuittable
    {
        void Quit();
    }

    // Employee class implements IQuittable
    public class Employee : IQuittable
    {
        // Employee properties
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Implement Quit method from IQuittable
        public void Quit()
        {
            Console.WriteLine($"{FirstName} {LastName} (ID: {Id}) has quit the job.");
        }

        // Optional: override Equals and GetHashCode from previous exercise
        public override bool Equals(object obj)
        {
            var other = obj as Employee;
            if (other == null) return false;
            return this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        // Optional: Overload == and != (still valid from previous example)
        public static bool operator ==(Employee emp1, Employee emp2)
        {
            if (ReferenceEquals(emp1, emp2)) return true;
            if (ReferenceEquals(emp1, null) || ReferenceEquals(emp2, null)) return false;
            return emp1.Id == emp2.Id;
        }

        public static bool operator !=(Employee emp1, Employee emp2)
        {
            return !(emp1 == emp2);
        }
    }
}
