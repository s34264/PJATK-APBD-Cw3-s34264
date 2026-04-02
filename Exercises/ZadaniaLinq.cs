using LinqConsoleLab.PL.Data;
using LinqConsoleLab.PL.Models;
using System.Linq;

namespace LinqConsoleLab.PL.Exercises;

public sealed class ZadaniaLinq
{
    /// <summary>
    /// Zadanie:
    /// Wyszukaj wszystkich studentów mieszkających w Warsaw.
    /// Zwróć numer indeksu, pełne imię i nazwisko oraz miasto.
    ///
    /// SQL:
    /// SELECT NumerIndeksu, Imie, Nazwisko, Miasto
    /// FROM Studenci
    /// WHERE Miasto = 'Warsaw';
    /// </summary>
    public IEnumerable<string> Zadanie01_StudenciZWarszawy()
    {
        return DaneUczelni.Studenci
                .Where(s => s.Miasto == "Warsaw")
                .Select(s => $"{s.Imie} | {s.Nazwisko} | {s.Miasto}");
    }
    
    /// <summary>
    /// Zadanie:
    /// Przygotuj listę adresów e-mail wszystkich studentów.
    /// Użyj projekcji, tak aby w wyniku nie zwracać całych obiektów.
    ///
    /// SQL:
    /// SELECT Email
    /// FROM Studenci;
    /// </summary>
    public IEnumerable<string> Zadanie02_AdresyEmailStudentow()
    {
        return DaneUczelni.Studenci.Select(s => s.Email);
    }

    /// <summary>
    /// Zadanie:
    /// Posortuj studentów alfabetycznie po nazwisku, a następnie po imieniu.
    /// Zwróć numer indeksu i pełne imię i nazwisko.
    ///
    /// SQL:
    /// SELECT NumerIndeksu, Imie, Nazwisko
    /// FROM Studenci
    /// ORDER BY Nazwisko, Imie;
    /// </summary>
    public IEnumerable<string> Zadanie03_StudenciPosortowani()
    {
        return DaneUczelni
            .Studenci
            .OrderBy(s => s.Nazwisko)
            .ThenBy(s => s.Imie)
            .Select(s => $"{s.NumerIndeksu} | {s.Imie} | {s.Nazwisko}");
    }

    /// <summary>
    /// Zadanie:
    /// Znajdź pierwszy przedmiot z kategorii Analytics.
    /// Jeżeli taki przedmiot nie istnieje, zwróć komunikat tekstowy.
    ///
    /// SQL:
    /// SELECT TOP 1 Nazwa, DataStartu
    /// FROM Przedmioty
    /// WHERE Kategoria = 'Analytics';
    /// </summary>p
    public IEnumerable<string> Zadanie04_PierwszyPrzedmiotAnalityczny()
    {
        List<string> resultList = new List<string>();
        string result = "Not found";
        
        Przedmiot przedmiot = DaneUczelni
            .Przedmioty
            .FirstOrDefault(p => p.Kategoria == "Analytics");

        if (przedmiot is not null)
        {
            result = $"{przedmiot.Nazwa} | {przedmiot.DataStartu}";
        }
        resultList.Add(result);

        return resultList;
    }

    /// <summary>
    /// Zadanie:
    /// Sprawdź, czy w danych istnieje przynajmniej jeden nieaktywny zapis.
    /// Zwróć jedno zdanie z odpowiedzią True/False albo Tak/Nie.
    ///
    /// SQL:
    /// SELECT CASE WHEN EXISTS (
    ///     SELECT 1
    ///     FROM Zapisy
    ///     WHERE CzyAktywny = 0
    /// ) THEN 1 ELSE 0 END;
    /// </summary>
    public IEnumerable<string> Zadanie05_CzyIstniejeNieaktywneZapisanie()
    {
        List<string> resultList = new List<string>();
        bool any = DaneUczelni
            .Zapisy
            .Any(z => z.CzyAktywny == false); 
        resultList.Add(any.ToString());
        return resultList;
    }

    /// <summary>
    /// Zadanie:
    /// Sprawdź, czy każdy prowadzący ma uzupełnioną nazwę katedry.
    /// Warto użyć metody, która weryfikuje warunek dla całej kolekcji.
    ///
    /// SQL:
    /// SELECT CASE WHEN COUNT(*) = COUNT(Katedra)
    /// THEN 1 ELSE 0 END
    /// FROM Prowadzacy;
    /// </summary>
    public IEnumerable<string> Zadanie06_CzyWszyscyProwadzacyMajaKatedre()
    {
        List<string> resultList = new List<string>();
        resultList.Add(
                DaneUczelni.Prowadzacy
                .All(p => p.Katedra.Length > 0)
                .ToString()
            );
        return resultList;
    }

