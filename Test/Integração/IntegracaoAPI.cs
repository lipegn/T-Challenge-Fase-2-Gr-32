using Core.Entity;
using Core.Input;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using TechChallangeCadastroContatosAPI.Controllers;

namespace Test.Integração
{
    public class IntegracaoAPI
    {
        HttpClient client = new();
        const string URL_API = "https://localhost:7259/";

        async Task<string> BuscarToken()
        {
            HttpClient client = new();
            var parametro = new LoginInput() { Usuario = "usuario-fiap", Senha = "senha-fiap" };
            var payload = JsonSerializer.Serialize(parametro);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{URL_API}Login", content);
            if (response.IsSuccessStatusCode)
            {
                string resultContent = await response.Content.ReadAsStringAsync();
                return resultContent;
            }
            else return string.Empty;
        }

        [Fact]
        public async void Deve_Realizar_Login_na_API()
        {
            var token = await BuscarToken();
            Assert.NotEmpty(token);
        }

        [Fact]
        public async void Deve_Buscar_Contatos_na_API()
        {            
            string token = await BuscarToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync($"{URL_API}Contato");
            Assert.True(response.IsSuccessStatusCode);

            if (response.IsSuccessStatusCode)
            {
                string resultContent = await response.Content.ReadAsStringAsync();
                var resultObject = JsonSerializer.Deserialize<IList<Contato>>(resultContent);
                Assert.IsType<List<Contato>>(resultObject);
            }
        }
    }
}
