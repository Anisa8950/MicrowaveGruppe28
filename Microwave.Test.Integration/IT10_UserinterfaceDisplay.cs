using System;
using System.IO;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT10_UserinterfaceDisplay
    {
        private ICookController _cookController;
        private Output _output;
        private Button _powerButton;
        private Button _timeButton;
        private Button _startCancelButton;
        private Display _display;
        private Door _door;
        private Light _light;
        private UserInterface _userInterface;
        private StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _cookController = Substitute.For<ICookController>();
            
            _output = new Output();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _display = new Display(_output);
            _door = new Door();
            _light = new Light(_output);

            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);

            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [Test]
        public void PowerButton_PressedOnce_DisplayShowPowerCalled()
        {
            _powerButton.Press();
            Assert.That(_stringWriter.ToString().Contains("50 W"));
        }

        [Test]
        public void PowerButton_PressedMultibleTimes_DisplayShowPowerCalled()
        {
            for (int i = 1; i <= 15; i++)
            { 
                _powerButton.Press();
            }

            Assert.That(_stringWriter.ToString().Contains("700 W"));
        }

        [Test]
        public void PowerButton_PressedResets_DisplayShowPowerCalled()
        {
            for (int i = 1; i <= 16; i++)
            {
                _powerButton.Press();
            }

            Assert.That(!_stringWriter.ToString().Contains("750 W"));
        }

        [Test]
        public void TimeButton_PressedOnce_DisplayShowTime()
        {
            _powerButton.Press();
            _timeButton.Press();
            Assert.That(_stringWriter.ToString().Contains("01:00"));
        }

        [Test]
        public void TimeButton_PressedMultipleTimes_DisplayShowTime()
        {
            _powerButton.Press();

            for (int i = 1; i < 10; i++)
            {
                _timeButton.Press();
            }
            
            Assert.That(_stringWriter.ToString().Contains("09:00"));
        }

        [Test]
        public void StartCancelButtonPressed_stateSetTime_displayClear()
        {
            _powerButton.Press();
            _startCancelButton.Press();

            Assert.That(_stringWriter.ToString().Contains("cleared"));
        }

        [Test]
        public void StartCancelButtonPressed_stateCooking_displayClear()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();

            Assert.That(_stringWriter.ToString().Contains("cleared"));
        }




    }
}
