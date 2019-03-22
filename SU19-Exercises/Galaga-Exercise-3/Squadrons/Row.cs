using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_3.GalagaEntities;

namespace Galaga_Exercise_3.Squadrons {
    public class Row : ISquadron {
        private Game game;
        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; }

        public Row() {
           
            MaxEnemies = 8;
            Enemies = new EntityContainer<Enemy>();
        }


        public void CreateEnemies(List<Image> enemyStrides) {
            for (var i = 0; i < MaxEnemies; i++) {
                Enemies.AddDynamicEntity(new Enemy(
                    new DynamicShape(new Vec2F(i * 0.1f + 0.1f, 0.90f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
            }
        }

        public void RenderEnemies() {
            foreach (Enemy enemy in Enemies) {
                enemy.RenderEntity();
            }
        }
    }
}