using Ghosn_DAL;
namespace Ghosn_BLL
{
    public class ClientDTO
    {
        public int ClientID { get; set; }
        public int PersonID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    internal static class ClientMapper
    {
        public static ClientObject ConvertDtoToObject(ClientDTO dto)
        {
            return new ClientObject(dto.ClientID, dto.PersonID, dto.Username, dto.Password);
        }

        public static ClientDTO ConvertObjectToDto(ClientObject obj)
        {
            return new ClientDTO
            {
                ClientID = obj.ClientID,
                PersonID = obj.PersonID,
                Username = obj.Username,
                Password = obj.Password
            };
        }
    }

    public class clsClients_BAL
    {
        public static List<ClientDTO> GetAllClients()
        {
            var clientObjects = clsClients_DAL.GetAllClients();
            return clientObjects.Select(ClientMapper.ConvertObjectToDto).ToList();
        }

        public static ClientDTO? GetClientById(int id)
        {
            var clientObject = clsClients_DAL.GetClientById(id);
            return clientObject != null ? ClientMapper.ConvertObjectToDto(clientObject) : null;
        }

        public static int AddClient(ClientDTO dto)
        {
            var clientObject = ClientMapper.ConvertDtoToObject(dto);
            return clsClients_DAL.AddClient(clientObject);
        }

        public static bool UpdateClient(ClientDTO dto)
        {
            var clientObject = ClientMapper.ConvertDtoToObject(dto);
            return clsClients_DAL.UpdateClient(clientObject);
        }

        public static bool DeleteClient(int id)
        {
            return clsClients_DAL.DeleteClient(id);
        }
    }
}
