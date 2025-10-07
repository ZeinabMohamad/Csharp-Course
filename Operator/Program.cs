using System;

namespace ConsoleAppExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create first Employee object
            Employee emp1 = new Employee
            {
                Id = 1,
                FirstName = "Zeinab",
                LastName = "Mohamad"
            };

            // Create second Employee object
            Employee emp2 = new Employee
            {
                Id = 1, // Same Id as zeinab to test equality
                FirstName = "Mohamed",
                LastName = "Mohamed"
            };

            // Compare using overloaded == operator
            if (emp1 == emp2)
            {
                Console.WriteLine("The employees are considered equal (same Id).");
            }
            else
            {
                Console.WriteLine("The employees are NOT equal.");
            }

            // Compare using overloaded != operator
            if (emp1 != emp2)
            {
                Console.WriteLine("The employees are different.");
            }
            else
            {
                Console.WriteLine("The employees are the same based on Id.");
            }

            // Wait for user input before closing the console
            Console.ReadLine();
        }
    }

    // Employee class defined inside the same file
    public class Employee
    {
        // Employee properties
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Overload == operator to compare Employee Ids
        public static bool operator ==(Employee emp1, Employee emp2)
        {
            // Handle null cases to prevent exceptions
            if (ReferenceEquals(emp1, emp2))
                return true;

            if (ReferenceEquals(emp1, null) || ReferenceEquals(emp2, null))
                return false;

            return emp1.Id == emp2.Id;
        }

        // Overload != operator as required
        public static bool operator !=(Employee emp1, Employee emp2)
        {
            return !(emp1 == emp2);
        }

        // Override Equals for consistency with ==
        public override bool Equals(object obj)
        {
            var other = obj as Employee;
            if (other == null) return false;
            return this.Id == other.Id;
        }

        // Override GetHashCode as best practice
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
