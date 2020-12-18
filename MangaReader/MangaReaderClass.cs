using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangaReader.Parsers;
using MangaReader.Manga;

namespace MangaReader
{
    class MangaReaderClass
    {
        static void Main(string[] args)
        {
            Parser _test0 = new ParserMangaClub();
            TitleLoader _titleLoader = new TitleLoader();
            _titleLoader.LoadTitle("https://readmanga.live/klinok__rassekaiuchii_demonov");
            //Title _title = _test0.GetTitle("https://mangaclub.ru/716-onepunch-man-2.html");
            //_title.InitializeChapter(_title.Chapters[0]);

            _titleLoader.CurrentTitle.Chapters[0].Init();
            _titleLoader.CurrentTitle.Chapters[0].LoadPagesAsync();

            Console.ReadLine();
        }
    }
}
