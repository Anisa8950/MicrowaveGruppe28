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
        private Light _light; 
        private Output _output;
        public StringWriter _stringWriter;

        [SetUp]
        public void Setup()
        {
           _output = new Output();
            _light = new Light(_output);
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);

        }

        [Test]
        public void TurnOn_Output_LightIsTurnedOn()
        {
            _light.TurnOn();
            Assert.That(_stringWriter.ToString().Contains("on"));
        }


        [Test]
        public void TurnOn_TurnOff_Output_LightIsTurnedOff()
        {
            _light.TurnOn();
            _light.TurnOff();
            Assert.That(_stringWriter.ToString().Contains("off"));
        }
    }
}
