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
    public class IT7_ButtonUserinterface
    {
        private Button _powerButton;
        private Button _timeButton;
        private Button _startCancelButton;
        private IDisplay _display;
        private IDoor _door;
        private ILight _light;
        private ICookController _cookController;
        private UserInterface _userinterface;

        [SetUp]
        public void SetUp()
        {
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();
            _light = Substitute.For<ILight>();
            _cookController = Substitute.For<ICookController>();
            _userinterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
        }

        #region PowerButton
        [Test]
        public void StateReady_PowerPress_powerlevel50()
        {
            _powerButton.Press();
            _display.Received(1).ShowPower(50);
        }

        [Test]
        public void StateReady_fourteenPowerPress_powerlevel750()
        {
            for (int i = 0; i < 14; i++)
            {
                _powerButton.Press();
            }
            
            _display.Received(1).ShowPower(700);
        }

        [Test]
        public void StateReady_FifteenPowerPress_powerlevelReturn50()
        {
            for (int i = 0; i < 15; i++)
            {
                _powerButton.Press();
            }

            _display.Received(2).ShowPower(50);
        }
        #endregion

        #region TimeButton
        
        [Test]
        public void StateSetPower_TimePress_display1min()
        {
            _powerButton.Press(); //Set state SetPower

            _timeButton.Press();

            _display.Received(1).ShowTime(1,0);
        }

        [Test]
        public void StateSetPower_TimeTwoPress_display2min()
        {
            _powerButton.Press(); //Set state SetPower

            _timeButton.Press();
            _timeButton.Press();

            _display.Received(1).ShowTime(2, 0);
        }

        [Test]
        public void StateSetPower_TimeTwoPress_display10min()
        {
            _powerButton.Press(); //Set state SetPower

            for (int i = 0; i < 10; i++)
            {
                _timeButton.Press();
            }

            _display.Received(1).ShowTime(10, 0);
        }

        #endregion

        #region StartCancelPress
        [Test]
        public void StateSetPower_StartCancelPress_displayLightOff()
        {
            _powerButton.Press(); //Set state SetPower

            _startCancelButton.Press();

            _display.Received(1).Clear();
            _light.Received(1).TurnOff();
        }

        [Test]
        public void StateSetTime_StartCancelPress_LightCookerOn()
        {
            _powerButton.Press(); 
            _timeButton.Press(); //Set state SetTime

            _startCancelButton.Press();

            _light.Received(1).TurnOn();
            _cookController.Received(1).StartCooking(50, 1*60);
        }

        [Test]
        public void StateCooking_StartCancelPress_cookerLightDisplayOff()
        {
            _powerButton.Press();
            _timeButton.Press(); 
            _startCancelButton.Press(); //Set state SetCooking

            _startCancelButton.Press();

            _cookController.Received(1).Stop();
            _light.Received(1).TurnOff();
            _display.Received(1).Clear();
        }

        #endregion

    }
}
