using System;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Threading;

namespace MouseClicker
{
    class Program
    {
        static Key trigger = Key.AbntC1;
        static bool triggerHeld = false;
        static Key autoKey = Key.AbntC1;
        static int mouseButton = 0;
        static double speed = 10;
        static bool clicking = false;

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Hit trigger key.");
            trigger = GetNextKeyDown();
            Console.WriteLine(trigger);
            Thread.Sleep(1000);
            
            Console.WriteLine("Choose which button to autoclick.");
            while (true)
            {
                Console.WriteLine("Hit 'L' for leftclick and 'R' for right click.");
                Key RorL = GetNextKeyDown();
                Console.WriteLine(RorL);
                Thread.Sleep(1000);
                if (RorL == Key.R)
                {
                    mouseButton = 2;
                    break;
                }
                else if (RorL == Key.L)
                {
                    mouseButton = 1;
                    break;
                }
            }

            while (true)
            {
                Console.WriteLine("Type the speed to autoclick. c/s");
                if (double.TryParse(Console.ReadLine(), out speed))
                {
                    Thread.Sleep(1000);
                    break;
                }
                else
                {
                    Console.WriteLine("Type only a number.");
                }
            }

            Console.WriteLine("Hit the trigger key to toggle autoclicking.");

            while (true)
            {
                if (Keyboard.IsKeyDown(trigger))
                {
                    if (!triggerHeld)
                    {
                        if (clicking)
                        {
                            clicking = false;
                        }
                        else
                        {
                            clicking = true;
                        }
                        triggerHeld = true;
                    }
                }
                else if (triggerHeld)
                {
                    triggerHeld = false;
                }
                Thread.Sleep((int)(1000.0 / speed));

                if (clicking)
                {
                    if (mouseButton == 0)
                    {

                    }
                    else if (mouseButton == 1)
                    {
                        VirtualMouse.LeftClick();
                    }
                    else if (mouseButton == 2)
                    {
                        VirtualMouse.RightClick();
                    }
                }
            }
        }

        [STAThread]
        public static Key GetAnyKeyDown()
        {
            var values = Enum.GetValues(typeof(Key));

            foreach (var v in values)
            {
                if (((Key)v) != Key.None)
                {
                    if (Keyboard.IsKeyDown((Key)v))
                    {
                        return (Key)v;
                    }
                }
            }

            return Key.AbntC1;
        }

        [STAThread]
        public static Key GetNextKeyDown()
        {
            while (true)
            {
                Key val = GetAnyKeyDown();
                if (val != Key.AbntC1)
                {
                    return val;
                }
            }
        }
        
    }

    

    public static class VirtualMouse
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        public static void Move(int xDelta, int yDelta)
        {
            mouse_event(MOUSEEVENTF_MOVE, xDelta, yDelta, 0, 0);
        }
        public static void MoveTo(int x, int y)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, x, y, 0, 0);
        }
        public static void LeftClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        public static void LeftDown()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        public static void LeftUp()
        {
            mouse_event(MOUSEEVENTF_LEFTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }



        public static void RightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        public static void RightDown()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        public static void RightUp()
        {
            mouse_event(MOUSEEVENTF_RIGHTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }
    }
}
