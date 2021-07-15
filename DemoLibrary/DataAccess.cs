using DemoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public static class DataAccess
    {
        private static string personTextFile = "PersonText.txt";

        public static void AddNewPerson(PersonModel person)
        {
            List<PersonModel> people = GetAllPeople();
            
            AddPersonToPeopleList(people, person);

            List<string> lines = ConvertModelsToCsv(people);
            
            File.WriteAllLines(personTextFile, lines);
        }

        public static void AddPersonToPeopleList(List<PersonModel> people, PersonModel person)
        {
            if (string.IsNullOrWhiteSpace(person.FirstName))
            {
                throw new ArgumentException("You passed in an invalid parameter", "FirstName");
            }
            if (string.IsNullOrWhiteSpace(person.LastName))
            {
                throw new ArgumentException("You passed in an invalid parameter", "LastName");
            }
            people.Add(person);
        }

        public static List<string> ConvertModelsToCsv(List<PersonModel> people)
        {
            List<string> output = new List<string>();

            foreach (PersonModel user in people)
            {
                output.Add($"{user.FirstName}, {user.LastName}");
            }

            return output;
        }

        public static List<PersonModel> GetAllPeople()
        {
            string[] personFile = ReadPersonFile(personTextFile);
            List<PersonModel> people = ConvertCsvToModel(personFile);
            return people;
        }

        public static string[] ReadPersonFile(string personFile)
        {
            string[] content = File.ReadAllLines(personFile);
            return content;
        }

        public static List<PersonModel> ConvertCsvToModel(string[] peopleCsv)
        {
            List<PersonModel> people = new List<PersonModel>();
            foreach (string person in peopleCsv)
            {
                string[] data = person.Split(',');
                people.Add(new PersonModel { FirstName = data[0], LastName = data[1] });
            }

            return people;
        }
    }
}
