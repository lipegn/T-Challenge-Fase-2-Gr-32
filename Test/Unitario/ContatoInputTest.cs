using Core.Entity;
using Core.Input;
using Core.Repository;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Unitario
{
    public class ContatoInputTest
    {
        [Fact]
        public void ContatoInput_DeveRetornarSucesso_QuandoOContatoImputForChamadoComTodosOsDadosCorretamente()
        {
            var contatoInput = new ContatoInput()
                {
                    Nome = "Jõao",
                    DDD = 11,
                    Telefone = "999990000",
                    Email = "joao@gmail.com",
                };

            Assert.Empty(ValidateModel(contatoInput));
        }

        [Fact]
        public void ContatoInput_DeveRetornarErro_QuandoOContatoImputForChamadoSemONome()
        {
            var contatoInput = new ContatoInput()
            {
                Nome = "",
                DDD = 11,
                Telefone = "999990000",
                Email = "joao@gmail.com",
            };

            var contatoInputResultado = ValidateModel(contatoInput);
            Assert.Contains(contatoInputResultado, v => v.ErrorMessage.Equals("Campo nome é obrigatório"));
        }

        [Fact]
        public void ContatoInput_DeveRetornarErro_QuandoOContatoImputForChamadoComONomeMaiorQue100Caracteres()
        {
            var contatoInput = new ContatoInput()
            {
                Nome = "Maximiliano Sebastião de Oliveira e Souza, Conde de Monte Alegre e Barão de São Francisco do Porto Alegre",
                DDD = 11,
                Telefone = "999990000",
                Email = "joao@gmail.com",
            };

            var contatoInputResultado = ValidateModel(contatoInput);
            Assert.Contains(contatoInputResultado, v => v.ErrorMessage.Equals("Campo nome deve ter no máximo 100 caracteres"));
        }

        [Fact]
        public void ContatoInput_DeveRetornarErro_QuandoOContatoImputForChamadoComDDDZero()
        {
            var contatoInput = new ContatoInput()
            {
                Nome = "João",
                DDD = 0,
                Telefone = "999990000",
                Email = "joao@gmail.com",
            };

            var contatoInputResultado = ValidateModel(contatoInput);
            Assert.Contains(contatoInputResultado, v => v.ErrorMessage.Equals("Campo DDD deve estar entre 11 e 99"));
        }

        public void ContatoInput_DeveRetornarErro_QuandoOContatoImputForChamadoSemOTelefone()
        {
            var contatoInput = new ContatoInput()
            {
                Nome = "João",
                DDD = 11,
                Email = "joao@gmail.com",
            };

            var contatoInputResultado = ValidateModel(contatoInput);
            Assert.Contains(contatoInputResultado, v => v.ErrorMessage.Equals("Campo telefone é obrigatório"));
        }

        [Fact]
        public void ContatoInput_DeveRetornarErro_QuandoOContatoImputForChamadoComOTelefoneMenorQue9Caracteres()
        {
            var contatoInput = new ContatoInput()
            {
                Nome = "Maximiliano",
                DDD = 11,
                Telefone = "1234567890",
                Email = "joao@gmail.com",
            };

            var contatoInputResultado = ValidateModel(contatoInput);
            Assert.Contains(contatoInputResultado, v => v.ErrorMessage.Equals("Campo telefone deve ter no máximo 9 caracteres"));
        }

        [Fact]
        public void ContatoInput_DeveRetornarErro_QuandoOContatoImputForChamadoComOEmailVazio()
        {
            var contatoInput = new ContatoInput()
            {
                Nome = "João",
                DDD = 11,
                Telefone = "999990000",
            };

            var contatoInputResultado = ValidateModel(contatoInput);
            Assert.Contains(contatoInputResultado, v => v.ErrorMessage.Equals("Campo e-mail é obrigatório"));
        }

        [Fact]
        public void ContatoInput_DeveRetornarErro_QuandoOContatoImputForChamadoComOEmailInvalido()
        {
            var contatoInput = new ContatoInput()
            {
                Nome = "João",
                DDD = 11,
                Telefone = "999990000",
                Email = "joão.com.br"
            };

            var contatoInputResultado = ValidateModel(contatoInput);
            Assert.Contains(contatoInputResultado, v => v.ErrorMessage.Equals("Campo e-mail inválido"));
        }




        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
