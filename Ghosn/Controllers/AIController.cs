using Microsoft.AspNetCore.Mvc;
using Ghosn_BLL.Services;
using Ghosn_BLL;

namespace Ghosn.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    [HttpGet("AllClients", Name = "GetAllClients")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<ClientDTO>> GetAllClients()
    {
        var clients = clsClients_BAL.GetAllClients();
        return clients.Count > 0 ? Ok(clients) : NotFound("No clients found.");
    }

    [HttpGet("Client/{id}", Name = "GetClientById")]
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

    [HttpPut("Client/{id}", Name = "UpdateClient")]
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

    [HttpDelete("Client/{id}", Name = "DeleteClient")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteClient(int id)
    {
        bool isDeleted = clsClients_BAL.DeleteClient(id);
        return isDeleted
            ? Ok($"Client with ID {id} deleted successfully.")
            : NotFound($"Client with ID {id} not found.");
    }

    [HttpGet("AllInputs", Name = "GetAllInputsWithPlants")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<InputResponseDTO>> GetAllInputsWithPlants()
    {
        var inputs = clsInputs_BLL.GetAllInputsWithPlants();
        return inputs.Count > 0 ? Ok(inputs) : NotFound("No inputs found.");
    }

    [HttpGet("Input/{id}", Name = "GetInputWithPlantsById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<InputResponseDTO> GetInputWithPlantsById(int id)
    {
        var input = clsInputs_BLL.GetInputWithPlantsById(id);
        return input != null ? Ok(input) : NotFound($"Input with ID {id} not found.");
    }

    [HttpPost("Input", Name = "AddInputWithPlants")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<InputResponseDTO> AddInputWithPlants([FromBody] InputRequestDTO newInput)
    {
        if (newInput == null)
            return BadRequest("Input data is required.");

        int newId = clsInputs_BLL.AddInputWithPlants(newInput);
        newInput.InputID = newId;
        return CreatedAtRoute("GetInputWithPlantsById", new { id = newId }, newInput);
    }

    [HttpPut("Input/{id}", Name = "UpdateInputWithPlants")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<InputResponseDTO> UpdateInputWithPlants(int id, [FromBody] InputRequestDTO updatedInput)
    {
        if (updatedInput == null)
            return BadRequest("Input data is required.");

        var existingInput = clsInputs_BLL.GetInputWithPlantsById(id);
        if (existingInput == null)
            return NotFound($"Input with ID {id} not found.");

        updatedInput.InputID = id;
        bool isUpdated = clsInputs_BLL.UpdateInputWithPlants(updatedInput);
        return isUpdated ? Ok(updatedInput) : BadRequest("Failed to update input.");
    }

    [HttpDelete("Input/{id}", Name = "DeleteInputWithPlants")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteInputWithPlants(int id)
    {
        bool isDeleted = clsInputs_BLL.DeleteInputWithPlants(id);
        return isDeleted
            ? Ok($"Input with ID {id} deleted successfully.")
            : NotFound($"Input with ID {id} not found.");
    }
}