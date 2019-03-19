using Galaga_Exercise_3.GalagaStates;

namespace Galaga_Exercise_3 {
    public class StateTransformer {
        
        public static GameStateType TransformStringToState(string state) {
            return GameStateType.GameRunning;
        }

        public static string TransformStateToString() {
            return "GameRunning";
        }
    }
}