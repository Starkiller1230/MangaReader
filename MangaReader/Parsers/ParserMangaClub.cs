using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MangaReader.Parsers;

namespace MangaReader.Parsers
{
    class ParserMangaClub : Parser
    {
        public ParserMangaClub()
        {
            // Set title patterns.
            _titleSourcePattern = new Regex(@"<div class=.manga-ch-list.>[\w\W]*manga-list-body-com.>");
            _titleNamePattern = new Regex(@".h1 class=.title.>(.+)<.h1>");
            _titleDescriptionPattern = new Regex(@"<div class=.description_manga.>(.+)((<\/p><\/div>)|(<\/div>))");

            // Set chapter patterns.
            _chaptersNamePattern = new Regex(@"https:..mangaclub.ru.manga.+\.html.+.>(.+)<\/a>");
            _chaptersUrlPattern = new Regex(@"(https:..mangaclub.ru.manga.+\.html)");
            _chaptersPublishDatePattern = new Regex(@"<div class=.chapter-date.>(.+)<\/div>");

            // Set page patterns.
            _imageUrlPattern = new Regex(@"data-i=.(https:\/\/img.mangaclub.ru\/[\w\W].+.jpg).>[0-9]{1,}<\/a>");

            _canParsePublishDate = true;
        }

        public override string ToString() => "MangaClub";
    }
}
