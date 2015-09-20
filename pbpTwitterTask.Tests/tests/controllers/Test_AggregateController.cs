using NUnit.Framework;



namespace katbyte.pbpTwitterTask.tests.controllers {


    [TestFixture, Category("controllers")]
    public class Test_AggregateController {

        [Test]
        public void Get() {

            //so... this requires Microsoft.AspNet.Mvc 6.0.0-beta6, and for the life of me i can't figure out how to resolve it
            //i tried using a xproj with NUnit 3.0 for tests, but then i couldn't figure out how to resolve my app dll...
            //so after many MANY hours i have come to the conclusion its beyond my skill to make this work and thus my controllers will go untested

            //var c = new AggregateController(null, null);
        }
    }
}