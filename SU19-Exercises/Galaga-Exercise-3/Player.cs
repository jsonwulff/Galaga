using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_3.GalagaStates;

namespace Galaga_Exercise_3 {
    public class Player : IGameEventProcessor<object> {

        public Entity entity { get; private set; }
        public List<PlayerShot> PlayerShots { get; private set; }
        public Image PlayerShotImage;

        public Player(DynamicShape shape, IBaseImage image) {
            //this.game = game;
            GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);
            entity = new Entity(shape, image);
            PlayerShots = new List<PlayerShot>();
            PlayerShotImage = new Image(
                Path.Combine("Assets", "Images", "BulletRed2.png"));
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
            PlayerShots.Add(
                new PlayerShot(
                    new DynamicShape(new Vec2F(
                            entity.Shape.Position.X+entity.Shape.Extent.X/2, 
                            this.entity.Shape.Position.Y+entity.Shape.Extent.Y),
                        new Vec2F(0.008f, 0.027f)), PlayerShotImage
                    ));
        }

        public void IterateShots() {
            foreach (PlayerShot shot in PlayerShots) {
                shot.Shape.Move();
                if (shot.Shape.Position.Y > 1.0f) {
                    shot.DeleteEntity();
                }
            }
            var newShot = new List<PlayerShot>();
            foreach (var shot in PlayerShots) {
                if (!shot.IsDeleted()) {
                    newShot.Add(shot);
                }
            }

            PlayerShots = newShot;
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