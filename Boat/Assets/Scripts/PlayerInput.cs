using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public class PlayerInputReceiver {
        private SNES.GamePad    gamePad;
        private const float axisThreshold = 0.2f;
        public PlayerInputReceiver(int player) {
            gamePad = SNES.gamePad[player];
        }
        public virtual bool up {
            get { return gamePad.Vertical() > axisThreshold; }
        }
        public virtual bool down {
            get { return gamePad.Vertical() < -axisThreshold; }
        }
        public virtual bool left {
            get { return gamePad.Horizontal() < -axisThreshold; }
        }
        public virtual bool right {
            get { return gamePad.Horizontal() > axisThreshold; }
        }
        public virtual bool a {
            get { return gamePad.A(); }
        }
        public virtual bool aDown {
            get { return gamePad.ADown(); }
        }
        public virtual bool b {
            get { return gamePad.B(); }
        }
        public virtual bool bDown {
            get { return gamePad.BDown(); }
        }
        public virtual bool select {
            get { return gamePad.Select(); }
        }
        public virtual bool selectDown {
            get { return gamePad.SelectDown(); }
        }
        public virtual bool start {
            get { return gamePad.Start(); }
        }
        public virtual bool startDown {
            get { return gamePad.StartDown(); }
        }
    };

    class GamepadPlusKeyboardInputReceiver : PlayerInputReceiver {
        public GamepadPlusKeyboardInputReceiver() : base(0) {}
        override public bool up {
            get { return base.up || Input.GetKey(KeyCode.UpArrow); }
        }
        override public bool down {
            get { return base.down || Input.GetKey(KeyCode.DownArrow); }
        }
        override public bool left {
            get { return base.left || Input.GetKey(KeyCode.LeftArrow); }
        }
        override public bool right {
            get { return base.right || Input.GetKey(KeyCode.RightArrow); }
        }
        override public bool a {
            get { return base.a || Input.GetKey(KeyCode.Period); }
        }
        override public bool aDown {
            get { return base.aDown || Input.GetKeyDown(KeyCode.Period); }
        }
        override public bool b {
            get { return base.a || Input.GetKey(KeyCode.Comma); }
        }
        override public bool bDown {
            get { return base.aDown || Input.GetKeyDown(KeyCode.Comma); }
        }
        override public bool start {
            get { return base.start || Input.GetKey(KeyCode.Return); }
        }
        override public bool startDown {
            get { return base.startDown || Input.GetKeyDown(KeyCode.Return); }
        }
    }

    public static PlayerInputReceiver   GetInputReceiver(int playerNum) {
        if (playerNum == 0)
            return new GamepadPlusKeyboardInputReceiver();
        else
            return new PlayerInputReceiver(playerNum);
    }
}