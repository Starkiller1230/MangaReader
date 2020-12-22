using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MangaReader.Parsers
{
    class ParserReadManga : Parser
    {
        public ParserReadManga()
        {
            // Set title patterns.
            _titleSourcePattern = new Regex(@"<table class=.table table-hover.>[\w\W]+(?=<\/td>\s*<\/tr>)");
            _titleNamePattern = new Regex(@"<span class=.name.>(.+)<\/span>");
            _titleDescriptionPattern = new Regex(@"");

            // Set chapter patterns.
            _chaptersNamePattern = new Regex(@"<a href=.\/\w+\/\w+\/\w+. title=.+class=..>\s+(.+\s+.+\s+)<\/a>");
            _chaptersUrlPattern = new Regex(@"<a href=.(\/\w+\/\w+\/\w+). title=.+class=..>");
            _chaptersPublishDatePattern = new Regex(@"<a href=.\/\w+\/\w+\/\w+. title=.+\s*.*\s*.*\s+<\/a>\s*<.td>\s*.+\s+([0-9]{2,}\.[0-9]{2,}\.[0-9]{2,})");

            // Set page patterns.
            _imageUrlPattern = new Regex(@"");

            _rootUrlChapters = "https://readmanga.live/";
            _rootUrlPages = "https://h23.mangas.rocks/manga/";
            _canParsePublishDate = true;
        }

        public override string ToString() => "ReadManga";

    }
}