    /// <summary>
    /// Zadanie:
    /// Policz, ile aktywnych zapisów znajduje się w systemie.
    ///
    /// SQL:
    /// SELECT COUNT(*)
    /// FROM Zapisy
    /// WHERE CzyAktywny = 1;
    /// </summary>
    public IEnumerable<string> Zadanie07_LiczbaAktywnychZapisow()
    {
        List<string> resultList = new List<string>();
        int num = DaneUczelni.Zapisy.Count(z => z.CzyAktywny == true);
        resultList.Add(num.ToString());
        return resultList;
    }

    /// <summary>
    /// Zadanie:
    /// Pobierz listę unikalnych miast studentów i posortuj ją rosnąco.
    ///
    /// SQL:
    /// SELECT DISTINCT Miasto
    /// FROM Studenci
    /// ORDER BY Miasto;
    /// </summary>
    public IEnumerable<string> Zadanie08_UnikalneMiastaStudentow()
    {
       return DaneUczelni.Studenci
           .Select(s => s.Miasto)
           .Distinct()
           .OrderBy(m => m);
    }

    /// <summary>
    /// Zadanie:
    /// Zwróć trzy najnowsze zapisy na przedmioty.
    /// W wyniku pokaż datę zapisu, identyfikator studenta i identyfikator przedmiotu.
    ///
    /// SQL:
    /// SELECT TOP 3 DataZapisu, StudentId, PrzedmiotId
    /// FROM Zapisy
    /// ORDER BY DataZapisu DESC;
    /// </summary>
    public IEnumerable<string> Zadanie09_TrzyNajnowszeZapisy()
    {
        return DaneUczelni.Zapisy
            .OrderByDescending(z => z.DataZapisu)
            .Select(z => $"{z.DataZapisu} | {z.StudentId} | {z.PrzedmiotId}")
            .Take(3);
    }

    /// <summary>
    /// Zadanie:
    /// Zaimplementuj prostą paginację dla listy przedmiotów.
    /// Załóż stronę o rozmiarze 2 i zwróć drugą stronę danych.
    ///
    /// SQL:
    /// SELECT Nazwa, Kategoria
    /// FROM Przedmioty
    /// ORDER BY Nazwa
    /// OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY;
    /// </summary>
    public IEnumerable<string> Zadanie10_DrugaStronaPrzedmiotow()
    {
        //skip
        //take
        //zm. nr str.
        //zm. ilosć elementow strony
        const int Num = 1;
        const int RozmiarStrony = 2;
        return DaneUczelni.Przedmioty
            .OrderBy(p => p.Nazwa)
            .Skip(Num * RozmiarStrony)
            .Take(RozmiarStrony)
            .Select(p => $"{p.Nazwa} {p.Kategoria}");
    }

    /// <summary>
    /// Zadanie:
    /// Połącz studentów z zapisami po StudentId.
    /// Zwróć pełne imię i nazwisko studenta oraz datę zapisu.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, z.DataZapisu
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId;
    /// </summary>
    public IEnumerable<string> Zadanie11_PolaczStudentowIZapisy()
    {
        return DaneUczelni.Studenci.Join(
            DaneUczelni.Zapisy,
            s =>  s.Id,
            z => z.StudentId,
            (s, z) => $"{s.Imie} | {s.Nazwisko} | {z.DataZapisu}"
        );
    }

    /// <summary>
    /// Zadanie:
    /// Przygotuj wszystkie pary student-przedmiot na podstawie zapisów.
    /// Użyj podejścia, które pozwoli spłaszczyć dane do jednej sekwencji wyników.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, p.Nazwa
    /// FROM Zapisy z
    /// JOIN Studenci s ON s.Id = z.StudentId
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId;
    /// </summary>
    public IEnumerable<string> Zadanie12_ParyStudentPrzedmiot()
    {
        return DaneUczelni.Zapisy
            .Join(
            DaneUczelni.Studenci,
            z => z.StudentId,
            s => s.Id,
            (z, s) => new
                {
                    zapis = z,
                    student = s
                }
            )
            .Join(
                DaneUczelni.Przedmioty,
                obj => obj.zapis.PrzedmiotId,
                p => p.Id,
                (para, przedmiot) => $"{para.student.Imie} | {para.student.Nazwisko} | {przedmiot.Nazwa}"
            );
    }

