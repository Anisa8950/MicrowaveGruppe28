using System;
using NUnit.Framework;
using NSubstitute;
using System.IO;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;


namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT3_PowertubeOutput
    {
        private IPowerTube _powertube;
        private IOutput _output;
        private StringWriter _stringWriter;

        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _powertube = new PowerTube(_output);
        }

        [TestCase(2)]
        [TestCase(50)]
        [TestCase(99)]
        public void TurnOn_Output_PowerTubeWorksWithPower(int power)
        {
            using (_stringWriter = new StringWriter())
            {
                Console.SetOut(_stringWriter);

                _powertube.TurnOn(power);
            }

            Assert.That(_stringWriter.ToString(), Is.EqualTo("PowerTube works with "+power+"\r\n"));
        }

        [Test]
        public void TurnOff_Output_Nothing()
        {
            using (_stringWriter = new StringWriter())
            {
                Console.SetOut(_stringWriter);

                _powertube.TurnOff();
            }

            Assert.That(_stringWriter.ToString(), Is.EqualTo(String.Empty));
        }

        [Test]
        public void TurnOff_Output_PowerTubeTurnedOff()
        {
            using (_stringWriter = new StringWriter())
            {
                Console.SetOut(_stringWriter);

                _powertube.TurnOn(50);
                _powertube.TurnOff();
            }
            
            Assert.That(_stringWriter.ToString, Is.EqualTo("PowerTube turned off\r\n"));
        }
    }    
}
