using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MangaReader.Manga
{
    class Page
    {
        public Image Image { get; private set; }
        public string Url { get; }
        public int PageNumber { get; }

        public Page(string url, int number)
        {
            Url = url;
            PageNumber = number;
        }

        public void LoadImage()
        {
            try
            {
                WebClient _wc = new WebClient();
                MemoryStream _stream = new MemoryStream(_wc.DownloadData(Url));
                Image _temp = Image.FromStream(_stream);
                Image = _temp;
                Console.WriteLine($"Page '{PageNumber}' - loaded.");
            }
            catch
            {
                throw new Exception("Download fail.");
            }
            

        }

        public void SetImage(Image image) => Image = image;
    }
}
