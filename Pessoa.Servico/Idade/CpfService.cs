using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pessoa.Servico.Idade
{
    public class CPFService 
    {
        public bool ValidarCPF(string cpf)
        {
            
            cpf = Regex.Replace(cpf, "[^0-9]", ""); // Remover caracteres especiais


            if (cpf.Length != 11)   // Verifica se o CPF tem 11 dígitos
                return false;

           
            if (new string(cpf[0], cpf.Length) == cpf)  // Verifica se todos os dígitos são iguais
                return false;

            // Calcular os dígitos verificadores
            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            string digito = resto.ToString();

            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
