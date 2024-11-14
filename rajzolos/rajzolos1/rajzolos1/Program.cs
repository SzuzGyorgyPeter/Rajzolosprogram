using System;
using System.IO;
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
            static string fileName = "";  // Fájl neve

            //╔ ╗ ║ ═ ╝ ╚
            static void Border()
            {
                Console.Write("╔" + new string('═', Console.WindowWidth - 2) + '╗');

                for (int i = 1; i < Console.WindowHeight - 2; i++)
                {
                    Console.Write('║' + new string(' ', Console.WindowWidth - 2) + '║');
                }

                Console.WriteLine("╚" + new string('═', Console.WindowWidth - 2) + '╝');

                Console.SetCursorPosition(0, 0);
            }

            static int selectedButton = 0;
            static string[] buttons = { "Létrehozás", "Módosítás", "Törlés", "Kilépés" };

            public void NavigateButtons()
            {
                ConsoleKeyInfo cki;
                do
                {
                    DrawButtons();
                    cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.UpArrow)
                    {
                        selectedButton = (selectedButton - 1 + buttons.Length) % buttons.Length;
                    }
                    else if (cki.Key == ConsoleKey.DownArrow)
                    {
                        selectedButton = (selectedButton + 1) % buttons.Length;
                    }
                    else if (cki.Key == ConsoleKey.Enter)
                    {
                        // Handle button click event
                        HandleButtonClick(selectedButton);
                    }
                    else if (cki.Key == ConsoleKey.Escape)
                    {
                        // Exit the navigation loop
                        Environment.Exit(0);
                    }
                } while (true);
            }

            static void HandleButtonClick(int buttonIndex)
            {
                // Handle button click event based on the index
                switch (buttonIndex)
                {
                    case 0:
                        // Handle "Létrehozás" button click
                        CreateFile();
                        DrawLoop(true);
                        break;
                    case 1:
                        // Handle "Módosítás" button click
                        ModifyFile();
                        break;
                    case 2:
                        // Handle "Törlés" button click
                        DeleteFile();
                        break;
                    case 3:
                        // Handle "Kilépés" button click
                        Environment.Exit(0);
                        break;
                }
            }

            static void DrawButtons()
            {
                int buttonWidth = 14;
                int buttonHeight = 4;
                int verticalSpacing = 2;

                int startX = (Console.WindowWidth - buttonWidth) / 2;
                int startY = (Console.WindowHeight - (buttons.Length * (buttonHeight + verticalSpacing))) / 2;

                for (int i = 0; i < buttons.Length; i++)
                {
                    int buttonPosY = startY + i * (buttonHeight + verticalSpacing);

                    Console.SetCursorPosition(startX, buttonPosY);
                    Console.Write('╔');
                    for (int j = 1; j <= buttonWidth - 2; j++)
                    {
                        Console.Write('═');
                    }
                    Console.Write('╗');

                    Console.SetCursorPosition(startX, buttonPosY + 1);
                    Console.Write('║');
                    if (i == selectedButton)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.ResetColor();
                    }
                    int textPadding = (buttonWidth - buttons[i].Length - 2) / 2;
                    Console.Write(new string(' ', textPadding) + buttons[i] + new string(' ', buttonWidth - buttons[i].Length - 2 - textPadding));
                    Console.ResetColor();
                    Console.Write('║');

                    Console.SetCursorPosition(startX, buttonPosY + 2);
                    Console.Write('╚');
                    for (int j = 1; j <= buttonWidth - 2; j++)
                    {
                        Console.Write('═');
                    }
                    Console.Write('╝');
                }
            }

            static void CreateFile()
            {
                Console.Clear();
                Console.Write("Enter file name: ");
                fileName = Console.ReadLine();
                File.Create(fileName).Dispose();
                Console.WriteLine("File created.");
            }

            static void ModifyFile()
            {
                Console.Clear();
                Console.WriteLine("List of created files:");
                string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
                for (int i = 0; i < files.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
                }
                Console.Write("Enter the number of the file you want to modify: ");
                int fileNumber;
                if (int.TryParse(Console.ReadLine(), out fileNumber) && fileNumber > 0 && fileNumber <= files.Length)
                {
                    fileName = files[fileNumber - 1];
                    if (File.Exists(fileName))
                    {
                        try
                        {
                            string[] lines = File.ReadAllLines(fileName);
                            if (lines.Length > 0)
                            {
                                string[] parts = lines[0].Split(',');
                                cursorX = int.Parse(parts[0].Split(':')[1].Trim());
                                cursorY = int.Parse(parts[1].Split(':')[1].Trim());
                                string colorName = parts[2].Split(':')[1].Trim();
                                switch (colorName)
                                {
                                    case "Blue":
                                        currentColor = ConsoleColor.Blue;
                                        break;
                                    case "Red":
                                        currentColor = ConsoleColor.Red;
                                        break;
                                    case "Green":
                                        currentColor = ConsoleColor.Green;
                                        break;
                                    case "Yellow":
                                        currentColor = ConsoleColor.Yellow;
                                        break;
                                    default:
                                        currentColor = ConsoleColor.White;
                                        break;
                                }
                                currentChar = char.Parse(parts[3].Split(':')[1].Trim());
                            }
                            DrawLoop(true);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error loading file: " + ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("File not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid file number.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }

            static void DeleteFile()
            {
                Console.Clear();
                Console.Write("Enter file name: ");
                fileName = Console.ReadLine();
                if (File.Exists(fileName))
                {
                    try
                    {
                        File.Delete(fileName);
                        Console.WriteLine("File deleted.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error deleting file: " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("File not found.");
                }
            }

            static void LoadDrawing()
            {
                if (File.Exists(fileName))
                {
                    string[] lines = File.ReadAllLines(fileName);
                    if (lines.Length > 0)
                    {
                        string[] parts = lines[0].Split(',');
                        cursorX = int.Parse(parts[0].Split(':')[1].Trim());
                        cursorY = int.Parse(parts[1].Split(':')[1].Trim());
                        string colorName = parts[2].Split(':')[1].Trim();
                        switch (colorName)
                        {
                            case "Blue":
                                currentColor = ConsoleColor.Blue;
                                break;
                            case "Red":
                                currentColor = ConsoleColor.Red;
                                break;
                            case "Green":
                                currentColor = ConsoleColor.Green;
                                break;
                            case "Yellow":
                                currentColor = ConsoleColor.Yellow;
                                break;
                            default:
                                currentColor = ConsoleColor.White;
                                break;
                        }
                        currentChar = char.Parse(parts[3].Split(':')[1].Trim());
                    }
                }
            }

            static void DrawLoop(bool saveOnExit)
            {
                ConsoleKey key;
                Console.Clear();
                DrawCursor();
                Border();

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
                        case ConsoleKey.Escape:
                            SaveDrawing();
                            Environment.Exit(0);
                            break;
                    }

                    ShowCurrentSettings();  // Aktuális beállítások megjelenítése
                    SaveDrawing();
                } while (true);
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
                Console.Write($"Current Color: {currentColor}, Current Char: {currentChar}   "); // Az extra szóközök letisztítják a korábbi kijelzést
                Console.SetCursorPosition(cursorX, cursorY);
            }

            static void SaveDrawing()
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.WriteLine($"Cursor X: {cursorX}, Cursor Y: {cursorY}, Color: {currentColor}, Char: {currentChar}");
                }
            }
        }

        static void Main()
        {
            ConsoleDrawer consoleDrawer = new ConsoleDrawer();
            consoleDrawer.NavigateButtons();
        }
    }
}