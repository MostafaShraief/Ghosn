using Microsoft.AspNetCore.Mvc;
using Ghosn_BLL.Services;
using Ghosn_BLL;

namespace Ghosn.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    [HttpGet("All", Name = "GetAllClients")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<ClientDTO>> GetAllClients()
    {
        var clients = clsClients_BAL.GetAllClients();
        return clients.Count > 0 ? Ok(clients) : NotFound("No clients found.");
    }

    [HttpGet("{id}", Name = "GetClientById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ClientDTO> GetClientById(int id)
    {
        var client = clsClients_BAL.GetClientById(id);
        return client != null ? Ok(client) : NotFound($"Client with ID {id} not found.");
    }

    [HttpPost(Name = "AddClient")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ClientDTO> AddClient([FromBody] ClientDTO newClient)
    {
        if (string.IsNullOrEmpty(newClient.Username) || string.IsNullOrEmpty(newClient.Password))
            return BadRequest("Username and Password are required.");

        int newId = clsClients_BAL.AddClient(newClient);
        newClient.ClientID = newId;
        return CreatedAtRoute("GetClientById", new { id = newId }, newClient);
    }

    [HttpPut("{id}", Name = "UpdateClient")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ClientDTO> UpdateClient(int id, [FromBody] ClientDTO updatedClient)
    {
        if (string.IsNullOrEmpty(updatedClient.Username) || string.IsNullOrEmpty(updatedClient.Password))
            return BadRequest("Username and Password are required.");

        var existingClient = clsClients_BAL.GetClientById(id);
        if (existingClient == null)
            return NotFound($"Client with ID {id} not found.");

        updatedClient.ClientID = id;
        clsClients_BAL.UpdateClient(updatedClient);
        return Ok(updatedClient);
    }

    [HttpDelete("{id}", Name = "DeleteClient")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteClient(int id)
    {
        bool isDeleted = clsClients_BAL.DeleteClient(id);
        return isDeleted
            ? Ok($"Client with ID {id} deleted successfully.")
            : NotFound($"Client with ID {id} not found.");
    }
}