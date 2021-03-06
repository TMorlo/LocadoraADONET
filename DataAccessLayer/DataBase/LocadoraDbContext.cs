﻿using Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;

namespace DataAccessLayer
{
    public class LocadoraDbContext : DbContext
    {
        //Construtor padrão da classe que, quando invocado, chama o construtor da classe pai
        //que inicializa a connectionstring que contém as informações da base que iremos trabalhar

        public LocadoraDbContext() : base(SqlData.ConnectionString)
        {
           Database.SetInitializer(new XxxLocadoraTesteStrategy());
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());


            modelBuilder.Entity<Locacao>()
            .HasMany<Filme>(x => x.Filmes)
            .WithMany(y => y.Locacao).Map(z =>
            {
                z.MapLeftKey("LocacaoID");
                z.MapRightKey("FilmesID");
                z.ToTable("FILMES_LOACACOES");
            });

            modelBuilder.Entity<Locacao>()
            .HasRequired<Cliente>(s => s.Cliente)
            .WithMany(g => g.Locacao)
            .HasForeignKey<int>(s => s.ClienteID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
