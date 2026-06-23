using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BiteArcade.WebAPI.Models;

[Table("Jogo")]
public partial class Jogo
{
    [Key]
    public Guid IdJogo { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Imagem { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Titulo { get; set; } = null!;

    public Guid? IdGenero { get; set; }

    [ForeignKey("IdGenero")]
    [InverseProperty("Jogos")]
    public virtual Genero? IdGeneroNavigation { get; set; }
}
