using System;
using System.IO;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using Timer = MicrowaveOvenClasses.Boundary.Timer;
using System.Threading;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT11_UserinterfaceCookcontroller
    {
        private Button _powerButton;
        private Button _timeButton;
        private Button _startCancelButton;
        private Door _door;
        private Output _output;
        private Display _display;
        private Light _light;
        private PowerTube _powertube;
        private Timer _timer;
        private CookController _cookController;
        private UserInterface _userinterface;
        private StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _door = new Door();
            _output = new Output();
            _display = new Display(_output);
            _light = new Light(_output);
            _powertube = new PowerTube(_output);
            _timer = new Timer();
            _cookController = new CookController(_timer, _display, _powertube);
            _userinterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
            _cookController = new CookController(_timer, _display, _powertube, _userinterface);
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [Test]
        public void SetTimeStarte_StartCancelPressed_lightPowertubeONTimerStart()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            _timer.TimerTick += (sender, args) => pause.Set();

            _powerButton.Press();
            _timeButton.Press();


            _startCancelButton.Press();


            Assert.That(pause.WaitOne(120000));
            Assert.That(_stringWriter.ToString().Contains("50 W"));//powertube
            Assert.That(_stringWriter.ToString().Contains("on"));//light
            Assert.That(_stringWriter.ToString().Contains("01:00"));//timer display
            Assert.That(_stringWriter.ToString().Contains("00:30"));//timer display
            Assert.That(_stringWriter.ToString().Contains("00:00"));//timer display
        }

        public void CookingStarte_StartCancelPressed_lightPowertubeOffTimerStart()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            _timer.TimerTick += (sender, args) => pause.Set();

            _powerButton.Press();
            _timeButton.Press();


            _startCancelButton.Press();


            Assert.That(pause.WaitOne(60000));
            Assert.That(_stringWriter.ToString().Contains("50 W"));//powertube
            Assert.That(_stringWriter.ToString().Contains("on"));//light
            Assert.That(_stringWriter.ToString().Contains("01:00"));//timer display
        }
    }
}
