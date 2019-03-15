using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga_Exercise_2.GalagaEnities.Enemy;

namespace Galaga_Exercise_2.Squadrons {
    public class Diagonal : ISquadron {
        private Game game;
        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; }
        public Diagonal(Game game) {
            this.game = game;
            MaxEnemies = 4;
            Enemies = new EntityContainer<Enemy>();
        }


        public void CreateEnemies(List<Image> enemyStrides) {
            for (var i = 0; i < MaxEnemies; i++) {
                Enemies.AddDynamicEntity(new Enemy(game,
                    new DynamicShape(new Vec2F(i * 0.1f + 0.3f, 0.90f - i * 0.1f), new Vec2F(0.1f, 0.1f)),
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