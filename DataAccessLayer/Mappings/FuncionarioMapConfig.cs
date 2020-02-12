using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Mappings
{
    internal class FuncionarioMapConfig : EntityTypeConfiguration<Funcionario>
    {
        public FuncionarioMapConfig()
        {
            this.ToTable("FUNCIONARIOS");
            this.Property(f => f.Nome).HasMaxLength(50).IsRequired().IsUnicode(false);
            this.HasIndex(f => f.Nome).IsUnique();
            this.Property(f => f.Email).HasMaxLength(70).IsRequired().IsUnicode(false);
            this.HasIndex(f => f.Email).IsUnique();
            this.Property(f => f.CPF).IsFixedLength().HasMaxLength(14).IsRequired();
            this.HasIndex(f => f.CPF).IsUnique();
            this.Property(f => f.DataNascimento).HasColumnType("date").IsRequired();
            this.Property(f => f.Senha).HasMaxLength(50).IsRequired().IsUnicode(false);
            this.Property(f => f.Telefone).HasMaxLength(11).IsRequired().IsUnicode(false);
            this.HasIndex(f => f.Telefone).IsUnique();
        }
    }
}
