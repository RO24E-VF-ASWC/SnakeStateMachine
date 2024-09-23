using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeLib.snake
{
    public class Posistion
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Posistion():this(0,0)
        {
        }

        public Posistion(int row, int col)
        {
            Row = row;
            Col = col;
        }

        /*
         * For checking if two prositions have the same values
         */
        protected bool Equals(Posistion other)
        {
            return Row == other.Row && Col == other.Col;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Posistion) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Row * 397) ^ Col;
            }
        }
    }
}
