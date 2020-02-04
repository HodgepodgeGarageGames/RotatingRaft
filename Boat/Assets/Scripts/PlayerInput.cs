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

    protected enum Control {
        Up,
        Down,
        Left,
        Right,
        A,
        B,
        Select,
        Start
    }

    class GamepadPlusKeyboardInputReceiver : PlayerInputReceiver {

        private KeyMapping[]  keymap;

        public GamepadPlusKeyboardInputReceiver(int player, KeyMapping[] keymap) : base(player) {
            this.keymap = keymap;
        }

        override public bool up {
            get { return base.up || Input.GetKey(KeyFor(Control.Up)); }
        }
        override public bool down {
            get { return base.down || Input.GetKey(KeyFor(Control.Down)); }
        }
        override public bool left {
            get { return base.left || Input.GetKey(KeyFor(Control.Left)); }
        }
        override public bool right {
            get { return base.right || Input.GetKey(KeyFor(Control.Right)); }
        }
        override public bool a {
            get { return base.a || Input.GetKey(KeyFor(Control.A)); }
        }
        override public bool aDown {
            get { return base.aDown || Input.GetKeyDown(KeyFor(Control.A)); }
        }
        override public bool b {
            get { return base.a || Input.GetKey(KeyFor(Control.B)); }
        }
        override public bool bDown {
            get { return base.aDown || Input.GetKeyDown(KeyFor(Control.B)); }
        }
        override public bool start {
            get { return base.start || Input.GetKey(KeyFor(Control.Start)); }
        }
        override public bool startDown {
            get { return base.startDown || Input.GetKeyDown(KeyFor(Control.Start)); }
        }

        protected KeyCode KeyFor(Control control) {
            int i = 0;
            while (keymap[i] != null) {
                if (keymap[i].control == control) return keymap[i].key;
                ++i;
            }
            return KeyCode.None;
        }
    }

    class KeyMapping {
        public Control control;
        public KeyCode key;
        public KeyMapping(Control control, KeyCode key) {
            this.control = control;
            this.key = key;
        }
    }

    // Using slow-lookup array-of-arrays instead of a Dictionary,
    // because we REALLY want this mapping to be static, and because number
    // of elements is small.
    static KeyMapping[][] keymaps = new KeyMapping[][]{
        new KeyMapping[]{
            new KeyMapping(Control.Up, KeyCode.UpArrow),
            new KeyMapping(Control.Down, KeyCode.DownArrow),
            new KeyMapping(Control.Left, KeyCode.LeftArrow),
            new KeyMapping(Control.Right, KeyCode.RightArrow),
            new KeyMapping(Control.A, KeyCode.Period),
            new KeyMapping(Control.B, KeyCode.Comma),
            new KeyMapping(Control.Start, KeyCode.Return),
            null
        },
        new KeyMapping[]{
            new KeyMapping(Control.Up, KeyCode.W),
            new KeyMapping(Control.Down, KeyCode.S),
            new KeyMapping(Control.Left, KeyCode.A),
            new KeyMapping(Control.Right, KeyCode.D),
            new KeyMapping(Control.A, KeyCode.LeftControl),
            new KeyMapping(Control.B, KeyCode.LeftShift),
            new KeyMapping(Control.Start, KeyCode.Return),
            null
        }
    };

    public static PlayerInputReceiver   GetInputReceiver(int playerNum) {
        if (playerNum >= 0 && playerNum <2)
            return new GamepadPlusKeyboardInputReceiver(0, keymaps[playerNum]);
        else
            return new PlayerInputReceiver(playerNum);
    }
}