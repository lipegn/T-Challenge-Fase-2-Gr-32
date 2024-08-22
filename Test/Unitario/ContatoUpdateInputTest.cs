using Core.Input;
using System.ComponentModel.DataAnnotations;

namespace Test.Unitario
{
    public class ContatoUpdateInputTest
    {

        [Fact]
        public void ContatoUpdateInput_DeveRetornarSucesso_QuandoOContatoUpdateImputForChamadoComTodosOsDadosCorretamente()
        {
            var contatoInput = new ContatoUpdateInput()
            {
                Id = 1,
                Nome = "Jõao",
                DDD = 11,
                Telefone = "999990000",
                Email = "joao@gmail.com",
            };

            Assert.Empty(ValidateModel(contatoInput));
        }

        [Fact]
        public void ContatoUpdateInput_DeveRetornarErro_QuandoOContatoUpdateImputForChamadoSemONome()
        {
            var contatoInput = new ContatoUpdateInput()
            {
                Id = 1,
                Nome = "",
                DDD = 11,
                Telefone = "999990000",
                Email = "joao@gmail.com",
            };

            var contatoInputResultado = ValidateModel(contatoInput);
            Assert.Contains(contatoInputResultado, v => v.ErrorMessage.Equals("Campo nome é obrigatório"));
        }

        [Fact]
        public void ContatoUpdateInput_DeveRetornarErro_QuandoOContatoUpdateImputForChamadoComONomeMaiorQue100Caracteres()
        {
            var contatoInput = new ContatoUpdateInput()
            {
                Id = 1,
                Nome = "Maximiliano Sebastião de Oliveira e Souza, Conde de Monte Alegre e Barão de São Francisco do Porto Alegre",
                DDD = 11,
                Telefone = "999990000",
                Email = "joao@gmail.com",
            };

            var contatoInputResultado = ValidateModel(contatoInput);
            Assert.Contains(contatoInputResultado, v => v.ErrorMessage.Equals("Campo nome deve ter no máximo 100 caracteres"));
        }

        [Fact]
        public void ContatoUpdateInput_DeveRetornarErro_QuandoOContatoUpdateImputForChamadoComDDDZero()
        {
            var contatoInput = new ContatoUpdateInput()
            {
                Id= 1,
                Nome = "João",
                DDD = 0,
                Telefone = "999990000",
                Email = "joao@gmail.com",
            };

            var contatoInputResultado = ValidateModel(contatoInput);
            Assert.Contains(contatoInputResultado, v => v.ErrorMessage.Equals("Campo DDD deve estar entre 11 e 99"));
        }
        
        [Fact]
        public void ContatoUpdateInput_DeveRetornarErro_QuandoOContatoUpdateImputForChamadoSemOTelefone()
        {
            var contatoInput = new ContatoUpdateInput()
            {
                Id= 1,
                Nome = "João",
                DDD = 11,
                Email = "joao@gmail.com",
            };

            var contatoInputResultado = ValidateModel(contatoInput);
            Assert.Contains(contatoInputResultado, v => v.ErrorMessage.Equals("Campo telefone é obrigatório"));
        }

        [Fact]
        public void ContatoUpdateInput_DeveRetornarErro_QuandoOContatoUpdateImputForChamadoComOTelefoneMenorQue9Caracteres()
        {
            var contatoInput = new ContatoUpdateInput()
            {
                Id= 1,
                Nome = "Maximiliano",
                DDD = 11,
                Telefone = "1234567890",
                Email = "joao@gmail.com",
            };

            var contatoInputResultado = ValidateModel(contatoInput);
            Assert.Contains(contatoInputResultado, v => v.ErrorMessage.Equals("Campo telefone deve ter no máximo 9 caracteres"));
        }

        [Fact]
        public void ContatoUpdateInput_DeveRetornarErro_QuandoOContatoUpdateImputForChamadoComOEmailVazio()
        {
            var contatoInput = new ContatoUpdateInput()
            {
                Id = 1,
                Nome = "João",
                DDD = 11,
                Telefone = "999990000",
            };

            var contatoInputResultado = ValidateModel(contatoInput);
            Assert.Contains(contatoInputResultado, v => v.ErrorMessage.Equals("Campo e-mail é obrigatório"));
        }

        [Fact]
        public void ContatoUpdateInput_DeveRetornarErro_QuandoOContatoUpdateImputForChamadoComOEmailInvalido()
        {
            var contatoInput = new ContatoUpdateInput()
            {
                Id = 1,
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
