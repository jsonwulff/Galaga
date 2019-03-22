using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using Galaga_Exercise_3.GalagaEntities;
using Galaga_Exercise_3.MovementStrategy;
using Galaga_Exercise_3.Squadrons;

namespace Galaga_Exercise_3.GalagaStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        
        private Entity backGroundImage;

        private Player player;
     
        private List<Enemy> enemies;
        private List<Image> enemyStrides;
        private Row row;
        private ZigZagDown movementStrategy;
        
        private List<Image> explosionStrides;
        private AnimationContainer explosions;
        private int explosionLength = 500;
        
        private Score score;
        
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
           backGroundImage = new Entity(
                new StationaryShape(new Vec2F(0,0), new Vec2F(1,1) ), 
                new Image(Path.Combine( "Assets",  "Images", "SpaceBackground.png")));
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            enemyStrides = ImageStride.CreateStrides(4,
                Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemies = new List<Enemy>();
            row = new Row();
            movementStrategy = new ZigZagDown();
            
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
            explosions = new AnimationContainer(4);
            score = new Score(new Vec2F(0.01f, -0.25f), new Vec2F(0.3f, 0.3f));
            
            
            row.CreateEnemies(enemyStrides);
            
            

        }

        public void UpdateGameLogic() {
            player.Move();
            movementStrategy.MoveEnemies(row.Enemies);
            player.IterateShots();
        }
        
        public void RenderState() {
            backGroundImage.RenderEntity();
            player.entity.RenderEntity();
            row.Enemies.Iterate(entity => entity.RenderEntity());
            explosions.RenderAnimations();
            
            foreach (var shot in player.PlayerShots) {
                shot.RenderEntity();
                
                foreach (Enemy enemy in row.Enemies) {
                    var shotHit = CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape);
                    if (shotHit.Collision) {
                        AddExplosion(enemy.Shape.Position.X, enemy.Shape.Position.Y, 
                            enemy.Shape.Extent.X, enemy.Shape.Extent.Y);
                        shot.DeleteEntity();
                        enemy.DeleteEntity();
                        score.AddPoint(100);
                    }
                }
            }
            
            score.RenderScore();
        }
        
        
        public void AddExplosion(float posX, float posY,
            float extentX, float extentY) {
            explosions.AddAnimation(
                new StationaryShape(posX, posY, extentX, extentY), explosionLength,
                new ImageStride(explosionLength / 8, explosionStrides));
        }
        
        
        
        public void KeyPress(string key) {
            switch (key) {
            case "KEY_RIGHT":
                GalagaBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "MOVE_RIGHT", "", ""));
                break;
            case "KEY_LEFT":
                GalagaBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "MOVE_LEFT", "", ""));
                break;
            case "KEY_SPACE":
                GalagaBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "SHOOT", "", ""));
                break;
            case "KEY_ESCAPE":
                GalagaBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.GameStateEvent, this, "CHANGE_STATE", "GAME_PAUSED", ""));
                break;
            }
        }

        public void KeyRelease(string key) {
            switch (key) {
            case "KEY_RIGHT":
            case "KEY_LEFT":
                GalagaBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "MOVE_STOP", "", ""));
                break;
                
            }
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            switch (keyAction) {
            case "KEY_PRESS":
                KeyPress(keyValue);
                break;
            case "KEY_RELEASE":
                KeyRelease(keyValue);
                break;
            }
        }
    }
}
