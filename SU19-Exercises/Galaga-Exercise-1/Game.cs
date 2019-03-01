using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Timers;

namespace Galaga_Exercise_1 {
    public class Game : IGameEventProcessor<object> {
        private List<Enemy> enemies;
        private List<Image> enemyStrides;
        private GameEventBus<object> eventBus;

        private int explosionLength = 500;
        private AnimationContainer explosions;
        private List<Image> explosionStrides;
        private GameTimer gameTimer;
        private Player player;
        private Score score;
        private Window win;

        /// <summary>
        /// Constructor for Game class
        /// </summary>
        public Game() {
            // For the window, we recommend a 500x500 resolution (a 1:1 aspect ratio).
            win = new Window("Galaga", 500, 500);
            gameTimer = new GameTimer(60, 30);
            player = new Player(this,
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));
            enemyStrides = ImageStride.CreateStrides(4,
                Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemies = new List<Enemy>();
            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType> {
                GameEventType.InputEvent, // key press / key release
                GameEventType.WindowEvent // messages to the window
            });
            playerShots = new List<PlayerShot>();
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
            explosions = new AnimationContainer(4);
            score = new Score(new Vec2F(0.01f, -0.25f), new Vec2F(0.3f, 0.3f));
            win.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this);
        }

        public List<PlayerShot> playerShots { get; private set; }

        /// <summary>
        /// ProcessEvent is the handler for keypresses on during runtime. It broadcasts GameEvents.
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="gameEvent"></param>
        public void ProcessEvent(GameEventType eventType,
            GameEvent<object> gameEvent) {
            if (eventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                case "CLOSE_WINDOW":
                    win.CloseWindow();
                    break;
                }
            } else if (eventType == GameEventType.InputEvent) {
                switch (gameEvent.Parameter1) {
                case "KEY_PRESS":
                    KeyPress(gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    KeyRelease(gameEvent.Message);
                    break;
                }
            }
        }

        /// <summary>
        /// AddEnemies instantiates the enemies that are collected in the enemies List.
        /// </summary>
        public void AddEnemies() {
            for (var i = 0; i < 8; i++) {
                var enemy = new Enemy(this,
                    new DynamicShape(new Vec2F(i * 0.1f + 0.1f, 0.90f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides));
                enemies.Add(enemy);
            }
        }
        /// <summary>
        /// Instantiates the explosion animation at the given position.
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="extentX"></param>
        /// <param name="extentY"></param>
        public void AddExplosion(float posX, float posY,
            float extentX, float extentY) {
            explosions.AddAnimation(
                new StationaryShape(posX, posY, extentX, extentY), explosionLength,
                new ImageStride(explosionLength / 8, explosionStrides));
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

                foreach (var enemy in enemies) {
                    var shotHit = CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape);
                    if (shotHit.Collision) {
                        AddExplosion(enemy.Shape.Position.X, enemy.Shape.Position.Y, 0.1f, 0.1f);
                        explosions.RenderAnimations();
                        shot.DeleteEntity();
                        enemy.DeleteEntity();
                        score.AddPoint();
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
            
            var newEnemies = new List<Enemy>();
            foreach (var enemy in enemies) {
                if (!enemy.IsDeleted()) {
                    newEnemies.Add(enemy);
                }
            }

            enemies = newEnemies;
        }

        /// <summary>
        /// GameLoop utilizes the GameTimer class to ensure that the game runs at a steady speed on all systems.
        /// We update both rendering and game logic in the loop. The speed of the updates are specified in the gameTimer object.
        /// </summary>
        public void GameLoop() {
            AddEnemies();
            while (win.IsRunning()) {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate()) {
                    win.PollEvents();
                    // Update game logic here
                    player.Move();
                    IterateShots();
                    eventBus.ProcessEvents();
                }

                if (gameTimer.ShouldRender()) {
                    win.Clear();
                    // Render gameplay entities here
                    foreach (var enemy in enemies) {
                        enemy.RenderEntity();
                    }

                    foreach (var shot in playerShots) {
                        shot.RenderEntity();
                    }

                    explosions.RenderAnimations();
                    player.RenderEntity();
                    score.RenderScore();
                    win.SwapBuffers();
                }
            }
        }

        /// <summary>
        /// KeyPress handles logic for a given key sent by ProcessEvent. 
        /// </summary>
        /// <param name="key"></param>
        public void KeyPress(string key) {
            switch (key) {
            case "KEY_ESCAPE":
                eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
                break;
            case "KEY_RIGHT":
                player.Direction(new Vec2F(0.01f, 0.0f));
                break;
            case "KEY_LEFT":
                player.Direction(new Vec2F(-0.01f, 0.0f));
                break;
            case "KEY_SPACE":
                player.Shot();
                break;
            }
        }
        
        /// <summary>
        /// KeyRelease handles logic when a key sent by ProcessEvent is released.
        /// </summary>
        /// <param name="key"></param>
        public void KeyRelease(string key) {
            switch (key) {
            case "KEY_RIGHT":
            case "KEY_LEFT":
            case "KEY_UP":
            case "KEY_DOWN":
                player.Direction(new Vec2F(0.00f, 0.0f));
                break;
            }
        }
    }
}