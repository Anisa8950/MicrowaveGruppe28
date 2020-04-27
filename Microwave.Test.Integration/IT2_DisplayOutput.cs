using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace Microwave.Test.Integration
{

    [TestFixture]
    public class IT2_DisplayOutput
    {
        private IDisplay display_;
        private IOutput output_;
        public StringWriter _stringWriter;
        public StringReader _stringReader;


        [SetUp]

        public void Setup()
        {

            display_=new Display(output_);
            output_=new Output();
            _stringWriter = new StringWriter();


        }

        [TestCase(3, 20)]
        [TestCase(2, 40)]
        [TestCase(1, 15)]

        public void ShowTime_Output_DisplayShowsCorrectTime(int min, int sec)
        {

            using (_stringReader)
            {
                Console.SetOut(_stringWriter);

                display_.ShowTime(min, sec);
            }
            Assert.That(_stringWriter.ToString(),Is.EqualTo("Display shows: "+min+ ":" +sec+ " \r\n"));

        }

        [TestCase(30)]
        [TestCase(20)]
        [TestCase(10)]
        public void ShowPower_Output_DisplayShowsCorrectPower(int power)
        {
            using (_stringReader)
            {
                Console.SetOut(_stringWriter);

                display_.ShowPower(power);
            }
            Assert.That(_stringWriter.ToString(), Is.EqualTo("Display shows: "+power+ " W\r\n"));

        }

        [Test]

        public void Clear_Output_DisplayShowsClear()
        {
            using (_stringReader)
            {
                Console.SetOut(_stringWriter);

                display_.Clear();
            }
            Assert.That(_stringWriter.ToString(), Is.EqualTo("Display cleared\r\n"));


        }








    }
}
