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
            Assert.Contains<PersonModel>(newPerson, people);
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
    }
}