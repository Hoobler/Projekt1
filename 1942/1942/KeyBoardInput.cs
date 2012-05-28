using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace _1942
{
    static class KeyBoardInput
    {
        private static KeyboardState keyState;
        private static KeyboardState oldKeyState;

        private static string textInput = String.Empty;

        

        /// <summary>
        /// Call this method when you want basic text input.
        /// </summary>
        /// <param name="LengtOfText">How long you want the maximum lenght to be</param>
        /// <param name="fullKeyBoardInput">Set to true if you want to input more then one char at a time</param>
        /// <returns></returns>
        public static string TextInput(int LengtOfText, bool fullKeyBoardInput)
        {
            int textLengt = LengtOfText;
            int inputLenght = 0; 
            oldKeyState = keyState;
            keyState = Keyboard.GetState();

            if (fullKeyBoardInput)
            {
                inputLenght = 20;
            }
            else
            {
                inputLenght = 2;
            }

            foreach (Keys key in keyState.GetPressedKeys())
            {
                if (oldKeyState.IsKeyUp(key))
                {
                    if (key == Keys.Back && textInput.Length > 0)
                    {
                        textInput = textInput.Remove(textInput.Length - 1, 1);
                    }

                    else if (key == Keys.Enter)
                    {
                        break;
                    }
                    else
                    {
                        if (textInput.Length >= textLengt)
                        { break; }
                        else
                        {
                            if(key.ToString().Length >= inputLenght)
                            { break; }
                            else
                            {
                                textInput += key.ToString();
                            }
                        }
                    }
                }
            }
            return textInput;
        }

        public static string EmptyWord
        {
            set { textInput = value; }
        }

        public static KeyboardState KeyState()
        {
            keyState = Keyboard.GetState();
            return keyState;
        }

        public static KeyboardState OldKeyState()
        {
            oldKeyState = keyState;
            keyState = Keyboard.GetState();
            return oldKeyState;
        }
    }
}
