using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangaReader.Parsers;

namespace MangaReader.Manga
{
    class Title
    {
        public List<Chapter> Chapters { get; private set; } = new List<Chapter>();
        public string Name { get; }
        public string Description { get; }

        private Parser _parser;

        public Title(List<Chapter> chapters, string name, string description, Parser parser)
        {
            Chapters = chapters;

            _parser = parser;
            Description = description == "" ? "Описание отсутствует." : description;
            Name = name;
        }

        public Chapter InitChapter(int chapter) => Chapters[chapter].Init();

        public override string ToString() => $"{Name}\n{Description}";
    }
}
