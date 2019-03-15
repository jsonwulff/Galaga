using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_2 {
    public class Player : Entity {
        private Game game;

        public Player(Game game, DynamicShape shape, IBaseImage image)
            : base(shape, image) {
            this.game = game;
        }
        
        
        // <summary>
        /// Sets player direction as given direction.
        /// </summary>
        /// <param name="direction"></param>
        public void Direction(Vec2F direction) {
            var shape = Shape.AsDynamicShape();
            shape.ChangeDirection(direction);
        }

        /// <summary>
        /// Updates the movement of player object.
        /// </summary>
        public void Move() {
            Vec2F newPos = Shape.AsDynamicShape().Direction + Shape.Position;
            if (!(newPos.X < 0.0f ||
                  newPos.X + Shape.Extent.X > 1.0f ||
                  newPos.Y + Shape.Extent.Y < 0.0f ||
                  newPos.Y > 1.0f)) {
                Shape.Move();
            }
        }

        /// <summary>
        /// Instantiates playerShot at the players gun's position.
        /// </summary>
        public void Shot() {
            game.playerShots.Add(
                new PlayerShot(game,
                    new DynamicShape(
                        new Vec2F(
                            this.Shape.Position.X+this.Shape.Extent.X/2, 
                            this.Shape.Position.Y+this.Shape.Extent.Y),
                        new Vec2F(0.008f, 0.027f)),
                    game.playerShotImage));
        }
        
        /// <summary>
        /// KeyPress handles logic for a given key sent by ProcessEvent. 
        /// </summary>
        /// <param name="key"></param>
        public void KeyPress(string key) {
            switch (key) {
            case "KEY_RIGHT":
                Direction(new Vec2F(0.01f, 0.0f));
                break;
            case "KEY_LEFT":
                Direction(new Vec2F(-0.01f, 0.0f));
                break;
            case "KEY_SPACE":
                Shot();
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
                Direction(new Vec2F(0.00f, 0.0f));
                break;
            }
        }

/*       public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.InputEvent) {
                switch (gameEvent.Parameter1) {
                case "KEY_PRESS":
                    KeyPress(gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    KeyRelease(gameEvent.Message);
                    break;
                }
            }
       }*/
        
    }
}