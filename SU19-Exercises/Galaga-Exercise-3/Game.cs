using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.EventBus;
using DIKUArcade.Timers;


namespace Galaga_Exercise_3 {
    public class Game : IGameEventProcessor<object> {
        public Window win;
        private GameTimer gameTimer;

        private StateMachine stateMachine;
        
        public GameEventBus<object> eventBus;
        
        /// <summary>
        /// Constructor for Game class
        /// </summary>
        public Game() {
            win = new Window("Galaga", 500, 500);
            gameTimer = new GameTimer(60, 60);
            
            
            eventBus = GalagaBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType> {
                GameEventType.InputEvent, // key press / key release
                GameEventType.WindowEvent, // messages to the window
                GameEventType.PlayerEvent,
                GameEventType.GameStateEvent
            });
            win.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.WindowEvent, this);
            stateMachine = new StateMachine();
        }
      
        /// <summary>
        /// GameLoop utilizes the GameTimer class to ensure that the game runs at a steady speed on all systems.
        /// We update both rendering and game logic in the loop. The speed of the updates are specified in the gameTimer object.
        /// </summary>
        public void GameLoop() {
            
            while (win.IsRunning()) {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate()) {
                    win.PollEvents();
                    eventBus.ProcessEvents();
                    stateMachine.ActiveState.UpdateGameLogic();
                }

                if (gameTimer.ShouldRender()) {
                    win.Clear();
                    stateMachine.ActiveState.RenderState();                 
                    win.SwapBuffers();
                }
                
                if (gameTimer.ShouldReset()) {
                    // 1 second has passed - display last captured ups and fps
                    win.Title = "Galaga | UPS: " + gameTimer.CapturedUpdates +
                                ", FPS: " + gameTimer.CapturedFrames;
                }
            }
        }

        /// <summary>
        /// ProcessEvent is the handler for keypresses on during runtime. It broadcasts GameEvents.
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="gameEvent"></param>
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                case "CLOSE_WINDOW":
                    win.CloseWindow();
                    break;
                }
            }
        }
    }
}
