using System;
using System.Collections.Generic;
using System.Text;
using SnakeLib.state;

namespace SnakeLib.snake
{
    public class SnakePlayground
    {
        // playgound
        private char[,] _playground;
        private readonly int maxWidth;
        private readonly int maxHeight;
        private readonly String horizontalLine = "";


        // snake 
        private Snake _snake;
        public bool IsLonger { get; private set; }

        // snake food
        private Posistion _food;

        // Points
        private int _point = 0;

        public int Point => _point;

        // random
        private static Random rnd = new Random(DateTime.Now.Millisecond);

        public SnakePlayground(int width, int height)
        {
            _playground = new char[width,height];
            maxWidth = width;
            maxHeight = height;
            for (int i = 0; i < width+2; i++)
            {
                horizontalLine += "-";
            }

            _snake = new Snake(width / 2, height / 2);
            _food = new Posistion(rnd.Next(width), rnd.Next(height));
        }

        public bool DoNextMove(Move move)
        {
            IsLonger= false;
           _snake.Move(move);

            bool inside = _snake.CheckInside(maxWidth, maxHeight);

            bool eatItSelf = _snake.EatItSelf();

            if (_snake.EatFood(_food))
            {
                _point++;
                _food.Row = rnd.Next(maxWidth);
                _food.Col = rnd.Next(maxHeight);
                IsLonger= true;
            }

            return inside && !eatItSelf;
        }

        public void PrintPlayground()
        {
            Console.Clear();
            Console.WriteLine("Snake ");
            Console.WriteLine(horizontalLine);
            for (int r = 0; r < maxHeight; r++)
            {
                Console.Write("|");
                PrintRowString(r);
                Console.WriteLine($"|");
            }
            Console.WriteLine(horizontalLine);
        }
        private void PrintRowString(int r)
        {
            StringBuilder sb = new StringBuilder();
            for (int c = 0; c < maxWidth; c++)
            {
                PrintColRowChar(r, c);
            }
        }

        private void PrintColRowChar(int row, int col)
        {
            Posistion p = new Posistion(row,col);

            if (_snake.Head.Equals(p))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write('@');
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (_snake.Body.Contains(p))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write('x');
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (_food.Equals(p))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write('o');
                Console.ForegroundColor = ConsoleColor.White;
            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(' ');
            }

        }
    }
}
