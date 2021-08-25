using System;
using todojorge.Functions.Functions;
using todojorge.Tests.Helpers;
using Xunit;

namespace todojorge.Tests.Tests
{
    public class ScheduleFunctionTest
    {
        [Fact]
        public void ScheduleFunctionTest_Should_Log_Message()
        {
            //Arrange
            MockCloudTableTodos mockTodos = new MockCloudTableTodos(new Uri("http://127.0.0.1:10002/devstoreaccount1/reports"));
            ListLogger logger = (ListLogger)TestFactory.CreateLogger(LoggerTypes.List);

            //Act
            ScheduleFunction.Run(null, mockTodos, logger);
            string message = logger.Logs[0];

            //Assert
            Assert.Contains("Deleting completed", message);
        }
    }
}
