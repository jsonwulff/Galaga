using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_1 {
    public class Player : Entity {
        private Game game;
        private Image shotImage;

        public Player(Game game, DynamicShape shape, IBaseImage image)
            : base(shape, image) {
            this.game = game;
            shotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
        }

        public void Direction(Vec2F direction) {
            var shape = Shape.AsDynamicShape();
            shape.ChangeDirection(direction);
        }

        public void Move() {
            Vec2F newPos = Shape.AsDynamicShape().Direction + Shape.Position;
            if (!(newPos.X < 0.0f ||
                  newPos.X + Shape.Extent.X > 1.0f ||
                  newPos.Y + Shape.Extent.Y < 0.0f ||
                  newPos.Y > 1.0f)) {
                Shape.Move();
            }
            
//            Shape.Move();
//
//            if (Shape.Position.X < 0.01f) {
//                Shape.SetPosition(new Vec2F(0.01f, 0.1f));
//            }
//
//            if (Shape.Position.X > 0.90f) {
//                Shape.SetPosition(new Vec2F(0.9f, 0.1f));
//            }
        }

        public void Shot() {
            var shot = new PlayerShot(game,
                new DynamicShape(
                    new Vec2F(
                        Shape.Position.X + 0.046f, 
                        Shape.Position.Y + 0.073f),
                    new Vec2F(0.008f, 0.027f)),
                shotImage);
            game.playerShots.Add(shot);
        }
    }
}