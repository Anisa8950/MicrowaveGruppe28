using System;
using System.IO;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT8_DoorUserinterface
    {
        private UserInterface _userInterface;
        private Door _door;
        private Button _powerButton;
        private Button _timeButton;
        private Button _startCancelButton;
        private IDisplay _display;
        private ILight _light;
        private ICookController _cookController;

        [SetUp]
        public void Setup()
        {
            _door = new Door();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _display = Substitute.For<IDisplay>();
            _light = Substitute.For<ILight>();
            _cookController = Substitute.For<ICookController>();

            _userInterface = new UserInterface(_powerButton,_timeButton,_startCancelButton,_door,_display,_light,_cookController);
        }

        [Test]
        public void Ready_Open_TurnOnIsCalled()
        {
            _door.Open();
            _light.Received(1).TurnOn();
            _display.Received(0).Clear();
        }

        [Test]
        public void Ready_OpenClose_TurnOffIsCalled()
        {
            _door.Open(); //set doorOpen state 
            _door.Close();
            _light.Received(1).TurnOff();
        }

        [Test]
        public void SetPower_Open_TurnOnAndClearIsCalled()
        {
            _powerButton.Press(); // set setPower state
            _door.Open();
            _light.Received(1).TurnOn();
            _display.Received(1).Clear();
        }

        [Test]
        public void SetTime_Open_TurnOnAndClearIsCalled()
        {
            _powerButton.Press(); // set setPower state
            _timeButton.Press(); // then set setTime state
            _door.Open();
            _light.Received(1).TurnOn();
            _display.Received(1).Clear();
        }

        [Test]
        public void Cooking_Open_TurnOnAndClearIsCalled()
        {
            _powerButton.Press(); // set setPower state
            _timeButton.Press(); // then set setTime state
            _startCancelButton.Press(); // then set cooking state
            _door.Open();
            _cookController.Received(1).Stop();
            _display.Received(1).Clear();
        }
    }
}
