using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello_Game_CSharp
{
    public class Move
    {
        private int m_Row;
        private int m_Column;

        public Move(int i_Row, int i_Column)
        {
            this.Row = i_Row;
            this.Column = i_Column;
        }

        public int Row
        {
            get { return this.m_Row; }
            set { this.m_Row = value; }
        }

        public int Column
        {
            get { return this.m_Column; }
            set { this.m_Column = value; }
        }
    }
}
