using System;

namespace Tree
{
    public sealed class FormatInfo
    {
        public FormatInfo(ShowType type)
        {
            InitPos(type);
            ValueStart = ' ';
            ValueEnd = ' ';
            RegionStart = '(';
            RegionEnd = ')';
        }

        public int RootPos { get; set; }

        public int LeftChildPos { get; set; }

        public int RightChildPos { get; set; }

        public char ValueStart { get; set; }

        public char ValueEnd { get; set; }

        public char RegionStart { get; set; }

        public char RegionEnd { get; set; }

        private void InitPos(ShowType type)
        {
            if (type == ShowType.Infix)
            {
                RootPos = 1;
                LeftChildPos = 0;
                RightChildPos = 2;
            }
            else if (type == ShowType.Prefix)
            {
                RootPos = 0;
                LeftChildPos = 1;
                RightChildPos = 2;
            }
            else if (type == ShowType.Postfix)
            {
                RootPos = 1;
                LeftChildPos = 2;
                RightChildPos = 0;
            }
        }
    }
}
