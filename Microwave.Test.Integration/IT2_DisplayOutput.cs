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
        private Display display_;
        private Output output_;
        public StringWriter _stringWriter;
        //public StringReader _stringReader;


        [SetUp]

        public void Setup()
        {
            output_ = new Output();
            display_ =new Display(output_);
           
           


        }

        [TestCase("03", "20")]
        [TestCase("02", "40")]
        [TestCase("01", "15")]

        public void ShowTime_Output_DisplayShowsCorrectTime(string min, string sec)
        {

            using (_stringWriter= new StringWriter())
            {
                Console.SetOut(_stringWriter);

                display_.ShowTime(Convert.ToInt32(min), Convert.ToInt32(sec));
            }
            Assert.That(_stringWriter.ToString(),Is.EqualTo("Display shows: "+min+ ":" +sec+"\r\n"));

        }


        [TestCase(30)]
        [TestCase(20)]
        [TestCase(10)]
        public void ShowPower_Output_DisplayShowsCorrectPower(int power)
        {
            using (_stringWriter= new StringWriter())
            {
                Console.SetOut(_stringWriter);

                display_.ShowPower(power);
            }
            Assert.That(_stringWriter.ToString(), Is.EqualTo("Display shows: "+power+ " W\r\n"));

        }

        [Test]

        public void Clear_Output_DisplayShowsClear()
        {
            using (_stringWriter= new StringWriter())
            {
                Console.SetOut(_stringWriter);

                display_.Clear();
            }
            Assert.That(_stringWriter.ToString(), Is.EqualTo("Display cleared\r\n"));


        }








    }
}
