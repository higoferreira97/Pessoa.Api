using Pessoa.Dominio;
using System;

namespace Pessoa.Servico.Idade
{
    public class IdadeService
    {
        public IdadeModel CalcularIdade(DateTime dataNascimento)
        {
            var hoje = DateTime.Today; //retorna a data atual sem as horas.
            var idadeInteira = hoje.Year - dataNascimento.Year;
            if (dataNascimento.Date > hoje.AddYears(-idadeInteira)) idadeInteira--;

            var anos = idadeInteira;
            var meses = hoje.Month - dataNascimento.Month;
            if (meses < 0)
            {
                meses += 12;
                anos--;
            }

            var dias = hoje.Day - dataNascimento.Day;
            if (dias < 0)
            {
                meses--;
                dias += DateTime.DaysInMonth(hoje.AddMonths(-1).Year, hoje.AddMonths(-1).Month);
            }

            return new IdadeModel
            {
                IdadeInteira = idadeInteira,
                Anos = anos,
                Meses = meses,
                Dias = dias
            };
        }
    }
}
