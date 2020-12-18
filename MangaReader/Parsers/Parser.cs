using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using MangaReader.Manga;

namespace MangaReader.Parsers
{
    /// <summary>
    /// All patterns apply Group 1 exept sourcePatterns.
    /// </summary>
    internal abstract class Parser
    {
        protected Title _currentTitle;
        // Title regex patterns.
        protected Regex _titleSourcePattern = new Regex(@"");
        protected Regex _titleNamePattern = new Regex(@"");
        protected Regex _titleDescriptionPattern = new Regex(@"");
        // Chapter regex patterns.
        protected Regex _chaptersNamePattern = new Regex(@"");
        protected Regex _chaptersUrlPattern = new Regex(@"");
        protected Regex _chaptersPublishDatePattern = new Regex(@"");
        // Image regex patterns.
        protected Regex _imageUrlPattern = new Regex(@"");
        protected bool _canParsePublishDate = false;
        protected bool _usePrefixChapterName = false;
        protected string _prefixChapterName = "Глава: ";
        protected string _rootUrlChapters = "";
        protected string _rootUrlPages = "";

        private static readonly (string sitePattern, Parser parser)[] _supportedSites = {
            (@"^https:..mangaclub.ru\/.+\.html", new ParserMangaClub()),
            (@"^https:..readmanga.live\/\w+", new ParserReadManga())

        };

        public static Parser GetParser(string url)
        {
            for (int i = 0; i < _supportedSites.Length; i++)
            {
                if (Regex.IsMatch(url, _supportedSites[i].sitePattern))
                {
                    Console.WriteLine(_supportedSites[i].parser.ToString());
                    return _supportedSites[i].parser;
                }
            }

            return null;
        }

        public virtual Title GetTitle(string url)
        {
            return ParseTitle(url);
        }

        public virtual List<Page> GetChapterPages(string url)
        {
            return ParseChapter(url);
        }

        protected List<Page> ParseChapter(string url)
        {
            if (Regex.IsMatch(url, @"^(https://)") == false)
                url = "https://" + url;

            List<Page> _pages = new List<Page>();
            WebClient _wc = new WebClient();
            _wc.Encoding = Encoding.UTF8;
            string _page = _wc.DownloadString(url);

            var _matches = _imageUrlPattern.Matches(_page);

            for (int i = 0; i < _matches.Count; i++)
                _pages.Add(new Page($"{_rootUrlPages}{_matches[i].Groups[1].Value}", i));

            return _pages;
        }

        protected Title ParseTitle(string url)
        {
            WebClient _wc = new WebClient();
            _wc.Encoding = Encoding.UTF8;
            string _page = _wc.DownloadString(url);

            // Find title name.
            string _titleName = _titleNamePattern.Match(_page).Groups[1].Value;
            string _descripton = _titleDescriptionPattern.Match(_page).Groups[1].Value;

            // Select source element.
            _page = _titleSourcePattern.Match(_page).Value;

            // Find chapters sourse.
            MatchCollection _names = default, _urls = default, _publishDates = default;
            _names = _chaptersNamePattern.Matches(_page);
            _urls = _chaptersUrlPattern.Matches(_page);
            if(_canParsePublishDate == true)
                _publishDates = _chaptersPublishDatePattern.Matches(_page);

            List<Chapter> _chaptersList = new List<Chapter>();
            List<string> _namesList = new List<string>(), 
                         _urlsList = new List<string>(), 
                         _publishDatesList = new List<string>();

            for (int i = 0; i < _names.Count; i++)
            {
                _namesList.Add(_names[i].Groups[1].Value.Trim());
                _urlsList.Add($"{_rootUrlChapters}{_urls[i].Groups[1].Value}");
                if(_canParsePublishDate == true)
                    _publishDatesList.Add(_publishDates[i].Groups[1].Value);
            }

            bool _checkDatePublish = (_canParsePublishDate == true && _urlsList.Count == _publishDatesList.Count) == 
                                     (_canParsePublishDate == true ? true : false);

            if (_namesList.Count == _urlsList.Count && _checkDatePublish == true)
            {
                for (int i = 0; i < _names.Count - 1; i++)
                {
                    _chaptersList.Add(new Chapter(_urlsList[i + 1],
                                                 (_usePrefixChapterName == true ? _prefixChapterName : "") + _namesList[i + 1],
                                                  _canParsePublishDate == true ? _publishDatesList[i + 1] : ""));
                }
                ClearListFromMatches(_chaptersList);

                foreach (var item in _chaptersList)
                    Console.WriteLine($"{item.Name} {item.PublishDate}");
            }
            else
            {
                throw new Exception("Error. The number of regex matches don't match.");
            }

            Title _parsedTitle = _descripton == "" ? new Title(_chaptersList, _titleName, this) : new Title(_chaptersList, _descripton, _titleName, this);

            Console.WriteLine("Parse title ended.");
            return _parsedTitle;
        }

        private void ClearListFromMatches(List<Chapter> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (list[i] == list[j])
                    {
                        list.RemoveAt(j);
                        break;
                    }
                }
            }


        }

    }

}
