using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Kinect;

namespace KiPong
{
    public class KeyboardInput : Input
    {
        private KeyboardState keyboardState;
        private KeyboardState lastState;
        public bool IsHoldable { get; set;}

        public KeyboardInput()
        {
            keyboardState = Keyboard.GetState();
            lastState = keyboardState;
        }

        public override void Update()
        {
            lastState = keyboardState;
            keyboardState = Keyboard.GetState();
        }

        private bool holdable(Keys k)
        {
            if (IsHoldable)
                return keyboardState.IsKeyDown(k);
            else
                return keyboardState.IsKeyDown(k) && lastState.IsKeyUp(k); 
        }

        public override bool Retour()
        {
            return holdable(Keys.Back) || holdable(Keys.Left);
        }

        public bool UpRight
        {
            get { return holdable(Keys.Up); }
        }

        public bool DownRight
        {
            get { return holdable(Keys.Down); }
        }

        public bool UpLeft
        {
            get { return holdable(Keys.Z); }
        }

        public bool DownLeft
        {
            get { return holdable(Keys.S); }
        }

        public override bool Valider()
        {
           return keyboardState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter);
        }

        public override bool Pause()
        {
            return keyboardState.IsKeyDown(Keys.Space) && lastState.IsKeyUp(Keys.Space);
        }

        public override bool Aide()
        {
            return keyboardState.IsKeyDown(Keys.F1) && lastState.IsKeyUp(Keys.F1);
        }

        public bool Exit
        {
            get { return keyboardState.IsKeyDown(Keys.Escape); }
        }
    }
}
