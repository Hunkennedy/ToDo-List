using Api.Controllers;
using Api.Interfaces;
using FakeItEasy;
using Xunit;


namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var dataStore = A.Fake<ITodoTask>();
            var controller = new TodotaskController(dataStore);
        }
    }
}