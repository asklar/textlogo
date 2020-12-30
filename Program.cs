using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace logo
{
    public enum Pen
    {
        Down,
        Up,
        Erase
    }
    public class Turtle
    {
        public float Direction {get;set;}
        public float X {get;set;}
        public float Y {get;set;}
        private char[,] buffer;
        public Pen Pen { get; set; } = Pen.Down;
        public Turtle(int width, int height)
        {
            buffer = new char[width, height];
            /* Without the next block, Terminal does not print the output correctly (skips instead of printing spaces) */

            for (var y = 0; y < buffer.GetLength(1); y++) 
            {
                for (var x = 0; x < buffer.GetLength(0); x++) {
                    buffer[x, y] = ' ';
                }
            }
            X = width / 2;
            Y = height / 2;
            Direction = MathF.PI / 2;
        }

        public void PrintBuffer()
        {
            Console.Write(new string('=', buffer.GetLength(0) + 2) + '\n');
            for (var y = 0; y < buffer.GetLength(1); y++) 
            {
                Console.Write('=');
                for (var x = 0; x < buffer.GetLength(0); x++) {
                    Console.Write(buffer[x,y]);
                }
                Console.Write("=\n");
            }
            Console.Write(new string('=', buffer.GetLength(0) + 2));
        }

        private char Marker { get => '*'; }
        private void MarkCurrent()
        {
            var x = Math.Clamp((int)(X + 0.5f), 0, buffer.GetLength(0) - 1);
            var y = Math.Clamp((int)(Y + 0.5f), 0, buffer.GetLength(1) - 1);
            char c;
            switch (Pen)
            {
                case Pen.Down: c = Marker;break;
                case Pen.Up: return;
                case Pen.Erase: c = ' '; break;
                default: throw new InvalidOperationException();
            }
            buffer[x, y] = c;
        }

        public void Go(float dir, int n)
        {
            Direction += dir;
            var c = MathF.Cos(Direction);
            var s = MathF.Sin(Direction);
            for (var i=0; i < n; i++)
            {
                System.Diagnostics.Debug.WriteLine($"{X}, {Y} -> {(int)(X+0.5f)}, {(int)(Y+0.5f)}  ({Direction * 180/MathF.PI})");
                MarkCurrent();
                X += c;
                Y -= s;
            }
            Direction -= dir;
        }

        public void Forward(int n) {
            Go(0, n);
        }
        public void Backward(int n)
        {
            Go(MathF.PI, n);
        }

        public void Right(int n)
        {
            Go(-MathF.PI/2, n);
        }

        public void Left(int n)
        {
            Go(MathF.PI/2, n);
        }

        public void RotateLeft(float f)
        {
            Direction += f;
        }
        public void RotateRight(float f)
        {
            Direction -= f;
        }

        public void Polygon(int nSides, int length)
        {
            for (int i = 0; i < nSides; i++) {
                Right(length);
                RotateLeft(2 * MathF.PI / nSides);
            }
        }
    }


    class Program
    {
        static ICommand Parse(string cmd)
        {
            //var input = new AntlrInputStream(cmd);
            LogoLexer lexer = new LogoLexer(CharStreams.fromstring(cmd));
            LogoParser parser = new LogoParser(new CommonTokenStream(lexer));
            var cl = parser.program().command_list();
            return cl.c;
            //return new Repeat(3, new Sequence( new ICommand[] { new Move(8, Direction.Forward), new Rotate(120) }) );
        }
        static void Main(string[] args)
        {
            Turtle t = new Turtle(40, 40);
            // t.Polygon(3, 10);
            if (args.Length == 0) {
                //new Repeat(3, new Sequence( new ICommand[] { new Move(8, Direction.Forward), new Rotate(120) }) ).Do(t);
            } else {
                Parse(args[0]).Do(t);
            }
            // t.RotateRight(MathF.PI / 4);
            // t.Forward(6);
            t.PrintBuffer();
        }
    }
}
