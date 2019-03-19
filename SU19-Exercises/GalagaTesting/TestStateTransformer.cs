using NUnit.Framework;
using Galaga_Exercise_3;
using Galaga_Exercise_3.GalagaStates;

namespace Galaga_Testing {
    public class TestStateTransformer {

        [Test]
        public void TestStringToState1() {
            Assert.AreEqual(StateTransformer.TransformStringToState("GameRunning"),GameStateType.GameRunning);
        }
    }
}