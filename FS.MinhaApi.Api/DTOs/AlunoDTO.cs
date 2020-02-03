﻿using FS.MinhaApi.Api.HATEOAS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FS.MinhaApi.Api.DTOs
{
    public class AlunoDTO : RestResource
    {
        public int Id { get; set; }

        [Required( ErrorMessage = "O nome do aluno é obrigatorio")]
        [StringLength(maximumLength:20 , MinimumLength = 2, ErrorMessage ="O nome deve conter" +
            "entre 2 e 20 caracteres")]
        public string Nome { get; set; }

        [MaxLength(100, ErrorMessage = "O endereço deve conter até 100 Caracteres")]
        public string Endereco { get; set; }

        [Required (ErrorMessage = "A mensalidade é obrigatorio")]
        [Range(0.01, 9999.99, ErrorMessage = "A mensalidade deve estar entre R$0,01 e R$9.999,99")]
        public decimal Mensalidade { get; set; }
    }
}