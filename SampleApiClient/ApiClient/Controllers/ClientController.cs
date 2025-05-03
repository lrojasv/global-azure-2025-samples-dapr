using ApiClient.Dtos;
using Dapr.Client;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly DaprClient _daprClient;

        public ClientController(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        // GET: api/<ClientController>
        [HttpGet("{dni}")]
        public async Task<IActionResult> Get(string dni)
        {
            var result = await _daprClient.GetStateAsync<ClientDto>("azure-table-storage-client", dni);
            return new OkObjectResult(result);
        }

        // POST api/<ClientController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientDto client)
        {
            await _daprClient.SaveStateAsync<ClientDto>("azure-table-storage-client", client.dni,client);
            return Ok();
        }

        // PUT api/<ClientController>/5
        [HttpPut("{dni}")]
        public async Task<IActionResult> Put(string dni, [FromBody] ClientDto client)
        {
            await _daprClient.SaveStateAsync<ClientDto>("azure-table-storage-client", dni, client);
            return Ok();
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{dni}")]
        public async Task<IActionResult> Delete(string dni)
        {
            await _daprClient.DeleteStateAsync("azure-table-storage-client", dni);
            return Ok();
        }
    }
}
