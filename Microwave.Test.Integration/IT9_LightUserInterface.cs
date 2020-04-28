using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using System.IO;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Controllers;

namespace Microwave.Test.Integration
{

    [TestFixture]
    public class IT9_LightUserInterface
    {
        private Door door_;
        private Button powerButton_;
        private Button timeButton_;
        private Button startCancelButton_;
        private UserInterface userinterface_;
        private Light light_;
        private ICookController cookcontroller_;
        private IDisplay display_;
        private Output output_;
        private StringWriter stringWriter_;


        [SetUp]

        public void SetUp()
        {
            door_=new Door();
            powerButton_=new Button();
            timeButton_=new Button();
            startCancelButton_=new Button();
            output_=new Output();
            light_=new Light(output_);
            display_ = Substitute.For<IDisplay>();
            cookcontroller_ = Substitute.For<ICookController>();
            userinterface_=new UserInterface(powerButton_,timeButton_,startCancelButton_,door_,display_,light_,cookcontroller_);
            stringWriter_ = new StringWriter();
            Console.SetOut(stringWriter_);
        }


        [Test] 
        public void startCancelButton_Pressed_LightOn()
        {

            startCancelButton_.Press();
            Assert.That(stringWriter_.ToString().Contains("Light is turned off"));


        }


        [Test]
        public void timeButton_Pressed_()
        {



        }

        [Test]
        public void test3()
        {



        }

    }


}
