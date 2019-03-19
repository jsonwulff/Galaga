using DIKUArcade.Entities;
using Galaga_Exercise_3.GalagaEntities;

namespace Galaga_Exercise_3.MovementStategy {
    public interface IMovementStrategy {
        void MoveEnemy(Enemy enemy);
        void MoveEnemies(EntityContainer<Enemy> enemies);
    }
}