namespace szamok;

public class Szamok
{
    public string Kerdes { get; set; }
    public int Valasz { get; set; }
    public int Pont { get; set; }
    public string Tema { get; set; }
}
public class Program
{
    static List<Szamok> list = new List<Szamok>();
    static Random rand = new Random();
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Feladat1();
        Feladat2();
        Feladat3();
        Feladat4();
        Feladat5();
        Feladat6();
        Feladat7();

        Console.ReadKey();
    }
    private static void Feladat1()
    {
        string[] adat = File.ReadAllLines(@"felszam.txt");

        for (int i = 0; i < adat.Length; i +=2)
        {
            Szamok uj = new Szamok();
            string[] sor2 = adat[i + 1].Split(' ');

            uj.Kerdes = adat[i];
            uj.Valasz = int.Parse(sor2[0]);
            uj.Pont = int.Parse(sor2[1]);
            uj.Tema = sor2[2];

            list.Add(uj);
        }
    }
    private static void Feladat2()
    {
        Console.WriteLine("2. feladat");
        Console.WriteLine($"Az adatfájlban {list.Count} fájl van.\n");
    }
    private static void Feladat3()
    {
        Console.WriteLine("3. feladat");

        var szures = list.Where(x => x.Tema == "matematika").ToList();
        int[] pontok = new int[3];

        foreach (var item in list)
        {
            switch (item.Pont)
            {
                case 1:
                    pontok[0]++;
                    break;
                case 2:
                    pontok[1]++;
                    break;
                case 3:
                    pontok[2]++;
                    break;
                default:
                    break;
            }
        }

        Console.WriteLine($"Az adatfájlban {szures.Count} matematika feladat van, " +
            $"1 pontot ér {pontok[0]} feladat, 2 pontot ér {pontok[1]} feladat, 3 pontot ér {pontok[2]} feladat.\n");
    }
    private static void Feladat4()
    {
        Console.WriteLine("4. feladat");

        int min = list[0].Valasz, max = list[0].Valasz;

        foreach (var item in list)
        {
            if (item.Valasz > max) max = item.Valasz;
            if (item.Valasz < min) min = item.Valasz;
        }

        Console.WriteLine($"A válaszok számértéke {min}-től {max}-ig terjed.\n");
    }
    private static void Feladat5()
    {
        Console.WriteLine("5. feladat");

        var csoport = list.GroupBy(x => x.Tema).OrderBy(g => g.Key).ToList();

        foreach (var group in csoport)
        {
            Console.WriteLine(group.Key);
        }

        Console.WriteLine();
    }
    private static void Feladat6()
    {
        Console.WriteLine("6. feladat");

        Console.Write("Milyen témakőrből szeretne kérdést kapni? ");
        string bTema = Console.ReadLine();

        var szures = list.Where(x => x.Tema == bTema).ToList();
        int szam = rand.Next(0, szures.Count);
        var kerdes = szures[szam];

        Console.Write($"{kerdes.Kerdes} ");
        int valasz = int.Parse(Console.ReadLine());

        if (valasz == kerdes.Valasz) Console.WriteLine($"A válasz {kerdes.Pont} pontot ér");
        else
        {
            Console.WriteLine("A válasz 0 pontot ér");
            Console.WriteLine($"A helyes válasz: {kerdes.Valasz}\n");
        }
    }
    private static void Feladat7()
    {
        Console.WriteLine("7. feladat");

        StreamWriter sw = new StreamWriter(@"tesztfel.txt");

        int[] szamok = new int[10];
        int osszPont = 0;

        for (int i = 0; i < szamok.Length; i++)
        {
            int szam = 0;

            do
            {
                szam = rand.Next(0, list.Count);
            } while (szamok.Contains(szam));
            szamok[i] = szam;
        }

        for (int i = 0; i < szamok.Length; i++)
        {
            var kerdes = list[szamok[i]];
            osszPont += kerdes.Pont;
            sw.WriteLine($"{kerdes.Pont} {kerdes.Kerdes}\n");
        }

        sw.Write($"A feladatsorra összesen {osszPont} pont adható.");
        sw.Close();
        Console.WriteLine("Feladatsor kész.");
    }
}
