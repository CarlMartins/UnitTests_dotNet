using System;
using System.Collections.Generic;
using DemoLibrary.Models;
using Xunit;

namespace DemoLibrary.Tests
{
    public class DataAccess_Tests
    {
        [Fact]
        public void AddPersonToPeopleList_ShouldWork()
        {
            PersonModel newPerson = new PersonModel
            {
                FirstName = "Carlos",
                LastName = "Martins"
            };
            List<PersonModel> people = new List<PersonModel>();
            
            DataAccess.AddPersonToPeopleList(people, newPerson);
            
            Assert.True(people.Count == 1);
            Assert.Contains(newPerson, people);
        }
        
        [Theory]
        [InlineData("Carlos", "", "LastName")]
        [InlineData("", "Martins", "FirstName")]
        public void AddPersonToPeopleList_ShouldFail(string firstName, string lastName, string param)
        {
            PersonModel newPerson = new PersonModel
            {
                FirstName = firstName,
                LastName = lastName
            };
            List<PersonModel> people = new List<PersonModel>();

            Assert.Throws<ArgumentException>(param, () => DataAccess.AddPersonToPeopleList(people, newPerson));
        }

        [Fact]
        public void ConvertModelsToCSV_ShouldWork()
        {
            List<PersonModel> people = new List<PersonModel>
            {
                new PersonModel
                {
                    FirstName = "Carlos",
                    LastName = "Martins"
                },
                new PersonModel
                {
                    FirstName = "Bruna",
                    LastName = "Oliveira"
                }
            };

            var output = DataAccess.ConvertModelsToCsv(people);
            
            Assert.Equal("Carlos, Martins", output[0]);
            Assert.Equal("Bruna, Oliveira", output[1]);
            Assert.True(output.Count == 2);
        }

        [Fact]
        public void ConvertCsvToModel_ShouldWork()
        {
            string[] people =
            {
                "Carlos, Martins",
                "Bruna, Oliveira"
            };

            var actual = DataAccess.ConvertCsvToModel(people);
            
            Assert.True(actual != null);
            Assert.True(actual[0].FirstName == "Carlos");
            Assert.True(actual.Count == 2);
        }
    }
}