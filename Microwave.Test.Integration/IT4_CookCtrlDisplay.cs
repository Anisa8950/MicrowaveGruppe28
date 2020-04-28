using System;
using NUnit.Framework;
using NSubstitute;
using System.IO;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Controllers;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT4_CookCtrlDisplay
    {
        private IDisplay _display;
        private IOutput _output;
        private IPowerTube _powertube;
        private ITimer _timer;
        private ICookController _CookController;
        private StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _display = new Display(_output);
            _timer = Substitute.For<ITimer>();
            _powertube = Substitute.For<IPowerTube>();
            _CookController = new CookController(_timer, _display, _powertube);
            _stringWriter = new StringWriter();
        }

        [Test]
        public void TimerOnDisplay_output_DisplayShowsMinSec()
        {
            using (_stringWriter)
            {
                Console.SetOut(_stringWriter);

                _CookController.StartCooking(50, 2);
                _timer.TimerTick += Raise.Event();
            }
            Assert.That(_stringWriter.ToString(), Is.EqualTo("Display shows: 00:00\r\n"));
        }
    }    
}
