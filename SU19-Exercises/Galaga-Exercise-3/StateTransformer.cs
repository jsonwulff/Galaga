using Galaga_Exercise_3.GalagaStates;

namespace Galaga_Exercise_3 {
    public class StateTransformer {
        
        public static GameStateType TransformStringToState(string state) {
            switch (state) {
            case "GameRunning":
                return GameStateType.GameRunning;
            case "GamePaused":
                return GameStateType.GamePaused;
            case "MainMenu":
                return GameStateType.MainMenu;
            }

            return GameStateType.MainMenu;
        }

        public static string TransformStateToString(GameStateType state) {
            return state.ToString();
        }
    }
}