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
        private IUserInterface _userInterface;
        private Output _output;
        private Timer _timer;
        private CookController _cookController;
        private StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
            _powerTube = Substitute.For<IPowerTube>();
            _userInterface = Substitute.For<IUserInterface>();

            _output = new Output();
            _timer = new Timer();
            _cookController = new CookController(_timer,_display,_powerTube,_userInterface);

            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [Test]
        public void StartCooking_TimerTick_WaitForTick_DisplayShowTime()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            _timer.TimerTick += (sender, args) => pause.Set();
            _cookController.StartCooking(50,60);

            // wait for one tick
            Assert.That(pause.WaitOne(1100));
            _display.Received(1).ShowTime(Arg.Any<int>(), Arg.Any<int>());
        }

        [Test]
        public void StartCooking_TimerTick_DoNotWaitForTick_DisplayShowsNothing()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            _timer.TimerTick += (sender, args) => pause.Set();
            _cookController.StartCooking(50,60);

            // wait shorter than a tick
            Assert.That(!pause.WaitOne(900));
            _display.DidNotReceive().ShowTime(Arg.Any<int>(), Arg.Any<int>());
        }

        [Test]
        public void StartCooking_TimerExpires_WaitForTick_CookingIsDone()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            _timer.Expired += (sender, args) => pause.Set();
            _cookController.StartCooking(50,2);

            // wait for expiration
            Assert.That(pause.WaitOne(2100));
            _userInterface.Received(1).CookingIsDone();
        }

        [Test]
        public void StartCooking_TimerExpires_DoNotWaitForTick_CookingNotDone()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            _timer.Expired += (sender, args) => pause.Set();
            _cookController.StartCooking(50, 2);

            // wait for expiration
            Assert.That(!pause.WaitOne(1900));
            _userInterface.DidNotReceive().CookingIsDone();
        }

        [Test]
        public void StopCooking_TimerTick_DoNotWaitForTick_CookingNotDone()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            _timer.Expired += (sender, args) => pause.Set();
            _cookController.StartCooking(50, 2);

            // wait for expiration
            Assert.That(!pause.WaitOne(1900));
            _userInterface.DidNotReceive().CookingIsDone();
        }

    }
}
