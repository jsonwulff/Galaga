using System.Collections.Generic;
using DIKUArcade.EventBus;
using Galaga_Exercise_3;
using Galaga_Exercise_3.GalagaStates;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Galaga_Testing {
    [TestFixture]
    public class StateMachineTesting{
        private StateMachine stateMachine;
        
        [SetUp]
        public void InitiateStateMachine() {
            DIKUArcade.Window.CreateOpenGLContext();
            // Here you should:
            // (1) Initialize a GalagaBus with proper GameEventTypes
            GalagaBus.GetBus().InitializeEventBus(new List<GameEventType> {
                GameEventType.GameStateEvent
            });
            // (2) Instantiate the StateMachine
            stateMachine = new StateMachine();
            // (3) Subscribe the GalagaBus to proper GameEventTypes    
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
            //     and GameEventProcessors

        }
        
        [Test]
        public void TestInitialState() {
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
        }
        [Test]
        public void TestEventGamePaused() {
            GalagaBus.GetBus().RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.GameStateEvent,
                    this,
                    "CHANGE_STATE",
                    "GAME_PAUSED", ""));
            GalagaBus.GetBus().ProcessEventsSequentially();
            Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
        }

        
    }
    
}