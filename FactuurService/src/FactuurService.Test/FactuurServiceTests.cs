using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FactuurService;
using RabbitMQ;
using rabbitmq_demo;

namespace FactuurService.Test
{
    public class FactuurServiceTests
    {
        [Fact]
        public void DeFactuurServiceKanEenFactuurAanmakenCommandOntvangen()
        {
            var FactuurAanmakenCommand = new FactuurAanmaken
            {
                ID = 0
            };

            var service = new FactuurService();
            service.Execute(FactuurAanmakenCommand);
        }
    }
}
