using DIKUArcade.Entities;
using DIKUArcade.Math;
using Galaga_Exercise_2.GalagaEnities.Enemy;

namespace Galaga_Exercise_2.MovementStrategy {
    public class NoMove : IMovementStrategy {
        public void MoveEnemy(Enemy enemy) {
            enemy.Direction(new Vec2F(0.0f,0.0f));
            enemy.Move();
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            enemies.Iterate(entity => MoveEnemy(entity));
        }
    }
}