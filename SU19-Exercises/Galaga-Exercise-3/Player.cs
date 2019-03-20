using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_3 {
    public class Player : IGameEventProcessor<object> {
        private Game game;

        public Entity entity { get; private set; }

        public Player(Game game, DynamicShape shape, IBaseImage image) {
            this.game = game;
            entity = new Entity(shape, image);
        }
        
        // <summary>
        /// Sets player direction as given direction.
        /// </summary>
        /// <param name="direction"></param>
        private void Direction(Vec2F direction) {
            var shape = entity.Shape.AsDynamicShape();
            shape.ChangeDirection(direction);
        }

        /// <summary>
        /// Updates the movement of player object.
        /// </summary>
        public void Move() {
            Vec2F newPos = entity.Shape.AsDynamicShape().Direction + entity.Shape.Position;
            if (!(newPos.X < 0.0f ||
                  newPos.X + entity.Shape.Extent.X > 1.0f ||
                  newPos.Y + entity.Shape.Extent.Y < 0.0f ||
                  newPos.Y > 1.0f)) {
                entity.Shape.Move();
            }
        }

        private void MoveLeft() {
            Direction(new Vec2F(-0.01f, 0.0f));
        }
        
        private void MoveRight() {
            Direction(new Vec2F(0.01f, 0.0f));
        }
        
        private void MoveStop() {
            Direction(new Vec2F(0.00f, 0.0f));
        }

        /// <summary>
        /// Instantiates playerShot at the players gun's position.
        /// </summary>
        public void Shoot() {
            game.playerShots.Add(
                new PlayerShot(game,
                    new DynamicShape(
                        new Vec2F(
                            entity.Shape.Position.X+entity.Shape.Extent.X/2, 
                            this.entity.Shape.Position.Y+entity.Shape.Extent.Y),
                        new Vec2F(0.008f, 0.027f)),
                    game.playerShotImage));
        }
        
        public void KeyPress(string key) {
            switch (key) {
            case "KEY_RIGHT":
               game.eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "MOVE_RIGHT", "", ""));
                break;
            case "KEY_LEFT":
                game.eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "MOVE_LEFT", "", ""));
                break;
            case "KEY_SPACE":
                game.eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "SHOOT", "", ""));
                break;
            
            }
        }
        
        public void KeyRelease(string key) {
            switch (key) {
            case "KEY_RIGHT":
            case "KEY_LEFT":
                game.eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "MOVE_STOP", "", ""));
                break;
            }
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType != GameEventType.PlayerEvent) {
                return;
            }
            switch (gameEvent.Message) {
                case "MOVE_LEFT":
                   MoveLeft();
                   break;
                case "MOVE_RIGHT":
                    MoveRight();
                    break;
                case "MOVE_STOP":
                    MoveStop();
                    break;
                case "SHOOT":
                    Shoot();
                    break;
            }
        }
    }
}