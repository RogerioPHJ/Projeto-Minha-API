using FS.Comum.Repositorios.Interfaces;
using FS.MinhaApi.AcessoDados.Entity.Context;
using FS.MinhaApi.Dominio;
using FS.MinhaApi.Repositorios.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FS.MinhaApi.Api.Controllers
{
    public class AlunosController : ApiController
    {
        private IRepositorioFS<Aluno, int> _repositorioAlunos
            = new RepositorioAlunos(new MinhaApiDbContext());

        public IHttpActionResult Get()
        {
            return Ok(_repositorioAlunos.Selecionar()); //Responde 200
            
        }

        public IHttpActionResult Get (int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(); // Responde 502
            }
            Aluno aluno = _repositorioAlunos.SelecionarPorId(id.Value);
            if(aluno == null)
            {
                return NotFound(); //Responde 404
            }
            return Content(HttpStatusCode.Found, aluno); //Responde 302 (OK)
        }

        public IHttpActionResult Post([FromBody]Aluno aluno)
        {
            try
            {
                _repositorioAlunos.Inserir(aluno);
                return Created($"{Request.RequestUri}/{aluno.Id}",aluno); //Responde 201 (Criado)
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); //Responde 500 (Internal Server Error)
            }

        }
    }
}
