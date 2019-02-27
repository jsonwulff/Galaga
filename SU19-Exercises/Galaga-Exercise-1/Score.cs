using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_1 {
    public class Score {
        private Text display;
        private int score;

        public Score(Vec2F postition, Vec2F extent) {
            score = 0;
            display = new Text(score.ToString(), postition, extent);
        }

        public void AddPoint() {
            score += 100;
        }

        public void RenderScore() {
            display.SetText(string.Format("Score: {0}", score.ToString()));
            display.SetColor(new Vec3I(255, 0, 0));
            display.RenderText();
        }
    }
}