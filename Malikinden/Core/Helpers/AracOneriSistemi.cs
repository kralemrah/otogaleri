using System;
using System.Collections.Generic;
using System.Linq;
using Entity;

namespace Core.Helpers
{
    public class AracOneriSistemi
    {
        public class OneriSonucu
        {
            public Arac Arac { get; set; }
            public double UygunlukPuani { get; set; }
            public List<string> UygunlukKriterleri { get; set; }
        }

        public class KullaniciTalep
        {
            public string YakitTercihi { get; set; } // "az", "orta", "farketmez"
            public string KasaTipi { get; set; } // "sedan", "suv", "hatchback" vb.
            public int? MinModelYili { get; set; }
            public int? MaxFiyat { get; set; }
            public string VitesTipi { get; set; } // "otomatik", "manuel", "farketmez"
            public string YakitTipi { get; set; } // "benzin", "dizel", "hibrit", "elektrik", "farketmez"
        }

        public List<OneriSonucu> AracOneriGetir(List<Arac> tumAraclar, string kullaniciGirdisi)
        {
            var talep = KullaniciGirdisiniAyristir(kullaniciGirdisi);
            var sonuclar = new List<OneriSonucu>();

            foreach (var arac in tumAraclar.Where(a => a.SatistaMi))
            {
                var puan = 0.0;
                var kriterler = new List<string>();

                // Model yılı kontrolü
                if (talep.MinModelYili.HasValue && arac.ModelYili >= talep.MinModelYili.Value)
                {
                    puan += 20;
                    kriterler.Add($"Model yılı kritere uygun ({arac.ModelYili})");
                }

                // Kasa tipi kontrolü
                if (!string.IsNullOrEmpty(talep.KasaTipi) && 
                    arac.KasaTipi.ToLower().Contains(talep.KasaTipi.ToLower()))
                {
                    puan += 25;
                    kriterler.Add($"İstediğiniz kasa tipinde ({arac.KasaTipi})");
                }

                // Fiyat kontrolü
                if (talep.MaxFiyat.HasValue && arac.Fiyat <= talep.MaxFiyat.Value)
                {
                    puan += 20;
                    kriterler.Add($"Bütçenize uygun (₺{arac.Fiyat:N0})");
                }

                // Yakıt tüketimi kontrolü
                if (!string.IsNullOrEmpty(talep.YakitTercihi))
                {
                    if (talep.YakitTercihi == "az" && arac.YakitTuketimi < 6)
                    {
                        puan += 25;
                        kriterler.Add($"Düşük yakıt tüketimi ({arac.YakitTuketimi:N1} Lt/100km)");
                    }
                    else if (talep.YakitTercihi == "orta" && arac.YakitTuketimi >= 6 && arac.YakitTuketimi <= 9)
                    {
                        puan += 15;
                        kriterler.Add($"Orta düzey yakıt tüketimi ({arac.YakitTuketimi:N1} Lt/100km)");
                    }
                }

                // Vites tipi kontrolü
                if (!string.IsNullOrEmpty(talep.VitesTipi) && talep.VitesTipi != "farketmez" &&
                    arac.VitesTipi.ToLower() == talep.VitesTipi.ToLower())
                {
                    puan += 15;
                    kriterler.Add($"İstediğiniz vites tipinde ({arac.VitesTipi})");
                }

                // Yakıt tipi kontrolü
                if (!string.IsNullOrEmpty(talep.YakitTipi) && talep.YakitTipi != "farketmez" &&
                    arac.YakitTipi.ToLower() == talep.YakitTipi.ToLower())
                {
                    puan += 15;
                    kriterler.Add($"İstediğiniz yakıt tipinde ({arac.YakitTipi})");
                }

                if (puan > 0)
                {
                    sonuclar.Add(new OneriSonucu
                    {
                        Arac = arac,
                        UygunlukPuani = puan,
                        UygunlukKriterleri = kriterler
                    });
                }
            }

            return sonuclar.OrderByDescending(s => s.UygunlukPuani).Take(3).ToList();
        }

        private KullaniciTalep KullaniciGirdisiniAyristir(string girdi)
        {
            var talep = new KullaniciTalep();
            girdi = girdi.ToLower();

            // Yakıt tüketimi analizi
            if (girdi.Contains("az yak") || girdi.Contains("ekonomik"))
                talep.YakitTercihi = "az";
            else if (girdi.Contains("orta yak"))
                talep.YakitTercihi = "orta";

            // Kasa tipi analizi
            if (girdi.Contains("sedan"))
                talep.KasaTipi = "sedan";
            else if (girdi.Contains("suv"))
                talep.KasaTipi = "suv";
            else if (girdi.Contains("hatchback"))
                talep.KasaTipi = "hatchback";

            // Model yılı analizi
            var yillar = girdi.Split(' ')
                .Where(s => s.All(char.IsDigit) && s.Length == 4)
                .Select(int.Parse)
                .ToList();
            if (yillar.Any())
                talep.MinModelYili = yillar.Max();

            // Vites tipi analizi
            if (girdi.Contains("otomatik"))
                talep.VitesTipi = "otomatik";
            else if (girdi.Contains("manuel") || girdi.Contains("düz"))
                talep.VitesTipi = "manuel";
            else
                talep.VitesTipi = "farketmez";

            // Yakıt tipi analizi
            if (girdi.Contains("benzin"))
                talep.YakitTipi = "benzin";
            else if (girdi.Contains("dizel"))
                talep.YakitTipi = "dizel";
            else if (girdi.Contains("hibrit"))
                talep.YakitTipi = "hibrit";
            else if (girdi.Contains("elektrik"))
                talep.YakitTipi = "elektrik";
            else
                talep.YakitTipi = "farketmez";

            return talep;
        }
    }
} 