using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using Galaga_Exercise_3.GalagaEntities;
using Galaga_Exercise_3.MovementStrategy;
using Galaga_Exercise_3.Squadrons;

namespace Galaga_Exercise_3.GalagaStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        
        private List<Enemy> enemies;
        private List<Image> enemyStrides;
        private Row row;
        private ZigZagDown movementStrategy;

        private GameRunning() {
            InitializeGameState();
        }

        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }
        
        public static GameRunning NewInstance() {
            return GameRunning.instance = new GameRunning();
        }

        public void GameLoop() {
            throw new System.NotImplementedException();
        }

        public void InitializeGameState() {
            enemyStrides = ImageStride.CreateStrides(4,
                Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemies = new List<Enemy>();
            row = new Row();
            movementStrategy = new ZigZagDown();
            
            row.CreateEnemies(enemyStrides);

        }

        public void UpdateGameLogic() {
            GameRunning.GetInstance();
            movementStrategy.MoveEnemies(row.Enemies);
        }

        public void AddEnemies() {
            for (var i = 0; i < 8; i++) {
                var enemy = new Enemy(
                    new DynamicShape(new Vec2F(i * 0.1f + 0.1f, 0.90f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides));
                enemies.Add(enemy);
            }
        }
        
        public void RenderState() {
            row.Enemies.Iterate(entity => entity.RenderEntity());
        }
        
        public void KeyPress(string key) {
            switch (key) {
            case "KEY_ESCAPE":
                GalagaBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.GameStateEvent, this, "CHANGE_STATE", "GAME_PAUSED", ""));
                break;
            }
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            switch (keyAction) {
            case "KEY_PRESS":
                KeyPress(keyValue);
                break;
            case "KEY_RELEASE":
                break;
            }
        }
    }
}
