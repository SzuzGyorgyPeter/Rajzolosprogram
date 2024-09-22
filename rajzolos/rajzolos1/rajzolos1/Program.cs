using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rajzolos1
{
    internal class Program
    {
        class ConsoleDrawer
        {
            static int cursorX = 0;  // Kurzor pozíciója X tengelyen
            static int cursorY = 0;  // Kurzor pozíciója Y tengelyen
            static ConsoleColor currentColor = ConsoleColor.White;  // Alapértelmezett szín
            static char currentChar = '█';  // Alapértelmezett karakter

            static void Main()
            {
                ConsoleKey key;
                Console.Clear();
                DrawCursor();
                do
                {
                    key = Console.ReadKey(true).Key;  // Billentyű lenyomásra várakozás
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            if (cursorY > 0) cursorY--;  // Mozgás felfelé
                            break;
                        case ConsoleKey.DownArrow:
                            if (cursorY < Console.WindowHeight - 1) cursorY++;  // Mozgás lefelé
                            break;
                        case ConsoleKey.LeftArrow:
                            if (cursorX > 0) cursorX--;  // Mozgás balra
                            break;
                        case ConsoleKey.RightArrow:
                            if (cursorX < Console.WindowWidth - 1) cursorX++;  // Mozgás jobbra
                            break;
                        case ConsoleKey.Spacebar:
                            DrawCursor();
                            break;
                        case ConsoleKey.NumPad1:
                            currentColor = ConsoleColor.Blue;
                            Console.ForegroundColor = currentColor;
                            break;
                        case ConsoleKey.NumPad2:
                            currentColor = ConsoleColor.Red;
                            Console.ForegroundColor = currentColor;
                            break;
                        case ConsoleKey.NumPad3:
                            currentColor = ConsoleColor.Green;
                            Console.ForegroundColor = currentColor;
                            break;
                        case ConsoleKey.NumPad4:
                            currentColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = currentColor;
                            break;

                        case ConsoleKey.D1:
                            currentChar = '█';  // Karakter változtatása
                            break;
                        case ConsoleKey.D2:
                            currentChar = '▓';  // Karakter változtatása
                            break;
                        case ConsoleKey.D3:
                            currentChar = '▒';  // Karakter változtatása
                            break;
                        case ConsoleKey.D4:
                            currentChar = '░';  // Karakter változtatása
                            break;
                    }

                    ShowCurrentSettings();  // Aktuális beállítások megjelenítése
                } while (key != ConsoleKey.Escape);  // Kilépés az Escape gombbal
            }

            // Kurzor rajzolása a jelenlegi helyén
            static void DrawCursor()
            {
                Console.SetCursorPosition(cursorX, cursorY);
                Console.Write(currentChar);  // Kurzor megjelenítése
                Console.SetCursorPosition(cursorX, cursorY);
            }

            // Aktuális beállítások kijelzése az ablak alján
            static void ShowCurrentSettings()
            {
                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                Console.Write($"Current Color: {currentColor}, Current Char: {currentChar}   ");  // Az extra szóközök letisztítják a korábbi kijelzést
                Console.SetCursorPosition(cursorX, cursorY);
            }
        }
    }
}
