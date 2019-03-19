namespace Galaga_Exercise_3.GalagaStates {
    public enum GameStateType {
        GameRunning,
        GamePaused,
        MainMenu
    }
    
    public class StateTransformer {
        
        public static GameStateType TransformStringToState(string state) {
            return GameStateType.GameRunning;
        }

        public static string TransformStateToString() {
            return "GameRunning";
        }
    }
}