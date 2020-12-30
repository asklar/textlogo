using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logo
{

    public interface ICommand
    {
        void Do(Turtle t);
    }

    public enum Direction
    {
        Forward,
        Backward,
        Left,
        Right
    }

    public class Move : ICommand
    {
        int N { get; set; }
        Direction Direction { get; set; }
        public Move(int n, Direction d)
        {
            N = n;
            Direction = d;
        }
        public void Do(Turtle t)
        {
            switch (Direction)
            {
                case Direction.Forward:
                    t.Forward(N); return;
                case Direction.Backward:
                    t.Backward(N); return;
                case Direction.Left:
                    t.Left(N); return;
                case Direction.Right:
                    t.Right(N); return;
            }
        }
    }

    public class Rotate : ICommand
    {
        float Angle { get; set; }
        public Rotate(int deg)
        {
            Angle = MathF.PI * deg / 180f;
        }
        public void Do(Turtle t)
        {
            t.RotateLeft(Angle);
        }
    }

    public class Repeat : ICommand
    {
        ICommand InnerCommand { get; set; }
        int Times { get; set; }
        public Repeat(int n, ICommand i)
        {
            Times = n;
            InnerCommand = i;
        }
        public void Do(Turtle t)
        {
            for (int i = 0; i < Times; i++)
            {
                InnerCommand.Do(t);
            }
        }
    }

    public class Sequence : ICommand
    {
        public List<ICommand> Commands { get; set; }
        public Sequence(List<ICommand> c) { Commands = c; }
        public void Do(Turtle t)
        {
            foreach (var c in Commands)
            {
                c.Do(t);
            }
        }
    }

    public class SetPen : ICommand
    {
        public Pen Pen { get; set; }
        public SetPen(Pen pen)
        {
            Pen = pen;
        }
        public void Do(Turtle t)
        {
            t.Pen = Pen;
        }
    }
}
