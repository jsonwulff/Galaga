using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using DIKUArcade.Timers;
using Galaga_Exercise_3.GalagaEntities;
using Galaga_Exercise_3.MovementStrategy;
using Galaga_Exercise_3.Squadrons;

namespace Galaga_Exercise_3.GalagaStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        private Game game;
       
        
        private Player player;
        
        public GameEventBus<object> eventBus;

        
        private List<Enemy> enemies;
        private List<Image> enemyStrides;
        private Row row;
        private ZigZagDown movementStrategy;

        public List<PlayerShot> playerShots { get; private set; }
        public Image playerShotImage { get; set; }

        private List<Image> explosionStrides;
        private AnimationContainer explosions;
        private int explosionLength = 500;
        

        private Score score;

        public GameRunning() {
            InitializeGameState();
        }
        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }
        
        public static GameRunning NewInstance() {
            GameRunning.instance = null;
            return GameRunning.instance = new GameRunning();
        }
        
        /// <summary>
        /// IterateShots handles the logic of the playerShots. It checks for collision with enemies and
        /// deletes both enemies and shots if needed.
        /// </summary>
        public void IterateShots() {
            foreach (var shot in playerShots) {
                shot.Shape.Move();
                if (shot.Shape.Position.Y > 1.0f) {
                    shot.DeleteEntity();
                }

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
            
            
            
            var newShot = new List<PlayerShot>();
            foreach (var shot in playerShots) {
                if (!shot.IsDeleted()) {
                    newShot.Add(shot);
                }
            }

            playerShots = newShot;
            
            row.Enemies.Iterate(entity => {
                entity.RenderEntity();
            });
        }

        public void AddExplosion(float posX, float posY,
            float extentX, float extentY) {
            explosions.AddAnimation(
                new StationaryShape(posX, posY, extentX, extentY), explosionLength,
                new ImageStride(explosionLength / 8, explosionStrides));
        }
        
        public void GameLoop() {
            //AddEnemies();
            row.CreateEnemies(enemyStrides);
            
            player.Move();
                    
            movementStrategy.MoveEnemies(row.Enemies);
                    
            IterateShots();
           
        }

        public void InitializeGameState() {
            player = new Player(game,
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));

            eventBus = GalagaBus.GetBus();
                        
            enemyStrides = ImageStride.CreateStrides(4,
                Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemies = new List<Enemy>();
            row = new Row();
            movementStrategy = new ZigZagDown();
            
            
            playerShots = new List<PlayerShot>();
            playerShotImage = new Image(
                Path.Combine("Assets", "Images", "BulletRed2.png"));
            
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
            explosions = new AnimationContainer(4);
            
            score = new Score(new Vec2F(0.01f, -0.25f), new Vec2F(0.3f, 0.3f));
        }

        public void UpdateGameLogic() {
            GameRunning.GetInstance();
        }

        public void RenderState() {
            player.entity.RenderEntity();
                    
            row.Enemies.Iterate(entity => entity.RenderEntity());
                    
            foreach (var shot in playerShots) {
                shot.RenderEntity();
            }

            explosions.RenderAnimations();
                    
            score.RenderScore();
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            switch (keyAction) {
            case "KEY_PRESS":
                player.KeyPress(keyValue);
                break;
            case "KEY_RELEASE":
                player.KeyRelease(keyValue);    
                break;
            }
            
        }
    }
}