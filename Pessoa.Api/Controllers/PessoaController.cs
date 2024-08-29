using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Pessoa.Dominio;
using Pessoa.Servico.Idade;
using System;
using System.ComponentModel.DataAnnotations;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute; //Necessário para usar as rotas

namespace Pessoa.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IdadeService _idadeService;

        public PessoaController()
        {
            _idadeService = new IdadeService();
        }

        [HttpGet(), Route("validarIdade")]
        public ActionResult<IdadeModel> ValidarIdade([FromQuery] string dataNascimento)
        {
            if (DateTime.TryParseExact(dataNascimento, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dataNasc))
            {
                var idade = _idadeService.CalcularIdade(dataNasc);
                return Ok(idade);
            }

            return BadRequest("Data de nascimento inválida. Use o formato dd/MM/yyyy.");

        }




        [HttpGet(), Route("validar - cpf")]
        public IActionResult ValidarCPF([FromQuery] string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return BadRequest("CPF não pode ser vazio.");

            var cpfService = new CPFService(); // Instanciando diretamente o serviço
            bool cpfValido = cpfService.ValidarCPF(cpf);

            if (cpfValido)
            {
                return Ok(new { Mensagem = "CPF válido" });
            }

            return BadRequest(new { Mensagem = "CPF inválido" });

        }


        [HttpGet(), Route("validar cpf e idade")]
        public IActionResult ValidarCPFIdade([FromQuery] string cpf, [FromQuery] string dataNascimento) 
        {
            if (string.IsNullOrEmpty(cpf))
                return BadRequest("CPF não pode ser vazio.");

            var cpfService = new CPFService(); // Instanciando diretamente o serviço
            bool cpfValido = cpfService.ValidarCPF(cpf);

            if (!cpfValido)
                return BadRequest(new { Mensagem = "CPF inválido" });

            if (!DateTime.TryParseExact(dataNascimento, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dataNasc))
                return BadRequest("Data de nascimento inválida. Use o formato dd/MM/yyyy.");

            var idade = _idadeService.CalcularIdade(dataNasc);

            if (idade.IdadeInteira >= 18)
            {
                return Ok(new { Mensagem = "CPF válido e a pessoa é maior de 18 anos." });
            }

            else
            {
                return BadRequest(new { Mensagem = "A pessoa é menor de 18 anos." });
            }

        }


    }
}


// Pessoa.Api: PessoaController expõe o endpoint e usa o serviço para calcular a idade.
// Pessoa.Dominio: Define o modelo Idade.
// Pessoa.Servico: Implementa a lógica de cálculo da idade no IdadeService.
