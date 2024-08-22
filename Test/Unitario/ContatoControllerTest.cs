using Core.Entity;
using Core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallangeCadastroContatosAPI.Controllers;

namespace Test.Unitario
{
    public class ContatoControllerTest
    {
        [Fact]
        public void Get_DeveReotrnar200ComUmaListaVazia_QuandoRepositoryDevolverVazio()
        {
            var mockContatoRepository = new Mock<IContatoRepository>();
            mockContatoRepository.Setup(repository => repository.ObterTodos()).Returns(new List<Contato>());

            var contatoController = new ContatoController(mockContatoRepository.Object);

            var resultado = Assert.IsType<OkObjectResult>(contatoController.Get());
            Assert.NotNull(resultado);
            Assert.Equal(StatusCodes.Status200OK, resultado.StatusCode);
            var valor = Assert.IsType<List<Contato>>(resultado.Value);
            Assert.Empty(valor);
        }

        [Fact]
        public void Get_DeveReotrnarBadRequest_QuandoDerUmaExeção()
        {
            var mockContatoRepository = new Mock<IContatoRepository>();
            mockContatoRepository.Setup(repository => repository.ObterTodos()).Throws<System.IO.IOException>();


            var contatoController = new ContatoController(mockContatoRepository.Object);

            var resultado = Assert.IsType<BadRequestObjectResult>(contatoController.Get());
            Assert.Equal(StatusCodes.Status400BadRequest, resultado.StatusCode);

        }
        [Fact]
        public void GetDDD_DeveReotrnar200_QuandoRepositoryForChamadoObterPorDD()
        {
            //var contatoRetorno = new Contato()
            //{
            //    Nome = "Jõao",
            //    DDD = 11,
            //    Telefone = 999990000,
            //    Email = "joao@gmail.com",
            //};
            //var contatoErro = new Contato()
            //{
            //    Nome = "Maria",
            //    DDD = 12,
            //    Telefone = 999990001,
            //    Email = "Maria@gmail.com",
            //};

            var mockContatoRepository = new Mock<IContatoRepository>();
            mockContatoRepository.Setup(repository => repository.ObterTodos()).Returns(new List<Contato>());

            var contatoController = new ContatoController(mockContatoRepository.Object);
            var resultado = Assert.IsType<OkObjectResult>(contatoController.GetPorDDD(11));
            Assert.NotNull(resultado);
            //Assert.Equal(StatusCodes.Status200OK, resultado.StatusCode);
            //var resultadoContato = Assert.IsType<List<Contato>>(contatoController.GetPorDDD(11));
            //Assert.Equal(contatoRetorno.Nome, contatoRetornado[0].Nome);
            //Assert.Equal(contatoRetorno.DDD, contatoRetornado[0].DDD);
            //Assert.Equal(contatoRetorno.Telefone, contatoRetornado[0].Telefone);
            //Assert.Equal(contatoRetorno.Email, contatoRetornado[0].Email);
        }

    }
}
