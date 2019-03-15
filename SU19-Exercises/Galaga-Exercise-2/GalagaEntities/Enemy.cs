using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_2.GalagaEnities.Enemy {
    public class Enemy : Entity {
        private Game game;
        public Vec2F startingPosition { get; }

        public Enemy(Game game, DynamicShape shape, IBaseImage image)
            : base(shape, image) {
            this.game = game;
            startingPosition = shape.Position;
        }
        
        private void Direction(Vec2F direction) {
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
        }
    }
}