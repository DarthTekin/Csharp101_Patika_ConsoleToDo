using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp101_Patika_ConsoleToDo
{
    public enum KartBuyuklugu
    {
        XS,
        S,
        M,
        L,
        XL
    }

    public class Kart
    {
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public string AtananKisiID { get; set; }
        public KartBuyuklugu Buyukluk { get; set; }
    }

    public class TODOUygulamasi
    {
        private static Dictionary<string, string> takimUyeleri = new Dictionary<string, string>()
        {
            { "1", "Ahmet" },
            { "2", "Mehmet" },
            { "3", "Ayşe" }
        };

        private static List<Kart> kartlar = new List<Kart>()
        {
            new Kart { Baslik = "Kart 1", Icerik = "Kart 1 İçeriği", AtananKisiID = "1", Buyukluk = KartBuyuklugu.M },
            new Kart { Baslik = "Kart 2", Icerik = "Kart 2 İçeriği", AtananKisiID = "2", Buyukluk = KartBuyuklugu.S },
            new Kart { Baslik = "Kart 3", Icerik = "Kart 3 İçeriği", AtananKisiID = "3", Buyukluk = KartBuyuklugu.XL }
        };

        private static Dictionary<string, List<Kart>> board = new Dictionary<string, List<Kart>>()
        {
            { "TODO", new List<Kart>() },
            { "IN PROGRESS", new List<Kart>() },
            { "DONE", new List<Kart>() }
        };

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Yapmak istediğiniz işlemi seçin:");
                Console.WriteLine("1. Kart Ekle");
                Console.WriteLine("2. Kart Güncelle");
                Console.WriteLine("3. Kart Sil");
                Console.WriteLine("4. Kart Taşı");
                Console.WriteLine("5. Board Listeleme");
                Console.WriteLine("6. Çıkış");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        KartEkle();
                        break;
                    case "2":
                        KartGuncelle();
                        break;
                    case "3":
                        KartSil();
                        break;
                    case "4":
                        KartTasi();
                        break;
                    case "5":
                        BoardListele();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Geçersiz bir seçim yaptınız!");
                        break;
                }

                Console.WriteLine();
            }
        }

        private static void KartEkle()
        {
            Console.WriteLine("Kart Başlık:");
            string baslik = Console.ReadLine();

            Console.WriteLine("Kart İçerik:");
            string icerik = Console.ReadLine();

            Console.WriteLine("Atanan Kişi ID:");
            string atananKisiID = Console.ReadLine();

            if (!takimUyeleri.ContainsKey(atananKisiID))
            {
                Console.WriteLine("Hatalı giriş yaptınız! İşlem iptal edildi.");
                return;
            }

            Console.WriteLine("Kart Büyüklüğü (XS, S, M, L, XL):");
            string buyuklukStr = Console.ReadLine();
            if (!Enum.TryParse<KartBuyuklugu>(buyuklukStr, out KartBuyuklugu buyukluk))
            {
                Console.WriteLine("Hatalı giriş yaptınız! İşlem iptal edildi.");
                return;
            }

            Kart yeniKart = new Kart
            {
                Baslik = baslik,
                Icerik = icerik,
                AtananKisiID = atananKisiID,
                Buyukluk = buyukluk
            };

            board["TODO"].Add(yeniKart);
            Console.WriteLine("Kart başarıyla eklendi.");
        }

        private static void KartGuncelle()
        {
            Console.WriteLine("Güncellenecek Kart Başlık:");
            string baslik = Console.ReadLine();

            Kart kart = FindKart(baslik);
            if (kart == null)
            {
                Console.WriteLine("Kart bulunamadı!");
                return;
            }

            Console.WriteLine("Yeni Kart Başlık:");
            string yeniBaslik = Console.ReadLine();

            Console.WriteLine("Yeni Kart İçerik:");
            string yeniIcerik = Console.ReadLine();

            Console.WriteLine("Yeni Atanan Kişi ID:");
            string yeniAtananKisiID = Console.ReadLine();

            if (!takimUyeleri.ContainsKey(yeniAtananKisiID))
            {
                Console.WriteLine("Hatalı giriş yaptınız! İşlem iptal edildi.");
                return;
            }

            Console.WriteLine("Yeni Kart Büyüklüğü (XS, S, M, L, XL):");
            string yeniBuyuklukStr = Console.ReadLine();
            if (!Enum.TryParse<KartBuyuklugu>(yeniBuyuklukStr, out KartBuyuklugu yeniBuyukluk))
            {
                Console.WriteLine("Hatalı giriş yaptınız! İşlem iptal edildi.");
                return;
            }

            kart.Baslik = yeniBaslik;
            kart.Icerik = yeniIcerik;
            kart.AtananKisiID = yeniAtananKisiID;
            kart.Buyukluk = yeniBuyukluk;

            Console.WriteLine("Kart başarıyla güncellendi.");
        }

        private static void KartSil()
        {
            Console.WriteLine("Silinecek Kart Başlık:");
            string baslik = Console.ReadLine();

            Kart kart = FindKart(baslik);
            if (kart == null)
            {
                Console.WriteLine("Kart bulunamadı!");
                return;
            }

            foreach (var line in board.Values)
            {
                if (line.Contains(kart))
                {
                    line.Remove(kart);
                    break;
                }
            }

            Console.WriteLine("Kart başarıyla silindi.");
        }

        private static void KartTasi()
        {
            Console.WriteLine("Taşınacak Kart Başlık:");
            string baslik = Console.ReadLine();

            Kart kart = FindKart(baslik);
            if (kart == null)
            {
                Console.WriteLine("Kart bulunamadı!");
                return;
            }

            Console.WriteLine("Hedef Line (TODO, IN PROGRESS, DONE):");
            string hedefLine = Console.ReadLine();

            if (!board.ContainsKey(hedefLine))
            {
                Console.WriteLine("Hatalı giriş yaptınız! İşlem iptal edildi.");
                return;
            }

            foreach (var line in board.Values)
            {
                if (line.Contains(kart))
                {
                    line.Remove(kart);
                    break;
                }
            }

            board[hedefLine].Add(kart);

            Console.WriteLine("Kart başarıyla taşındı.");
        }

        private static void BoardListele()
        {
            Console.WriteLine("TODO Line:");
            Listele(board["TODO"]);

            Console.WriteLine("IN PROGRESS Line:");
            Listele(board["IN PROGRESS"]);

            Console.WriteLine("DONE Line:");
            Listele(board["DONE"]);
        }

        private static void Listele(List<Kart> kartlar)
        {
            foreach (var kart in kartlar)
            {
                Console.WriteLine("Başlık: " + kart.Baslik);
                Console.WriteLine("İçerik: " + kart.Icerik);
                Console.WriteLine("Atanan Kişi: " + takimUyeleri[kart.AtananKisiID]);
                Console.WriteLine("Büyüklük: " + kart.Buyukluk);
                Console.WriteLine();
            }
        }

        private static Kart FindKart(string baslik)
        {
            foreach (var line in board.Values)
            {
                foreach (var kart in line)
                {
                    if (kart.Baslik == baslik)
                    {
                        return kart;
                    }
                }
            }

            return null;
        }
    }
}

