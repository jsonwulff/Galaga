
using NUnit.Framework;
using Galaga_Exercise_3;
using Galaga_Exercise_3.GalagaStates;

namespace Galaga_Testing {
    public class TestStateTransformer {

        [Test]
        public void TestStringToState1() {
            Assert.AreEqual(StateTransformer.TransformStringToState("GameRunning"),
                GameStateType.GameRunning);
        }

        [Test]
        public void TestStringToState2() {
            Assert.AreEqual(StateTransformer.TransformStringToState("MainMenu"),
                GameStateType.MainMenu);
        }

        [Test]
        public void TestStringToState3() {
            Assert.AreEqual(StateTransformer.TransformStringToState("GamePaused"),
                GameStateType.GamePaused);
        }
        
        
        [Test]
        public void TestStateToString1() {
            Assert.AreEqual(StateTransformer.TransformStringToState("GameRunning"),
                GameStateType.GameRunning);
        }

        [Test]
        public void TestStateToString2() {
            Assert.AreEqual(StateTransformer.TransformStringToState("MainMenu"),
                GameStateType.MainMenu);
        }

        [Test]
        public void TestStateToString3() {
            Assert.AreEqual(StateTransformer.TransformStringToState("GamePaused"),
                GameStateType.GamePaused);
        }
        
        
        
    }
}