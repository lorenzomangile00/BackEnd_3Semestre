using BiteArcade.WebAPI.DTO;
using BiteArcade.WebAPI.Models;
using System;
using System.Collections.Generic;


namespace BiteArcade.WebAPI.Interfaces;

public interface IGeneroRepository
{
    void Cadastrar(Genero novoGenero);
    void AtualizarIdCorpo(Genero generoAtualizado);
    void AtualizarIdUrl(Guid id, Genero generoAtualizado);
    List<Genero> Listar();
    void Deletar(Guid id);
    Genero? BuscarPorId(Guid id);
    void AtualizarIdUrl(Guid id, GeneroDTO generoAtualizado);
}
