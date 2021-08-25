using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using System;
using System.IO;
using todojorge.Common.Models;
using todojorge.Functions.Entities;

namespace todojorge.Tests.Helpers
{
    public class TestFactory
    {
        public static TodoEntity GetTodoEntity()
        {
            return new TodoEntity
            {
                ETag = "",
                PartitionKey = "TODO",
                RowKey = Guid.NewGuid().ToString(),
                CreatedTime = DateTime.UtcNow,
                IsCompleted = false,
                TaskDescription = "Task kill the humans."
            };
        }

        public static DefaultHttpRequest CrateHttpRequest(Guid todoId, Todo todoRequest)
        {
            string request = JsonConvert.SerializeObject(todoRequest);
            return new DefaultHttpRequest(new DefaultHttpContext())
            {
                Body = GenerateStreamFromSting(request),
                Path = $"/{todoId }"
            };
        }

        public static DefaultHttpRequest CrateHttpRequest(Guid todoId)
        {
            return new DefaultHttpRequest(new DefaultHttpContext())
            {
                Path = $"/{todoId }"
            };
        }

        public static DefaultHttpRequest CrateHttpRequest(Todo todoRequest)
        {
            string request = JsonConvert.SerializeObject(todoRequest);
            return new DefaultHttpRequest(new DefaultHttpContext())
            {
                Body = GenerateStreamFromSting(request),
            };
        }

        public static DefaultHttpRequest CrateHttpRequest()
        {
            return new DefaultHttpRequest(new DefaultHttpContext());
        }

        public static Todo GetTodoRequest()
        {
            return new Todo
            {
                CreatedTime = DateTime.Now,
                IsCompleted = false,
                TaskDescription = "Try to conquer the world."
            };
        }

        private static Stream GenerateStreamFromSting(string stringToConvert)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(stringToConvert);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static ILogger CreateLogger(LoggerTypes type = LoggerTypes.Null)
        {
            ILogger logger;
            if (type == LoggerTypes.Null)
            {
                logger = new ListLogger();
            }
            else
            {
                logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");
            }

            return logger;
        }
    }
}
