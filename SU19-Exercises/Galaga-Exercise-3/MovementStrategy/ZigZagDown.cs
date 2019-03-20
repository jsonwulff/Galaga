using System;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Galaga_Exercise_3.GalagaEntities;

namespace Galaga_Exercise_3.MovementStrategy {
    public class ZigZagDown : IMovementStrategy {
        private float speed = -0.0003f;
        private float p = 0.045f;
        private float a = 0.05f;
        
        private float yi(float y) {
            return y + speed;
        }

        private float xi(float x0, float y0, float yi) {
            return x0 + a * (float) Math.Sin((2 * Math.PI * (y0 - yi)) / (p));
        }
        
        public void MoveEnemy(Enemy enemy) {
            enemy.Shape.SetPosition(new Vec2F(
                xi(enemy.startingPosition.X,enemy.startingPosition.Y,enemy.Shape.Position.Y),
                yi(enemy.Shape.Position.Y)));
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            enemies.Iterate(entity => MoveEnemy(entity));
        }
        
    }
}