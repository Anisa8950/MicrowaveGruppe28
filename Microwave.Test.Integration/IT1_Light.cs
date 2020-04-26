using System;
using System.IO;
using MicrowaveOvenClasses.Boundary;
using NUnit.Framework;
using NSubstitute;


namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT1_Light
    {
        private Light _light; //skal man erklære dem med deres interfaces eller egetnlige klasser?
        private Output _output;
        public StringWriter _stringWriter;

        [SetUp]
        public void Setup()
        {
           _output = new Output();
            _light = new Light(_output);
            _stringWriter = new StringWriter();

        }

        [Test]
        public void TurnOn_Output_LightIsTurnedOn()
        {
            using (_stringWriter)
            {
                Console.SetOut(_stringWriter);
                _light.TurnOn();
            }
            Assert.That(_stringWriter.ToString, Is.EqualTo("Light is turned on\r\n"));
        }


        [Test]
        public void TurnOn_TurnOff_Output_LightIsTurnedOff()
        {
            _light.TurnOn();

            using (_stringWriter)
            {
                Console.SetOut(_stringWriter);
                _light.TurnOff();
            }
            Assert.That(_stringWriter.ToString, Is.EqualTo("Light is turned off\r\n"));
        }
    }
}
