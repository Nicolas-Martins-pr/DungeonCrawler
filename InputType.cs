using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
     class InputType
    {
       private KeyboardState _currentKeyState; 
       private KeyboardState _previousKeyState;
        public InputType()
        {
            _currentKeyState = Keyboard.GetState();
        }
        
        public void Update()
        {
            _previousKeyState = _currentKeyState;
            _currentKeyState = Keyboard.GetState();
        }
        public bool GetKey(Keys key)
        {
            return _currentKeyState.IsKeyDown(key);
        }
        public bool GetKeyDown(Keys key)
        {
            return _currentKeyState.IsKeyDown(key) && !_previousKeyState.IsKeyDown(key);
        }

    }
}
