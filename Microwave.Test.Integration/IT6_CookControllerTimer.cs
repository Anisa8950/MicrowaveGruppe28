using System;
using System.IO;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;



namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT6_CookControllerTimer
    {
        private IDisplay _display;
        private IPowerTube _powerTube;
        private Output _output;
        private Timer _timer;
        private CookController _cookController;
        public StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
            _powerTube = Substitute.For<IPowerTube>();

            _output = new Output();
            _timer = new Timer();
            _cookController = new CookController(_timer,_display,_powerTube);
        }

        [Test]
        public void StartCooking_TimerStart()
        {
            _cookController.StartCooking(50,60);
            using (_stringWriter = new StringWriter())
            {
                Console.SetOut(_stringWriter);
                _cookController.StartCooking(50, 60);
            }

            
        }
    }
}
