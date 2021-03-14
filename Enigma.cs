using System;
using System.Collections;
using System.Collections.Generic;

namespace Enigma
{
    class Letter
    {
        enum OneLetter { a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z, 
                         A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z };
        private OneLetter thisLetter;
        public Letter(char let)
        {
            Set(let);
        }
        public char ToChar()
        {
            if((int)thisLetter > 25) return (char)((int)thisLetter + 39);
            else return (char)((int)thisLetter + 97);

        }
        public void Set(char let)
        {
            if (let > 64 && let < 91) thisLetter = (OneLetter)((int)let - 39);
            else if (let > 96 && let < 123) thisLetter = (OneLetter)((int)let - 97);
            else throw new ArgumentOutOfRangeException();
        }
        public void Increase(Letter amount)
        {
            for (int i = 0; i <= (int)amount.thisLetter; i++)
            {
                if (thisLetter != OneLetter.Z) thisLetter++;
                else thisLetter = OneLetter.a;
            }
        }
        public void Decrease()
        {
            if (thisLetter != OneLetter.a) thisLetter--;
            else thisLetter = OneLetter.Z;
        }
        public void Decrease(Letter amount)
        {
            for (int i = 0; i <= (int)amount.thisLetter; i++)
            {
                if (thisLetter != OneLetter.a) thisLetter--;
                else thisLetter = OneLetter.Z;
            }
        }
        public void Increase()
        {
            if (thisLetter != OneLetter.Z) thisLetter++;
            else thisLetter = OneLetter.a;
        }
    }
    class Enigma
    {
        private List<Letter> message = new List<Letter>();
        private Letter firstWheel = new Letter('a');
        private Letter secWheel = new Letter('a');
        private Letter thirdWheel = new Letter('a');
        public Enigma(char first, char second, char third)
        {
            SetWheels(first, second, third);
        }
        public void SetMessage(string newMessage)
        {
            if (message != null) message.Clear();
            for (int i = 0; i < newMessage.Length; i++)
            {
                Letter temp = new Letter(newMessage[i]);
                message.Add(temp);
            }
        }
        public void SetWheels(char first, char second, char third)
        {
            firstWheel.Set(first);
            secWheel.Set(second);
            thirdWheel.Set(third);
        }
        public char[] GetMessage()
        {
            char[] temp = new char[message.Count];
            for (int i = 0; i < message.Count; i++)
            {
                temp[i] = message[i].ToChar();
            }
            return temp;
        }
        public void IncreaseWheels()
        {
            thirdWheel.Increase();
            if (thirdWheel.ToChar() == 'a')
            {
                secWheel.Increase();
                if (secWheel.ToChar() == 'a')
                {
                    firstWheel.Increase();
                }
            }
        }
        public void DecreaseWheels()
        {
            thirdWheel.Decrease();
            if (thirdWheel.ToChar() == 'z')
            {
                secWheel.Decrease();
                if (secWheel.ToChar() == 'z')
                {
                    firstWheel.Decrease();
                }
            }
        }
        public void Cipher()
        {
            foreach (var item in message)
            {
                item.Increase(thirdWheel);
                item.Increase(secWheel);
                item.Increase(firstWheel);
                IncreaseWheels();
            }
        }
        public void Cipher(char a, char b, char c)
        {
            SetWheels(a, b, c);
            foreach (var item in message)
            {
                item.Increase(thirdWheel);
                item.Increase(secWheel);
                item.Increase(firstWheel);
                IncreaseWheels();
            }
        }
        public void Decipher(char a, char b, char c)
        {
            SetWheels(a, b, c);
            foreach (var item in message)
            {
                item.Decrease(thirdWheel);
                item.Decrease(secWheel);
                item.Decrease(firstWheel);
                IncreaseWheels();
            }
        }
    }
}