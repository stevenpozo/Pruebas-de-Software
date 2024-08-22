using Laboratorio1_3P.Models;
using MySqlX.XDevAPI.Common;

namespace SpecFlowTest.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        private readonly Calculator calculator = new Calculator();
        private int result;

        [Given("the first number is (.*)")]
        public void GivenTheFirstNumberIs(int number)
        {
           calculator.FirstNumber = number;
        }

        [Given("the second number is (.*)")]
        public void GivenTheSecondNumberIs(int number)
        {
            calculator.SecondNumber = number;
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            result = calculator.Add();
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(int result1)
        {
            result.Should().Be(result1);
        }
    }
}
