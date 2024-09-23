using System;
using System.Collections.Concurrent;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using SnakeLib.Debug;
using SnakeLib.snake;
using SnakeLib.state;

namespace SnakeGame
{
    internal class SnakeWorker
    {
        private int TimeDelay = 1000;
        private const int TimeDecrease = 100;
        private readonly ConcurrentQueue<char> _keyStrokes = new ConcurrentQueue<char>();
        private readonly StreamWriter _w = new StreamWriter("KeyLog.txt");
        public void Start()
        {
            Task.Run(() => StartKeyReader());
            SnakePlayground pg = new SnakePlayground(20,20);
            
            // using table state machine
            //IState stateMachine = new SnakeTableStateMachine();

            // using state machine pattern
            IState stateMachine = new SnakeStateMachinePattern();

            bool gameContinue = true;
            pg.PrintPlayground();

            while (gameContinue)
            {
                //Console.WriteLine("Point = " + pg.Point);

                InputType nextInput = ReadNextEvent();
                Move nextMove = stateMachine.NextMove(nextInput);

                gameContinue = pg.DoNextMove(nextMove);
                if (gameContinue)
                {
                    pg.PrintPlayground();
                }

                if (pg.IsLonger)
                {
                    TimeDelay = (TimeDelay < TimeDecrease) ? TimeDelay : TimeDelay - TimeDecrease;
                }
                Thread.Sleep(TimeDelay);
                
            }

            Console.WriteLine();
            Console.WriteLine("You loose :-( ");
            _w.Close();
            Notify.Stop();
        }

        private void StartKeyReader()
        {
            while (true)
            {
                ConsoleKeyInfo info = Console.ReadKey();
                char c = info.KeyChar;
                switch (c)
                {
                    case 'a':
                        _keyStrokes.Enqueue(c);
                        break;
                    case 'd':
                        _keyStrokes.Enqueue(c);
                        break;
                    case 'w':
                        _keyStrokes.Enqueue(c);
                        break;
                    default: /* nothing */ break;
                }

                _w.WriteLine("input Q: " + string.Join(":",_keyStrokes));
                _w.Flush();
                Thread.Sleep(10); // wait 10 msec
            }
        }

        private InputType ReadNextEvent()
        {
            InputType ev = InputType.FORWARD;

            if (_keyStrokes.Count > 0)
            {
                char c = 'w'; // default forward
                _keyStrokes.TryDequeue(out c);
            
                switch (c)
                {
                    case 'a':
                        ev = InputType.LEFT;
                        break;
                    case 'd':
                        ev = InputType.RIGHT;
                        break;
                    case 'w':
                        ev = InputType.FORWARD;
                        break;
                }
            }

            return ev;
        }
    }
}