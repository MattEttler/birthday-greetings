using System;
using Xunit;
using Moq;
using System.Collections.Generic;

namespace domain.service.test
{
    public class BirthdayGreetingsServiceTest
    {
        private int _debugSeed = 1;
        private Random _gen;
        private BirthdayGreetingsService _birthdayGreetingsService;
        private IEmployeeRepository _mockEmployeeRepository;
        private IGreetingChannel _mockGreetingChannel;

        public BirthdayGreetingsServiceTest()
        {
            _gen = new Random(_debugSeed);
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockGreetingChannel = new Mock<IGreetingChannel>();
            _birthdayGreetingsService = new BirthdayGreetingsService(_mockEmployeeRepository, _mockGreetingChannel);
        }

        private List<Employee> generateEmployees()
        {
            return new List<Employee>
            {
              new Employee {
                  dateOfBirth = DateTime.Now
              }
            };
        }

        /// <summary>
        /// sendGreetings identifies all employees with a date of birth matching the input date and sends those employees a greeting message.
        /// implement the IGreetingChannel port to control the greeting medium used to send the greeting(s). 
        /// </summary>      
        /// <param name="eligibilityDate"></param>
        /// <param name="employeeDateOfBirth"></param>
        [Fact]
        public void Returns_GreetedEmployees(DateTime eligibilityDate, DateTime employeeDateOfBirth)
        {
            var expectedResult = new List<Employee>();
            _mockGreetingChannel
                .Setup(c => c.sendGreeting(It.IsAny<Employee>()))
                    .Callback<Employee>(e => actualResult.Add(e));
            _mockEmployeeRepository
                .Setup(c => c.getAllEmployees())
                .Return(generateEmployees());

            var actualResult =
                _birthdayGreetingsService.sendGreetings(DateTime.Now);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
