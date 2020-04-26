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
    class BU_IT5_PowerTube
    {
        private IDisplay _display;
        private ITimer _timer;
        private PowerTube _powertube;
        private Output _output;
        private CookController _cookController;
        public StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
            _timer = Substitute.For<ITimer>();

            _output = new Output();
            _powertube = new PowerTube(_output);
            _cookController = new CookController(_timer,_display,_powertube);
        }

        [Test]
        public void PowerTube_TurnOn_PowerTubeIsTurnedOn()
        {
            using (_stringWriter = new StringWriter())
            {
                Console.SetOut(_stringWriter);
                _cookController.StartCooking(50, 60);
            }
            
            Assert.That(_stringWriter.ToString(), Is.EqualTo("PowerTube works with 50\r\n"));
        }

        [Test]
        public void PowerTube_TurnOn_PowerOutOfRangeException()
        {
            Assert.That(() => _cookController.StartCooking(101,60), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void PowerTube_TurnOn_PowerTurnIsAlreadyTurnedOn()
        {
            _cookController.StartCooking(50,60);
            Assert.That(() => _cookController.StartCooking(50, 60), Throws.TypeOf<ApplicationException>());
        }

    }
}
