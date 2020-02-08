using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Security
{
    public static class SenhaValidator
    {
        public static Response ValidateSenha(string senha)
        {
            Response response = new Response();


            if (string.IsNullOrWhiteSpace(senha))
            {
                response.Erros.Add("Senha deve ser informada");
            }
            if (senha.Length < 8)
            {
                response.Erros.Add("Senha deve conter pelo menos 8 caracteres.");
            }

            //Verificar se a senha possui ao menos 3 letras (1 maiúscula, 1 minúscula)
            //3 numeros e 1 símbolo

            int qtdMaiuscula = 0;
            int qtdMinuscula = 0;
            int qtdNumeros = 0;
            int qtdSimbolos = 0;

            senha = senha.Replace(" ", "");

            foreach (char caractere in senha)
            {
                if (char.IsLetter(caractere))
                {
                    if (char.IsUpper(caractere))
                    {
                        qtdMaiuscula++;
                    }
                    else
                    {
                        qtdMinuscula++;
                    }
                }
                else if (char.IsNumber(caractere))
                {
                    qtdNumeros++;
                }
                else
                {
                    qtdSimbolos++;
                }
            }

            int qtdLetras = qtdMaiuscula + qtdMinuscula;

            if (qtdLetras < 3 || qtdMinuscula < 1 || qtdMaiuscula < 1 || qtdNumeros < 3 || qtdSimbolos < 1)
            {
                response.Erros.Add("A senha deve conter ao menos 3 letras (1 maiúscula e 1 minúscula), 3 números e 1 símbolo"); 
            }

            response.Sucesso = !(response.HasErrors());

            return response;
        }

    }
}
