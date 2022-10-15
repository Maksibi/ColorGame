using System;
using SFML.Learning;
using SFML.Graphics;
using System.Collections.Generic;
using System.Linq;
using SFML.Window;
using SFML.Audio;

internal class Program : Game
{
    static Dictionary<string, Color> Words = new Dictionary<string, Color>()
    {
        ["Синий"] = Color.Blue,
        ["Зеленый"] = Color.Green,
        ["Красный"] = Color.Red,
        ["Голубой"] = Color.Cyan
    };

    static int input = -1, colorIndex = -1;
    static int score = 0, maxScore = 0;
    static string soundLose = LoadSound("sound_lose.wav"), soundPoint = LoadSound("sound_point.wav");
    static string music = LoadMusic("undertale_enemy-Approaching.ogg");
    static Color color;
    static string word;
    static bool isGame = false;
    static float timer = 10;
    static float efficiencyCoefficient;
    static void Reset()
    {
        StopMusic(music);
        PlaySound(soundLose, 40);
        score = 0;
        input = -1;
        colorIndex = -1;
        GenerateText();
        timer = 10;
        Delay(2000);
        PlayMusic(music, 20);
    }

    static void GenerateText()
    {
        Random rand = new Random();
        color = Words.ElementAt(rand.Next(0, Words.Count)).Value;
        word = Words.ElementAt(rand.Next(0, Words.Count)).Key;

        if (color == Color.Red) colorIndex = 0;
        if (color == Color.Green) colorIndex = 1;
        if (color == Color.Blue) colorIndex = 2;
        if (color == Color.Cyan) colorIndex = 3;

        SetFillColor(color);
        DrawText(350, 275, word, 50);
    }

    static void Controls()
    {
        if (GetKeyUp(Keyboard.Key.C) == true)
        {
            input = 2;
            if (input == colorIndex)
            {
                PlaySound(soundPoint);
                score++;
                GenerateText();
                timer = 10;
            }
            else Reset();

        }
        if (GetKeyUp(Keyboard.Key.R) == true)
        {
            input = 0;
            if (input == colorIndex)
            {
                PlaySound(soundPoint);
                score++;
                GenerateText();
                timer = 10;
            }
            else Reset();
        }
        if (GetKeyUp(Keyboard.Key.P) == true)
        {
            input = 1;
            if (input == colorIndex)
            {
                PlaySound(soundPoint);
                score++;
                GenerateText();
                timer = 10;
            }
            else Reset();
        }
        if (GetKeyUp(Keyboard.Key.U) == true)
        {
            input = 3;
            if (input == colorIndex)
            {
                PlaySound(soundPoint);
                score++;
                GenerateText();
                timer = 10;
            }
            else Reset();
        }

    }

    static void Main(string[] args)
    {
        InitWindow(800, 600, "ColorGame");
        SetFont("Undertale-Battle-Font.ttf");
        SetFillColor(Color.White);
        DrawText(0, 300, "В этой игре вам предстоит проверить свою внимательность.", 20);
        DrawText(0, 340, "Следите за цветом, которым написано слово\n и жмите соответствующую клавишу.", 20);
        DrawText(0, 420, "Для начала игры нажмите \"Пробел\"", 20);
        DisplayWindow();
        PlayMusic(music, 20);
    L1:
        if (GetKeyUp(Keyboard.Key.Space) == true) isGame = true;
        GenerateText();
        while (true)
        {
            efficiencyCoefficient = 1f;
            efficiencyCoefficient += score / 10;
            timer -= DeltaTime * efficiencyCoefficient;
            //((score+1)/2);
            DispatchEvents();
            if (timer <= 0) Reset();
            if (isGame == true)
            {
                if (maxScore < score) maxScore++;
                ClearWindow();

                SetFillColor(Color.White);
                DrawText(0, 0, "Синий - С(C)\nКрасный - К(R)\nГолубой - Г(U)\nЗеленый - З(P)", 20);
                DrawText(0, 520, "Очки: " + score, 20);
                DrawText(0, 560, "Рекорд: " + maxScore, 20);
                DrawText(380, 0, "Время: " + Math.Round(timer, 0), 30);
                SetFillColor(color);
                DrawText(350, 275, word, 50);
                Controls();
                DisplayWindow();
                Console.WriteLine(timer);
                Delay(1);
            }
            else goto L1;
        }
    }
}
