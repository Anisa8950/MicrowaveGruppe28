using System;
using System.IO;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using System.Threading;
using NUnit.Framework.Constraints;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT6_CookControllerTimer
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
            
        }

        [Test]
        public void StartCooking_TimerTick_ShortEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            _timer.TimerTick += (sender, args) => pause.Set();
            _cookController.StartCooking(50,2);

            // wait for one tick, but no longer
            Assert.That(pause.WaitOne(1100));
            _display.Received(1).ShowTime(Arg.Any<int>(),Arg.Any<int>());
            _display.Received(1).ShowTime(0, 1);
        }
    }
}
