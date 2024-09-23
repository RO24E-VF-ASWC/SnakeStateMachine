using System;
using System.Collections.Generic;
using System.Text;
using SnakeLib.state;

namespace SnakeLib.snake
{
    public class Snake
    {
        public Posistion Head { get; set; }
        public Queue<Posistion> Body { get; set; }

        
        public Snake(int headRow, int headCol)
        {
            Head = new Posistion(headRow,headCol);
            Body = new Queue<Posistion>();
        }

        public void Move(Move direction)
        {
            Body.Enqueue(new Posistion(Head.Row,Head.Col));
            
            SetHeadNewPosition(direction);
        }

        private void SetHeadNewPosition(Move move)
        {
            Head.Row += move.row;
            Head.Col += move.col;

        }

        public bool CheckInside(in int maxWidth, in int maxHeight)
        {
            return !(Head.Col == maxHeight || Head.Col == -1 ||
                    Head.Row == maxWidth || Head.Row == -1);

        }

        public bool EatItSelf()
        {
            foreach (Posistion pos in Body)
            {
                if (Head.Equals(pos))
                {
                    return true;
                }
            }

            return false;
        }

        public bool EatFood(Posistion food)
        {
            bool isFood = Head.Equals(food);
            if (!isFood)
            {
                Body.Dequeue();
            }
            return isFood;
        }
    }
}
