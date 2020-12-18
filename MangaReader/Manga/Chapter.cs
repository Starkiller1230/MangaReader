using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using MangaReader.Parsers;

namespace MangaReader.Manga
{
    class Chapter
    {
        public List<Page> Pages { get; private set; } = new List<Page>();
        public string PublishDate { get; }
        public string Name { get; }
        public string Url { get; }

        private List<string> _pagesUrl = new List<string>();

        public Chapter(string chapterUrl, string name, string publishDate)
        {
            Name = name;
            Url = chapterUrl;
            PublishDate = publishDate;
        }

        public Chapter(string chapterUrl, string name)
        {
            Name = name;
            Url = chapterUrl;
            PublishDate = "";
        }

        public Chapter Init()
        {
            Parser _parser = Parser.GetParser(Url);
            if (_parser != null)
                Pages = _parser.GetChapterPages(Url);

            return this;
        }

        public async void LoadPagesAsync()
        {
            await Task.Run(() => 
            {
                WebClient _wc = new WebClient();
                for (int i = 0; i < Pages.Count; i++)
                    Pages[i].LoadImage();

            });

            Console.WriteLine("Loading pages is complete.");
        }

    }
}
