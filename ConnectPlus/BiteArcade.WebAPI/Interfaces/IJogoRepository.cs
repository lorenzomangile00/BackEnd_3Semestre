
using System;
using System.Collections.Generic;
using BiteArcade.WebAPI.Models;

namespace BiteArcade.WebAPI.Interfaces;

    public interface IJogoRepository
    {
        void Cadastrar(Jogo novoJogo);

        void AtualizarIdCorpo(Jogo jogoAtualizado);

        void AtualizarIdUrl(Guid id, Jogo jogoAtualizado);

        List<Jogo> Listar();

        void Deletar(Guid id);

        Jogo BuscarPorId(Guid id);
    }

