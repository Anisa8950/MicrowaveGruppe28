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
    public class IT4_CookControllerDisplay
    {
        private Display _display;
        private Output _output;
        private IPowerTube _powertube;
        private ITimer _timer;
        private CookController _CookController;
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
            Console.SetOut(_stringWriter);
        }

        [TestCase("00", "01")]
        [TestCase("00", "59")]
        [TestCase("01", "00")]
        [TestCase("02", "00")]
        [TestCase("02", "47")]
        public void TimerOnDisplay_output_DisplayShowsMin(string min, string sek)
        {
            _timer.TimeRemaining.Returns(Convert.ToInt32(min)*60 + Convert.ToInt32(sek));
            _timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            Assert.That(_stringWriter.ToString().Contains(min +":"+ sek));
        }
    }    
}
