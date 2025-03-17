using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ghosn_BLL
{
    public class ClientDTO : PersonDTO
    {
        public int ClientID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    internal static class ClientMapper
    {
        // Convert ClientDTO to ClientObject
        public static ClientObject ConvertDtoToObject(ClientDTO dto)
        {
            return new ClientObject(
                dto.ClientID,
                dto.PersonID, // Inherited from PersonDTO
                dto.Username,
                dto.Password
            );
        }

        // Convert ClientObject to ClientDTO
        public static ClientDTO ConvertObjectToDto(ClientObject obj)
        {
            return new ClientDTO
            {
                ClientID = obj.ClientID,
                PersonID = obj.PersonID, // Inherited from PersonDTO
                Username = obj.Username,
                Password = obj.Password,
                FirstName = "", // These will be populated separately
                LastName = "",
                Email = ""
            };
        }

        // Populate PersonDTO properties from a PersonObject
        public static void PopulatePersonProperties(ClientDTO clientDto, PersonDTO personDto)
        {
            if (personDto != null)
            {
                clientDto.FirstName = personDto.FirstName;
                clientDto.LastName = personDto.LastName;
                clientDto.Email = personDto.Email;
            }
        }
    }

    public class clsClients_BAL
    {
        // Retrieve all clients with their PersonDTO properties populated
        public static List<ClientDTO> GetAllClients()
        {
            var clientObjects = clsClients_DAL.GetAllClients();
            var clientDtos = clientObjects.Select(ClientMapper.ConvertObjectToDto).ToList();

            // Populate PersonDTO properties for each client
            foreach (var clientDto in clientDtos)
            {
                var personDto = clsPeople_BLL.GetPersonById(clientDto.PersonID);
                ClientMapper.PopulatePersonProperties(clientDto, personDto);
            }

            return clientDtos;
        }

        // Retrieve a client by ID with their PersonDTO properties populated
        public static ClientDTO? GetClientById(int id)
        {
            var clientObject = clsClients_DAL.GetClientById(id);
            if (clientObject == null) return null;

            var clientDto = ClientMapper.ConvertObjectToDto(clientObject);

            // Populate PersonDTO properties
            var personDto = clsPeople_BLL.GetPersonById(clientDto.PersonID);
            if (personDto == null) return null;

            ClientMapper.PopulatePersonProperties(clientDto, personDto);

            return clientDto;
        }

        // Add a new client
        public static int AddClient(ClientDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
            {
                throw new ArgumentException("Invalid client data.");
            }

            // Add the PersonDTO first
            var personDto = new PersonDTO
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };
            int personId = clsPeople_BLL.AddPerson(personDto);

            // Add the ClientDTO
            dto.PersonID = personId; // Set the PersonID for the client
            var clientObject = ClientMapper.ConvertDtoToObject(dto);
            return clsClients_DAL.AddClient(clientObject);
        }

        // Update an existing client
        public static bool UpdateClient(ClientDTO dto)
        {
            var clientObj = GetClientById(dto.ClientID); 

            if (dto == null || clientObj == null)
                return false;

            dto.ClientID = clientObj.ClientID;
            dto.PersonID = clientObj.PersonID;

            // Update the PersonDTO
            var personDto = new PersonDTO
            {
                PersonID = dto.PersonID,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };
            bool isPersonUpdated = clsPeople_BLL.UpdatePerson(personDto);

            // Update the ClientDTO
            var clientObject = ClientMapper.ConvertDtoToObject(dto);
            bool isClientUpdated = clsClients_DAL.UpdateClient(clientObject);

            return isPersonUpdated && isClientUpdated;
        }

        // Delete a client
        public static bool DeleteClient(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Client ID.");
            }

            // Retrieve the client to get the associated PersonID
            var clientObject = clsClients_DAL.GetClientById(id);
            if (clientObject == null)
            {
                return false;
            }

            // Delete the client
            bool isClientDeleted = clsClients_DAL.DeleteClient(id);

            // Delete the associated person
            bool isPersonDeleted = clsPeople_BLL.DeletePerson(clientObject.PersonID);

            return isClientDeleted && isPersonDeleted;
        }
    }
}