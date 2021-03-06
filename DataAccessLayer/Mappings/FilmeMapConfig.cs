﻿using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Mappings
{
    internal class FilmeMapConfig : EntityTypeConfiguration<Filme>
    {
        public FilmeMapConfig()
        {
            
            this.ToTable("FILMES");

            this.Property(f => f.Nome).HasMaxLength(50).IsRequired().IsUnicode(false);
            this.HasIndex(f => f.Nome).IsUnique();
            this.Property(f => f.GeneroID).IsRequired();
            this.Property(f => f.Duracao).IsRequired();
            this.Property(f => f.Classificacao).IsRequired();
            this.Property(f => f.DataLancamento).HasColumnType("date").IsRequired();



        }

    }
}
