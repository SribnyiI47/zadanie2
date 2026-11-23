using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string sciezka = Path.Combine(AppContext.BaseDirectory, "../../../ranking_raw.txt");
        string[] linie = File.ReadAllLines(sciezka);

        List<string> ranking = new List<string>();
        ranking.Add("Nick;Czas;Punkty;Status;Opis");

        for (int i = 1; i < linie.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(linie[i]))
                continue;

            string[] pola = linie[i].Split(';');

            if (pola.Length != 5)
            {
                Console.WriteLine("Bledna linia: " + linie[i]);
                continue;
            }

            for (int j = 0; j < pola.Length; j++)
            {
                pola[j] = pola[j].Trim();
            }

            int punkty;

            if (!int.TryParse(pola[2], out punkty))
            {
                pola[2] = "0";
            }

            if (pola[3] == "HACKER" ||
                pola[1] == "00:00:01" ||
                pola[1] == "0:00:01")
            {
                Console.WriteLine("Odrzucono wynik: " + pola[0]);
                continue;
            }

            ranking.Add(string.Join(";", pola));
        }

        File.WriteAllLines(Path.Combine(AppContext.BaseDirectory, "ranking_clean.txt"), ranking);

        Console.WriteLine("Ranking zostal oczyszczony.");
        Console.WriteLine("Zapisano do ranking_clean.txt");
    }
}