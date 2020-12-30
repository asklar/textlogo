using System;

namespace logo
{
    class Turtle
    {
        public float Direction {get;set;}
        public float X {get;set;}
        public float Y {get;set;}
        private char[,] buffer;

        public Turtle(int width, int height)
        {
            buffer = new char[width, height];
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
            buffer[(int)(X+0.5f), (int)(Y+0.5f)] = Marker;
        }

        public void Go(float dir, int n)
        {
            Direction += dir;
            var c = MathF.Cos(Direction);
            var s = MathF.Sin(Direction);
            for (var i=0; i < n; i++)
            {
                Console.WriteLine($"{X}, {Y} -> {(int)(X+0.5f)}, {(int)(Y+0.5f)}  ({Direction * 180/MathF.PI})");
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
        static void Main(string[] args)
        {
            Turtle t = new Turtle(80, 80);
            t.Polygon(3, 20);
            // t.RotateRight(MathF.PI / 4);
            // t.Forward(6);
            t.PrintBuffer();
        }
    }
}
