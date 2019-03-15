using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_2 {
    public class Score {
        private Text display;
        private int score;

        public Score(Vec2F postition, Vec2F extent) {
            score = 0;
            display = new Text(score.ToString(), postition, extent);
        }

        public void AddPoint(int points) {
            score += points;
        }
        /// <summary>
        /// Renders the text in a green color.
        /// </summary>
        public void RenderScore() {
            display.SetText(string.Format("Score: {0}", score.ToString()));
            display.SetColor(new Vec3I(60, 210, 60));
            display.RenderText();
        }
    }
}