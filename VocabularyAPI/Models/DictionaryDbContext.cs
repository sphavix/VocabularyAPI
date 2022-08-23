﻿using Microsoft.EntityFrameworkCore;

namespace VocabularyAPI.Models
{
    public class DictionaryDbContext:DbContext
    {
        public DictionaryDbContext(DbContextOptions<DictionaryDbContext> options) : base(options)
        {

        }

        public DbSet<Word> Words { get; set; }
        public DbSet<Synonym> Synonyms { get; set; }
    }
}