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

        public IEnumerable<Aluno> Get()
        {
            return _repositorioAlunos.Selecionar();
        }

        public HttpResponseMessage Get (int? id)
        {
            if (!id.HasValue)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest); // Responde 502
            }
            Aluno aluno = _repositorioAlunos.SelecionarPorId(id.Value);
            if(aluno == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound); //Responde 404
            }
            return Request.CreateResponse(HttpStatusCode.Found, aluno); //Responde 302 (OK)
        }

        public HttpResponseMessage Post([FromBody]Aluno aluno)
        {
            try
            {
                _repositorioAlunos.Inserir(aluno);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
