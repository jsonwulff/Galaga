
using NUnit.Framework;
using Galaga_Exercise_3;
using Galaga_Exercise_3.GalagaStates;

namespace Galaga_Testing {
    public class TestStateTransformer {

        [Test]
        public void TestStringToState1() {
            Assert.AreEqual(StateTransformer.TransformStringToState("GAME_RUNNING"),
                GameStateType.GameRunning);
        }

        [Test]
        public void TestStringToState2() {
            Assert.AreEqual(StateTransformer.TransformStringToState("MAIN_MENU"),
                GameStateType.MainMenu);
        }

        [Test]
        public void TestStringToState3() {
            Assert.AreEqual(StateTransformer.TransformStringToState("GAME_PAUSED"),
                GameStateType.GamePaused);
        }
        
        
        [Test]
        public void TestStateToString1() {
            Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.GameRunning),
                "GAME_RUNNING");
        }

        [Test]
        public void TestStateToString2() {
            Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.GamePaused),
                "GAME_PAUSED");
        }

        [Test]
        public void TestStateToString3() {
            Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.MainMenu),
                "MAIN_MENU");
        }
        
        
        
    }
}