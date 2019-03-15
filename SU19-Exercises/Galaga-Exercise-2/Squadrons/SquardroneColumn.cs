using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Galaga_Exercise_2.GalagaEnities.Enemy;

namespace Galaga_Exercise_2.Squadrons {
    public class SquardroneColumn : ISquadron {
        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; }
        public void CreateEnemies(List<Image> enemyStrides) {
            throw new System.NotImplementedException();
        }
    }
}