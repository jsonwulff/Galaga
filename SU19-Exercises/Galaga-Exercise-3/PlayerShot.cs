using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_3 {
    public class PlayerShot : Entity {

        public PlayerShot(DynamicShape shape, IBaseImage image)
            : base(shape, image) {
            shape.ChangeDirection(new Vec2F(0.0f, 0.01f));
        }
    }
}