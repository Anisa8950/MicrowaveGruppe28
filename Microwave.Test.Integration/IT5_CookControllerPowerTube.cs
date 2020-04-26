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
    class IT5_CookControllerPowerTube
    {
        private IDisplay _display;
        private ITimer _timer;
        private IUserInterface _userInterface;
        private PowerTube _powertube;
        private Output _output;
        private CookController _cookController;
        public StringWriter _stringWriter;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
            _timer = Substitute.For<ITimer>();
            _userInterface = Substitute.For<IUserInterface>();

            _output = new Output();
            _powertube = new PowerTube(_output);
            _cookController = new CookController(_timer,_display,_powertube,_userInterface);
        }

        #region PowerTubeOn

        [Test]
        public void StartCooking_PowerTubeTurnOn_ThrowsNothing()
        {
            Assert.That(()=> _cookController.StartCooking(50,60), Throws.Nothing);
        }

        [Test]
        public void StartCooking_PowerTubeTurnOn_PowerTubeIsTurnedOn()
        {
            using (_stringWriter = new StringWriter())
            {
                Console.SetOut(_stringWriter);
                _cookController.StartCooking(50, 60);
            }
            
            Assert.That(_stringWriter.ToString(), Is.EqualTo("PowerTube works with 50\r\n"));
        }

        [Test]
        public void StartCooking_PowerTubeTurnOn_PowerOutOfRangeException()
        {
            Assert.That(() => _cookController.StartCooking(101,60), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void StartCooking_PowerTubeTurnOn_PowerTurnIsAlreadyTurnedOn()
        {
            _cookController.StartCooking(50,60);
            Assert.That(() => _cookController.StartCooking(50, 60), Throws.TypeOf<ApplicationException>());
        }
        #endregion


        #region PowerTubeOff
        [Test]
        public void OnTimerExpired_PowerTubeTurnOff_PowerTubeIsTurnedOff()
        {
            using (_stringWriter = new StringWriter())
            {
                Console.SetOut(_stringWriter);
                _cookController.StartCooking(50, 60);
                _timer.Expired += Raise.EventWith(this, EventArgs.Empty);
            }

            Assert.That(_stringWriter.ToString().Contains("off"));
        }

        [Test]
        public void StopCooking_PowerTubeTurnOff_PowerTubeIsTurnedOff()
        {
            using (_stringWriter = new StringWriter())
            {
                Console.SetOut(_stringWriter);
                _cookController.StartCooking(50, 60);
                _cookController.Stop();
            }

            Assert.That(_stringWriter.ToString().Contains("off"));
        }
        #endregion
    }
}
