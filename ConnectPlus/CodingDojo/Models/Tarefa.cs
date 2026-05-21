using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CodingDojo.Models;

[Table("Tarefa")]
public partial class Tarefa
{
    [Key]
    public Guid IdTarefa { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Titulo { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Descricao { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DataCriacao { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string StatusConclusao { get; set; } = null!;
}
