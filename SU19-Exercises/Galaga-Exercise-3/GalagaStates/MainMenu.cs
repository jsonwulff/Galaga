using System;
using System.Drawing;
using System.IO;
using System.Linq;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using Image = DIKUArcade.Graphics.Image;

namespace Galaga_Exercise_3.GalagaStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;

        private Entity backGroundImage;
        private Text[] menuButtons;
        private Text newGame;
        private Text quit;
        private int activeMenuButton;
        private int maxMenuButtons = 2;
        private Vec3I activeColor;
        private Vec3I inactiveColor;

        public MainMenu() {
            InitializeGameState();
        }
        
        public static MainMenu GetInstance() {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
        }

        public void GameLoop() {
            RenderState();
        }

        public void InitializeGameState() {
            activeMenuButton = 0;
            backGroundImage = new Entity(new StationaryShape(new Vec2F(0,0), new Vec2F(1,1) ), new Image(Path.Combine( "Assets",  "Images", "TitleImage.png")));
            menuButtons = new Text[maxMenuButtons];
            newGame = new Text("New Game", new Vec2F(0.5f, 0.5f), new Vec2F(0.2f, 0.2f));
            quit = new Text("Quit", new Vec2F(0.5f, 0.3f), new Vec2F(0.2f, 0.2f));
            menuButtons[0] = newGame;
            menuButtons[1] = quit;
            
            activeColor = new Vec3I(255,255,255);
            inactiveColor = new Vec3I(190,190,190);
            HandleButtons();

        }

        public void UpdateGameLogic() {
            MainMenu.GetInstance();
        }

        public void RenderState() {
            backGroundImage.RenderEntity();
            foreach (var button in menuButtons) {
                button.RenderText();
            }

        }

        public void HandleButtons() {
            foreach (var button in menuButtons) {
                button.SetColor(inactiveColor);                
            }
            menuButtons[activeMenuButton % maxMenuButtons].SetColor(activeColor);
            RenderState();
        }

        public void ActivateButton() {
            switch (activeMenuButton % maxMenuButtons) {
            case 0:
                GalagaBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.GameStateEvent, this, "CHANGE_STATE", "GAME_RUNNING", ""));
                break;
            case 1:
                GalagaBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
                break;
            }
        }
        
        public void KeyPress(string key) {
            Console.WriteLine("Keypres in main menu");
            switch (key) {
            case "KEY_UP":
                activeMenuButton += 1;
                HandleButtons();
                break;
            case "KEY_DOWN":
                activeMenuButton += 1;
                HandleButtons();
                break;
            case "KEY_ENTER":
                ActivateButton();
                HandleButtons();
                break;
            }
        }
        
        public void HandleKeyEvent(string keyValue, string keyAction) {
            Console.WriteLine("HandlekeyEvent");
            switch (keyAction) {
                case "KEY_PRESS":
                    KeyPress(keyValue);
                    break;
                case "KEY_RELEASE":
                    break;
            }
        }
    }
}