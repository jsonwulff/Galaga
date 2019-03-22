using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Galaga_Exercise_3.GalagaStates {
    public class GamePaused : IGameState {
        
        private static GamePaused instance = null;
        
        private Text[] pauseButtons;
        private Text buttonOne;
        private Vec3I activeColor;
        private Vec3I inactiveColor;

        public static GamePaused GetInstance() {
            return GamePaused.instance ?? (GamePaused.instance = new GamePaused());
        }

        public GamePaused() {
            InitializeGameState();
        }

        public void GameLoop() {
            throw new System.NotImplementedException();
        }

        public void InitializeGameState() {
            buttonOne = new Text("New Game", new Vec2F(0.5f, 0.5f), new Vec2F(0.2f, 0.2f));
            activeColor = new Vec3I(255,255,255);
            inactiveColor = new Vec3I(190,190,190);
            
            buttonOne.SetColor(activeColor);
        }

        public void UpdateGameLogic() {
            GamePaused.GetInstance();
        }

        public void RenderState() {
            
            buttonOne.RenderText();
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            throw new System.NotImplementedException();
        }
    }
}