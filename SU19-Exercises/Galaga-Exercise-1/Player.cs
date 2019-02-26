using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_1 {
    public class Player : Entity {
        private Game game;
        public Player(Game game, DynamicShape shape, IBaseImage image)
            : base(shape, image) {
            this.game = game;
        }

        public void Direction(Vec2F direction) {
            DynamicShape shape = Shape.AsDynamicShape();
            shape.ChangeDirection(direction);
        }

        public void Move() {
     
            if (Shape.Position.X >= 0.01f && Shape.Position.X <= 0.89f) {
                Shape.Move();
            }


        }
    }
}