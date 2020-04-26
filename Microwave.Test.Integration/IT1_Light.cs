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

        [SetUp]
        public void Setup()
        {
           _output = new Output();
            _light = new Light(_output);
            
        }

        //[Test]
        //public void TurnOn_ConsoleWritesLightIsTurnedOn()
        //{
        //    _light.TurnOn();

            
        //}
    }
}
