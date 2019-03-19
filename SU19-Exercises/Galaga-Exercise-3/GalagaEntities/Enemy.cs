using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_3.GalagaEntities {
    public class Enemy : Entity {
        private Game game;
        public Vec2F startingPosition { get; }

        public Enemy(Game game, DynamicShape shape, IBaseImage image)
            : base(shape, image) {
            this.game = game;
            startingPosition = shape.Position;
        }
        
        public void Direction(Vec2F direction) {
            var shape = Shape.AsDynamicShape();
            shape.ChangeDirection(direction);
        }
        
        public void Move() {
            Shape.Move();
        }
    }
}