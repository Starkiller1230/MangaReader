using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangaReader.Parsers;
using MangaReader.Manga;
using System.Text.RegularExpressions;

namespace MangaReader
{
    class TitleLoader
    {
        public Title CurrentTitle { get; set; }
        
        public void LoadTitle(string url)
        {
            Parser _parser = Parser.GetParser(url);

            if(_parser != default)
            {
                CurrentTitle = _parser.GetTitle(url);
                return;
            }
            else
            {
                Console.WriteLine("Unsupported or incorrect site.");
            }

        }



    }
}