    /// <summary>
    /// Zadanie:
    /// Pogrupuj zapisy według przedmiotu i zwróć nazwę przedmiotu oraz liczbę zapisów.
    ///
    /// SQL:
    /// SELECT p.Nazwa, COUNT(*)
    /// FROM Zapisy z
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId
    /// GROUP BY p.Nazwa;
    /// </summary>
    public IEnumerable<string> Zadanie13_GrupowanieZapisowWedlugPrzedmiotu()
    {
        return DaneUczelni
            .Zapisy
            .Join(
                DaneUczelni.Przedmioty,
                z => z.PrzedmiotId,
                p => p.Id,
                (z, p) => new
                {
                    Zapis = z,
                    Przedmiot = p
                }
            ).GroupBy(para => para.Przedmiot.Nazwa)
            .Select(group => $"{group.Key} : {group.Count()}");
    }

    /// <summary>
    /// Zadanie:
    /// Oblicz średnią ocenę końcową dla każdego przedmiotu.
    /// Pomiń rekordy, w których ocena końcowa ma wartość null.
    ///
    /// SQL:
    /// SELECT p.Nazwa, AVG(z.OcenaKoncowa)
    /// FROM Zapisy z
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY p.Nazwa;
    /// </summary>
    public IEnumerable<string> Zadanie14_SredniaOcenaNaPrzedmiot()
    {
        return DaneUczelni.Zapisy
            .Where(z => z.OcenaKoncowa is not null)
            .Join(
                DaneUczelni.Przedmioty,
                z => z.PrzedmiotId,
                p => p.Id,
                (z, p) => (z, p)
            )
            .GroupBy(para => para.p.Nazwa)
            .Select(para => $"{para.Key} : {para.Average(
                    para => para.z.OcenaKoncowa)
                }");
    }

    /// <summary>
    /// Zadanie:
    /// Dla każdego prowadzącego policz liczbę przypisanych przedmiotów.
    /// W wyniku zwróć pełne imię i nazwisko oraz liczbę przedmiotów.
    ///
    /// SQL:
    /// SELECT pr.Imie, pr.Nazwisko, COUNT(p.Id)
    /// FROM Prowadzacy pr
    /// LEFT JOIN Przedmioty p ON p.ProwadzacyId = pr.Id
    /// GROUP BY pr.Imie, pr.Nazwisko;
    /// </summary>
    public IEnumerable<string> Zadanie15_ProwadzacyILiczbaPrzedmiotow()
    {
        return DaneUczelni.Prowadzacy.GroupJoin(
                DaneUczelni.Przedmioty,
                pro => pro.Id,
                prz => prz.ProwadzacyId,
                (prowadzacy, przedmioty) => new
                {
                    prowadzacy,
                    przedmioty
                }
            )
            .Select(para => $"{para.prowadzacy.Imie} {para.prowadzacy.Nazwisko} {para.przedmioty.Count()}"
            );

    }

    /// <summary>
    /// Zadanie:
    /// Dla każdego studenta znajdź jego najwyższą ocenę końcową.
    /// Pomiń studentów, którzy nie mają jeszcze żadnej oceny.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, MAX(z.OcenaKoncowa)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY s.Imie, s.Nazwisko;
    /// </summary>
    public IEnumerable<string> Zadanie16_NajwyzszaOcenaKazdegoStudenta()
    {
        return DaneUczelni.Studenci
            .GroupJoin(
                DaneUczelni.Zapisy,
                s => s.Id,
                z => z.StudentId,
                (s, z) => new
                {
                    Student = s,
                    Zapisy = z
                })
          .Select(
                data => $"{data.Student.Imie} {data.Student.Nazwisko} : {data.Zapisy.Max(zapis => zapis.OcenaKoncowa)}"
            );
    }

    /// <summary>
    /// Wyzwanie:
    /// Znajdź studentów, którzy mają więcej niż jeden aktywny zapis.
    /// Zwróć pełne imię i nazwisko oraz liczbę aktywnych przedmiotów.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, COUNT(*)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.CzyAktywny = 1
    /// GROUP BY s.Imie, s.Nazwisko
    /// HAVING COUNT(*) > 1;
    /// </summary>
    public IEnumerable<string> Wyzwanie01_StudenciZWiecejNizJednymAktywnymPrzedmiotem()
    {
        return DaneUczelni.Studenci.Join(
                DaneUczelni.Zapisy.Where(z => z.CzyAktywny == true),
                s => s.Id,
                z => z.StudentId,
                (s, z) => new
                {
                    Student = s,
                    Zapis = z
                }
            )
            .GroupBy(para => para.Student)
            .Where(group => group.Count() > 1)
            .Select(group => $"{group.Key.Imie} {group.Key.Nazwisko} - {group.Count()}");
    }

    /// <summary>
    /// Wyzwanie:
    /// Wypisz przedmioty startujące w kwietniu 2026, dla których żaden zapis nie ma jeszcze oceny końcowej.
    ///
    /// SQL:
    /// SELECT p.Nazwa
    /// FROM Przedmioty p
    /// JOIN Zapisy z ON p.Id = z.PrzedmiotId
    /// WHERE MONTH(p.DataStartu) = 4 AND YEAR(p.DataStartu) = 2026
    /// GROUP BY p.Nazwa
    /// HAVING SUM(CASE WHEN z.OcenaKoncowa IS NOT NULL THEN 1 ELSE 0 END) = 0;
    /// </summary>
    public IEnumerable<string> Wyzwanie02_PrzedmiotyStartujaceWKwietniuBezOcenKoncowych()
    {
        return DaneUczelni.Przedmioty
            .Where(przedmiot => przedmiot.DataStartu.Month == 4 && przedmiot.DataStartu.Year == 2026)
            .GroupJoin(
                DaneUczelni.Zapisy.Where(zapisy => zapisy.OcenaKoncowa is not null),
                przedmiot => przedmiot.Id,
                zapis => zapis.PrzedmiotId,
                (przedmiot, zapisy) => new
                {
                    Przedmiot = przedmiot,
                    Zapisy = zapisy
                })
            .Where(para => para.Zapisy.ToList().Count == 0)
            .Select(para => para.Przedmiot.Nazwa);
    }

    /// <summary>
    /// Wyzwanie:
    /// Oblicz średnią ocen końcowych dla każdego prowadzącego na podstawie wszystkich jego przedmiotów.
    /// Pomiń brakujące oceny, ale pozostaw samych prowadzących w wyniku.
    ///
    /// SQL:
    /// SELECT pr.Imie, pr.Nazwisko, AVG(z.OcenaKoncowa)
    /// FROM Prowadzacy pr
    /// LEFT JOIN Przedmioty p ON p.ProwadzacyId = pr.Id
    /// LEFT JOIN Zapisy z ON z.PrzedmiotId = p.Id
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY pr.Imie, pr.Nazwisko;
    /// </summary>
    public IEnumerable<string> Wyzwanie03_ProwadzacyISredniaOcenNaIchPrzedmiotach()
    {
        return DaneUczelni.Prowadzacy
            .Join(
                DaneUczelni.Przedmioty,
                prowadzacy => prowadzacy.Id,
                przedmioty => przedmioty.Id,
                (prowadzancy, przedmiot) => new
                {
                    Prowadzancy = prowadzancy,
                    Przedmiot = przedmiot
                }
            )
            .GroupJoin(
                DaneUczelni.Zapisy.Where(zapis => zapis.OcenaKoncowa is not null),
                para => para.Przedmiot.Id,
                zapisy => zapisy.PrzedmiotId,
                    (para, zapis) => new
                    {
                        Para = para,
                        Zapis = zapis
                    }
            )
            .Select(data => $"{data.Para.Prowadzancy.Imie} {data.Para.Prowadzancy.Nazwisko} : Average {data.Zapis.Average(zapis => zapis.OcenaKoncowa)}");
    }

    /// <summary>
    /// Wyzwanie:
    /// Pokaż miasta studentów oraz liczbę aktywnych zapisów wykonanych przez studentów z danego miasta.
    /// Posortuj wynik malejąco po liczbie aktywnych zapisów.
    ///
    /// SQL:
    /// SELECT s.Miasto, COUNT(*)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.CzyAktywny = 1
    /// GROUP BY s.Miasto
    /// ORDER BY COUNT(*) DESC;
    /// </summary>
    public IEnumerable<string> Wyzwanie04_MiastaILiczbaAktywnychZapisow()
    {
        return DaneUczelni.Studenci
            .Join(
                DaneUczelni.Zapisy.Where(z => z.CzyAktywny == true),
                student => student.Id,
                zapis => zapis.StudentId,
                (student, zapis) => new
                {
                    Student = student,
                    Zapis = zapis
                })
            .GroupBy(para => para.Student.Miasto)
            .OrderByDescending(group => group.Count())
            .Select(group => $"{group.Key} - {group.Count()}");
    }

    private static NotImplementedException Niezaimplementowano(string nazwaMetody)
    {
        return new NotImplementedException(
            $"Uzupełnij metodę {nazwaMetody} w pliku Exercises/ZadaniaLinq.cs i uruchom polecenie ponownie.");
    }
}
