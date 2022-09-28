# CRUD

Este artigo demonstrará como desenvolver um CRUD básico da criação do projeto abordando Entity FrameworkCore, Migration a Controller usaremos o banco Postgre. 

CRUD vem do inglês Create Read Update e Delete que em tradução livre para o português seria Criar, 
Ler, Atualizar e Excluir. O CRUD é composto pelas operações básicas que uma aplicação faz a um banco de dados.

O CRUD que será mostrado neste artigo foi desenvolvido de uma forma bem simples para aqueles que realmente estão começando a dar seus primeiros passos, 
mais que já tenham um conhecimento básico em C# e em Postgre, como declarar variáveis e alguns comandos da sintaxe e executar instruções Postgre.

# Criação de um novo projeto

> Visual Studio 2022


![image](https://user-images.githubusercontent.com/99044436/192793818-44e172ae-c38c-40ba-9f46-2094d8f0a062.png)

> Framework

![image](https://user-images.githubusercontent.com/99044436/192794791-cbaef181-8715-47cb-a13c-10d273f2c600.png)

# Manage Nuget Packages

> Npgsql.EntityFrameworkCore.PostgreSQL

> Microsoft.EntityFrameworkCore.Tools

# Program.cs (alteração)

incluir o comando no arquivo Program.cs

```c#
builder.Services.AddDbContext<JogadorDbContext>();

```

# Criação das classes

> Entidade jogador 

```C#
    public class Jogador
    {
        public string Cpf { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public string Sobrenome { get; set; } = null!;
        public DateTime DataNascimento { get; set; }
        public string NomeMae { get; set; } = null!;
    }
```

> Context

```C#
public partial class JogadorDbContext :DbContext
    {

        public JogadorDbContext()
        {

        }

        public JogadorDbContext(DbContextOptions<JogadorDbContext> options)
          : base(options)
        {
        }

        public virtual DbSet<Jogador> Jogadors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=localhost; Database=Cadastro_1; user id=postgres; password=123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jogador>(entity =>
            {
                entity.HasKey(e => e.Cpf)
                    .HasName("pk_cpf_jogador");

                entity.ToTable("jogador");

                entity.Property(e => e.Cpf)
                    .HasColumnType("character varying")
                    .HasColumnName("cpf");

                entity.Property(e => e.DataNascimento).HasColumnName("data_nascimento");

                entity.Property(e => e.Nome)
                    .HasMaxLength(30)
                    .HasColumnName("nome");

                entity.Property(e => e.NomeMae)
                    .HasMaxLength(30)
                    .HasColumnName("nome_mae");

                entity.Property(e => e.Sobrenome)
                    .HasMaxLength(40)
                    .HasColumnName("sobrenome");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

```

# Criando Migrations

> Para acompanhar as mudanças feitas no modelo de dados e manter a sincronia do modelo com o banco de dados o EF Core oferece o 
> recurso Migrations ou Migrações que permite atualizar de forma incremental o esquema do banco de dados e assim mantê-lo 
> sincronizado com o modelo de dados do seu projeto preservando os dados existentes.

# Comandos

> add-migration MigracaoInicial

> update-database

![image](https://user-images.githubusercontent.com/99044436/192802668-0e5a3f82-90e6-4acb-805e-81c7ae06b006.png)

# Criando nossa JogadorController

O que é um Controller C#?
Um controlador determina qual resposta enviar de volta para um usuário quando um usuário faz uma solicitação de navegador

![image](https://user-images.githubusercontent.com/99044436/192806310-dd33473a-f406-4528-a36e-8ee4d889e951.png)

# Definições GET ,POST, PUT e DELETE

GET (Select)
O método GET significa recuperar qualquer informação (na forma de uma entidade)

POST (Insert)
O método POST é usado para solicitar que o servidor de origem que aceite na entidade indicada a inclusão de um novo valor 

PUT (Update)
O método PUT solicita que a entidade indicada a um recurso já existente uma versão modificada daquela que reside no servidor de origem.

DELETE (Delete)
O método DELETE solicita que o servidor de origem exclua o recurso da entidade indicada

# Controller

```c#
 [ApiController]
    [Route("api/[controller]")]
    public class JogadorController : Controller
    {
        private readonly JogadorDbContext _DbContext;

        public JogadorController(JogadorDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _DbContext.Jogadors.ToListAsync());
        }

        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetByCpf(string cpf)
        {
            return Ok(await _DbContext.Jogadors.Where(x => x.Cpf == cpf).FirstOrDefaultAsync());
        }

        [HttpPost]
        public IActionResult Create(Jogador jogador)
        {
            _DbContext.Jogadors.Add(jogador);
            _DbContext.SaveChanges();

            return Ok( new{ Message = "Jogador foi criado!" });
        }

        [HttpPut]
        public IActionResult Updated( Jogador jogador)
        {
            _DbContext.Jogadors.Update(jogador);
            _DbContext.SaveChanges();

            return Ok(new { Message = "Jogador foi alterado!" });
        }

        [HttpDelete]
        public IActionResult Deleted(Jogador jogador)
        {
            _DbContext.Jogadors.Remove(jogador);
            _DbContext.SaveChanges();

            return Ok(new { Message = "Jogador foi deletado" });

        }
   }

```







