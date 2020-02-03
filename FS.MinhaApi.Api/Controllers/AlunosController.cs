using FS.Comum.Repositorios.Interfaces;
using FS.MinhaApi.AcessoDados.Entity.Context;
using FS.MinhaApi.Api.AutoMapper;
using FS.MinhaApi.Api.DTOs;
using FS.MinhaApi.Api.Filters;
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
    [Authorize]
    [RoutePrefix("api/alunos")]
    public class AlunosController : ApiController
    {
        private IRepositorioFS<Aluno, int> _repositorioAlunos
            = new RepositorioAlunos(new MinhaApiDbContext());

        public IHttpActionResult Get()
        {
            List<Aluno> alunos = _repositorioAlunos.Selecionar();
            List<AlunoDTO> dtos = AutoMapperManager.Instance.Mapper.Map<List<Aluno>, List<AlunoDTO>>(alunos);
            return Ok(dtos); //Responde 200
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
            AlunoDTO dto = AutoMapperManager.Instance.Mapper.Map<Aluno,AlunoDTO>(aluno);
            return Content(HttpStatusCode.OK    , dto); //Responde 302 (OK)
        }

        [Route("por-nome/{nomeAluno}")]
        public IHttpActionResult Get (string nomeAluno)
        {
            List<Aluno> alunos = _repositorioAlunos.Selecionar(s => s.Nome.ToLower().Contains(nomeAluno.ToLower()));
            List<AlunoDTO> dtos = AutoMapperManager.Instance.Mapper.Map<List<Aluno>, List<AlunoDTO>>(alunos);
            return Ok(dtos);
        }

        [ApplyModelValidation]
        public IHttpActionResult Post([FromBody]AlunoDTO dto)
        {
            try
            {
                Aluno aluno = AutoMapperManager.Instance.Mapper.Map<AlunoDTO, Aluno>(dto);
                _repositorioAlunos.Inserir(aluno);
                return Created($"{Request.RequestUri}/{aluno.Id}",aluno); //Responde 201 (Criado)
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); //Responde 500 (Internal Server Error)
            }
        }

        [ApplyModelValidation]
        public IHttpActionResult Put(int? id, [FromBody]AlunoDTO dto)
        {
            try
            {
                if (!id.HasValue)
                {
                    return BadRequest(); // Responde 502
                }
                Aluno aluno = AutoMapperManager.Instance.Mapper.Map<AlunoDTO, Aluno>(dto);
                aluno.Id = id.Value;
                _repositorioAlunos.Atualizar(aluno);
                return Ok(); //Responde 200
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); //Responde 500 (Internal Server Error)
            }
        }

        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    return BadRequest(); // Responde 502
                }
                Aluno aluno = _repositorioAlunos.SelecionarPorId(id.Value);
                if(aluno == null)
                {
                    return NotFound();
                }
                _repositorioAlunos.ExcluirPorId(id.Value);
                return Ok(); //Responde 200
            }
            catch(Exception ex)
            {
                return InternalServerError(ex); //Responde 500 (Internal Server Error)
            }
        }
    }
}
