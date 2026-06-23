using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace BiteArcade.WebAPI.Models;

[Table("Genero")]
public partial class Genero
{
    [Key]
    public Guid IdGenero { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [JsonIgnore]
    [InverseProperty("IdGeneroNavigation")]
    public virtual ICollection<Jogo> Jogos { get; set; } = new List<Jogo>();
}
