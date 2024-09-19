using System;

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
                    Console.SetCursorPosition(cursorX, cursorY);
                    Console.ForegroundColor = currentColor;  // Beállított szín
                    Console.Write(currentChar);  // Kirajzolás a kurzor helyén
                    break;
                case ConsoleKey.D1:
                    currentColor = ConsoleColor.Red;  // Szín pirosra váltása
                    break;
                case ConsoleKey.D2:
                    currentColor = ConsoleColor.Green;  // Szín zöldre váltása
                    break;
                case ConsoleKey.D3:
                    currentColor = ConsoleColor.Blue;  // Szín kékre váltása
                    break;
                case ConsoleKey.D4:
                    currentChar = '█';  // Karakter változtatása
                    break;
                case ConsoleKey.D5:
                    currentChar = '▓';  // Karakter változtatása
                    break;
                case ConsoleKey.D6:
                    currentChar = '▒';  // Karakter változtatása
                    break;
                case ConsoleKey.D7:
                    currentChar = '░';  // Karakter változtatása
                    break;
            }

            DrawCursor();  // Kurzor újra rajzolása
            ShowCurrentSettings();  // Aktuális beállítások megjelenítése
        } while (key != ConsoleKey.Escape);  // Kilépés az Escape gombbal
    }

    // Kurzor rajzolása a jelenlegi helyén
    static void DrawCursor()
    {
        Console.SetCursorPosition(cursorX, cursorY);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write('X');  // Kurzor megjelenítése
        Console.SetCursorPosition(cursorX, cursorY);
    }

    // Aktuális beállítások kijelzése az ablak alján
    static void ShowCurrentSettings()
    {
        Console.SetCursorPosition(0, Console.WindowHeight - 1);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"Current Color: {currentColor}, Current Char: {currentChar}   ");  // Az extra szóközök letisztítják a korábbi kijelzést
        Console.SetCursorPosition(cursorX, cursorY);
    }
}
