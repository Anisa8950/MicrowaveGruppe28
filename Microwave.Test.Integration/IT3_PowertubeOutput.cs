using System;
using NUnit.Framework;
using System.IO;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;


namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT3_PowertubeOutput
    {
        private PowerTube _powertube;
        private Output _output;
        public StringWriter _stringWriter;

        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _powertube = new PowerTube(_output);
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [TestCase(50)]
        [TestCase(350)]
        [TestCase(700)]
        public void TurnOn_Output_PowerTubeWorksWithPower(int power)
        {
            _powertube.TurnOn(power);
            Assert.That(_stringWriter.ToString().Contains(""+power));
        }

        [Test]
        public void TurnOn_TurnOff_Output_PowerTubeTurnedOff()
        {
            _powertube.TurnOn(50);
            _powertube.TurnOff();
            Assert.That(_stringWriter.ToString().Contains("off"));
        }

        [Test]
        public void TurnOff_Output_Nothing()
        {
            _powertube.TurnOff();
            Assert.That(!_stringWriter.ToString().Contains("off"));
        }
    }    
}
