using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace monitoringdonatmusic
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                bool exit = false;
                Console.WriteLine("1. Allan");
                Console.WriteLine("2. Freelancer");
                Console.WriteLine("3. Joker");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        exit = true;
                        Console.Clear();
                        MonitorDonatepay();
                        break;
                    case '2':
                        exit = true;
                        Console.Clear();
                        MonitorDonatelall("13792");  //free 13792 + joker 14927
                        break;
                    case '3':
                        exit = true;
                        Console.Clear();
                        MonitorDonatelall("14927");  //free 13792 + joker 14927
                        break;
                    default:
                        Console.Clear();
                        break;
                }
                if (exit) { break; }
            }
            Task.Delay(-1).Wait();
        }
        public static void test()
        {
            string path1 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\donate\\1.json";
            string path2 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\donate\\2.json";
            string path3 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\donate\\3.json";
            string respath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\donate\\donateall.json";

            List<DonatelAll> list1 = DonatelAll.DeserializeList(File.ReadAllText(path1));
            List<DonatelAll> list2 = DonatelAll.DeserializeList(File.ReadAllText(path2));
            List<DonatelAll> list3 = DonatelAll.DeserializeList(File.ReadAllText(path3));

            List<DonatelAll> res = new List<DonatelAll>();
            res.AddRange(list1);
            res.AddRange(list2);
            res.AddRange(list3);

            File.WriteAllText(respath, JsonConvert.SerializeObject(res, Formatting.Indented));
            int index = 1;
            foreach (var i in res)
            {
                Console.WriteLine($"{index}. " + i.current.name);
                index++;
            }
            Console.ReadKey();
        }
        public static async void MonitorDonatepay() //--allan
        {
            /*await Task.Run(() =>
            {
                int index = 1;
                //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\donateall_{DateTime.Now.ToString("dd.MM.yyyy_HH-mm-ss")}.json";
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\donate\\donatepayallan.json";
                List<DonatePay> list = DonatePay.Convert(DonatePay.Deserialize(GetJsonDonatepay()));
                try
                {
                    list = DonatePay.Deserialize(File.ReadAllText(path));
                    foreach (var i in list)
                    {
                        Console.WriteLine($"{index}. " + i.name);
                        index++;
                    }
                }
                catch { }
                List<DonatePay> maindp = DonatePay.Deserialize(GetJsonDonatelall(GetJsonDonatepay()));
                while (true)
                {
                    try
                    {
                        if (maindp != null) { break; }
                        else { System.Threading.Thread.Sleep(10000); maindp = DonatePay.Deserialize(GetJsonDonatepay()); }
                    }
                    catch { System.Threading.Thread.Sleep(10000); }
                }
                bool find = false;
                for (int i = 0; i < list.Count; i++) { if (maindp. == list[i].current.url) { find = true; break; } }
                Console.WriteLine($"{index}. " + maindp.current.name + $" Streamer?: {maindp.current.is_streamer_playlist} Exist:?{find}");
                list.Add(maindp);
                find = false;
                File.WriteAllText(path, JsonConvert.SerializeObject(list, Formatting.Indented));
                while (true)
                {
                    System.Threading.Thread.Sleep(20000);
                    DonatePay tmp = DonatePay.Deserialize(GetJsonDonatepay());
                    while (true)
                    {
                        try
                        {
                            if (tmp != null) { break; }
                            else { System.Threading.Thread.Sleep(20000); tmp = DonatePay.Deserialize(GetJsonDonatelall(id)); }
                        }
                        catch { System.Threading.Thread.Sleep(20000); }
                    }
                    if (!DonatePay.Equal(maindp, tmp))
                    {
                        maindp = tmp;
                        index++;
                        for (int i = 0; i < list.Count; i++) { if (maindp.current.url == list[i].current.url) { find = true; break; } }
                        Console.WriteLine($"{index}. " + maindp.current.name + $" Streamer?: {maindp.current.is_streamer_playlist} Exist:?{find}");
                        list.Add(maindp);
                        find = false;
                        File.WriteAllText(path, JsonConvert.SerializeObject(list, Formatting.Indented));
                    }
                }
                //Console.WriteLine(json);
                //Console.Clear();
            });*/
        }
        public static async void MonitorDonatelall(string id)
        {
            await Task.Run(() =>
            {
                int index = 1;
                //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\donateall_{DateTime.Now.ToString("dd.MM.yyyy_HH-mm-ss")}.json";
                string path = id == "14927" ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\donate\\donatealljoker.json" : Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\donate\\donateallfreelancer.json";
                List<DonatelAll> list = new List<DonatelAll>();
                try
                {
                    list = DonatelAll.DeserializeList(File.ReadAllText(path));
                    foreach (var i in list)
                    {
                        Console.WriteLine($"{index}. " + i.current.name + $" S?: {i.current.is_streamer_playlist}");
                        index++;
                    }
                }
                catch { }
                DonatelAll maindp = DonatelAll.Deserialize(GetJsonDonatelall(id));
                while (true)
                {
                    try
                    {
                        if (maindp != null) { break; }
                        else { System.Threading.Thread.Sleep(10000); maindp = DonatelAll.Deserialize(GetJsonDonatelall(id)); }
                    }
                    catch { System.Threading.Thread.Sleep(10000); }
                }
                bool find = false;
                for (int i = 0; i < list.Count; i++) { if (maindp.current.url == list[i].current.url) { find = true; break; } }
                Console.WriteLine($"{index}. " + maindp.current.name + $" Streamer?: {maindp.current.is_streamer_playlist} Exist:?{find}");
                list.Add(maindp);
                find = false;
                File.WriteAllText(path, JsonConvert.SerializeObject(list, Formatting.Indented));
                while (true)
                {
                    System.Threading.Thread.Sleep(24000);
                    DonatelAll tmp = DonatelAll.Deserialize(GetJsonDonatelall(id));
                    while (true)
                    {
                        try
                        {
                            if (tmp != null) { break; }
                            else { System.Threading.Thread.Sleep(20000); tmp = DonatelAll.Deserialize(GetJsonDonatelall(id)); }
                        }
                        catch { System.Threading.Thread.Sleep(24000); }
                    }
                    if (!DonatelAll.Equal(maindp, tmp))
                    {
                        maindp = tmp;
                        index++;
                        for (int i = 0; i < list.Count; i++) { if (maindp.current.url == list[i].current.url) { find = true; break; } }
                        Console.WriteLine($"{index}. " + maindp.current.name + $" Streamer?: {maindp.current.is_streamer_playlist} Exist:?{find}");
                        list.Add(maindp);
                        find = false;
                        File.WriteAllText(path, JsonConvert.SerializeObject(list, Formatting.Indented));
                    }
                }
                //Console.WriteLine(json);
                //Console.Clear();
            });
        }

        public static string GetJsonDonatelall(string iduser) //--lancer
        {
            var url = $"https://donateall.online/add-song/getUserParameters?userId={iduser}";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";
            httpRequest.Headers.Add("Cookie", "_ga=GA1.1.322119151.1659553306; _ga_9MVYYSDGDV=GS1.1.1659553306.1.1.1659553306.0");
            httpRequest.Accept = "application/json, text/plain, */*";
            httpRequest.Headers.Add("Sec-Ch-Ua", "\" Not A; Brand\";v=\"99\", \"Chromium\";v=\"96\"");
            httpRequest.Headers.Add("Sec-Ch-Ua-Mobile", $"{(char)63}0");
            httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36";
            httpRequest.Headers.Add("Sec-Ch-Ua-Platform", "\"Windows\"");
            httpRequest.Headers.Add("Sec-Fetch-Site", "same-origin");
            httpRequest.Headers.Add("Sec-Fetch-Mode", "cors");
            httpRequest.Headers.Add("Sec-Fetch-Dest", "empty");
            httpRequest.Headers.Add("Referer", "https://donateall.online/");
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) { return streamReader.ReadToEnd(); }
        }
        public static string GetJsonDonatepay()
            => new WebClient().DownloadString("https://donatepay.ru/api/v2/streamers/114302/playlists");

        public class DonatelAll
        {
            public class Current
            {
                public string name = "";
                public string url = "";
                public bool is_streamer_playlist = false;
            }
            public class Next
            {
                public string name = "";
                public string url = "";
                public bool is_streamer_playlist = false;
            }
            public bool isMusicEnabled = false;
            public bool isPaymentFixed = false;
            public int minPayment = 0;
            public int maxPayment = 0;
            public int minWatches = 0;
            public int likesPercent = 0;
            public int maxLength = 0;
            public bool allowUnskippable = false;
            public int unskippablePrice = 0;
            public Current current;
            public Next[] next;
            public static List<DonatelAll> DeserializeList(string json)
            {
                try { return JsonConvert.DeserializeObject<List<DonatelAll>>(json); }
                catch { }
                return null;
            }
            public static DonatelAll Deserialize(string json)
            {
                try { return JsonConvert.DeserializeObject<DonatelAll>(json); }
                catch { }
                return null;
            }
            public static bool Equal(DonatelAll da1, DonatelAll da2)
            {
                if ((da1 == null) && (da2 != null) || (da1 != null) && (da2 == null)) { return false; }
                if ((da1 == null) && (da2 == null)) { return true; }
                if (((da1.current.url == "")&&(da2.current.url != "")) || ((da2.current.url == "")&&(da1.current.url != ""))) { return true; }
                return (da1.current.name == da2.current.name) &&(da1.current.url == da2.current.url)&&(da1.current.is_streamer_playlist == da2.current.is_streamer_playlist);
            }
        }

        public class DonatePay
        {
            public class Name
            {
                public string name = "";
            }
            public long id = 0;
            public string videoId = "";
            public string title = "";
            public Name name;
            public static List<DonatePay> Deserialize(string json)
            {
                try { return JsonConvert.DeserializeObject<List<DonatePay>>(json); }
                catch { }
                return null;
            }
            public static List<DonatePay> Convert(List<DonatePay> ldp)
            {
                List<DonatePay> result = new List<DonatePay>();
                for (int i = 0; i < ldp.Count; i++) { result.Add(new DonatePay { id = ldp[i].id, name = ldp[i].name, title = ldp[i].title, videoId = "https://www.youtube.com/watch?v=" + ldp[i].videoId }); }
                return result;
            }
            public static bool Equal(DonatePay dp1, DonatePay dp2)
            {
                if ((dp1 == null) && (dp2 != null) || (dp1 != null) && (dp2 == null)) { return false; }
                if ((dp1 == null) && (dp2 == null)) { return true; }
                if (((dp1.videoId == "") && (dp2.videoId != "")) || ((dp2.videoId == "") && (dp1.videoId != ""))) { return true; }
                return (dp1.name == dp2.name) && (dp1.videoId == dp2.videoId);
            }
        }
    }
}
