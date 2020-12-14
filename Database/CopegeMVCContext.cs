using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CopegeMVC.Database
{
    public class CopegeMVCContext : DbContext
    {
        public CopegeMVCContext(DbContextOptions<CopegeMVCContext> options) : base (options)
        {
        }

        //public DbSet<NewsletterEmail> NewsletterEmail { get; set; }
        //public DbSet<Cliente> Clientes { get; set; }
        //public DbSet<Colaborador> Colaboradores { get; set; }
        //public DbSet<Categoria> Categorias { get; set; }
        //public DbSet<Pacote> Pacotes { get; set; }
        //public DbSet<Imagem> Imagens { get; set; }
        //public DbSet<ServicoPacote> ServicosPacotes { get; set; }
        //public DbSet<MensagemMotivacional> MensagensMotivacionais { get; set; }
        //public DbSet<Servico> Servicos { get; set; }
        //public DbSet<ImagemServico> ImagensServicos { get; set; }
        //public DbSet<Testemunho> Testemunhos { get; set; }
        //public DbSet<ImagemTestemunho> ImagensTestemunhos { get; set; }
        //public DbSet<LinkTree> LinkTree { get; set; }
        //public DbSet<Documento> Documentos { get; set; }
    }
}
