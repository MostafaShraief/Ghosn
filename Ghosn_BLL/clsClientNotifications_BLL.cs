using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class ClientNotificationDTO
    {
        public int ClientNotificationID { get; set; }
        public int ClientID { get; set; }
        public int NotificationID { get; set; }
    }

    public class ClientNotificationRequestDTO : NotificationDTO
    {
        public int ClientID { get; set; }
    }

    public class clsClientNotifications_BLL
    {
        public static List<NotificationDTO?> GetAllClientNotifications()
        {
            var clientNotificationObjects = clsClientNotifications_DAL.GetAllClientNotifications();
            var clientNotificationDTO = clientNotificationObjects.Select(ConvertToDTO).ToList();

            List<NotificationDTO?> notificationDTO = new();
            foreach (var clientNotification in clientNotificationDTO)
            {
                notificationDTO.Add(clsNotifications_BLL.GetNotificationById(clientNotification.NotificationID));
            }

            return notificationDTO;
        }

        //public static ClientNotificationDTO? GetClientNotificationById(int id)
        //{
        //    var clientNotificationObject = clsClientNotifications_DAL.GetClientNotificationById(id);
        //    return clientNotificationObject != null ? ConvertToDTO(clientNotificationObject) : null;
        //}

        public static ClientNotificationDTO? GetClientNotificationByClientId(int id)
        {
            var clientNotificationObject = clsClientNotifications_DAL.GetClientNotificationByClientId(id);
            return clientNotificationObject != null ? ConvertToDTO(clientNotificationObject) : null;
        }

        public static int AddClientNotification(ClientNotificationRequestDTO dto)
        {
            NotificationDTO NotificationObject = new NotificationDTO { 
                NotificationID = dto.NotificationID, Title = dto.Title, 
                DateAndTime = dto.DateAndTime, Body = dto.Body
            };

            int NotificationID = clsNotifications_BLL.AddNotification(NotificationObject);

            ClientNotificationObject clientNotificationDTO = new ClientNotificationObject(0, dto.ClientID, NotificationID);

            clsClientNotifications_DAL.AddClientNotification(clientNotificationDTO);

            return NotificationID;
        }

        public static bool UpdateClientNotification(ClientNotificationDTO dto)
        {
            var clientNotificationObject = ConvertToDALObject(dto);
            return clsClientNotifications_DAL.UpdateClientNotification(clientNotificationObject);
        }

        public static bool DeleteClientNotification(int id)
        {
            return clsClientNotifications_DAL.DeleteClientNotification(id);
        }

        // Conversion methods
        private static ClientNotificationDTO ConvertToDTO(ClientNotificationObject obj)
        {
            return new ClientNotificationDTO
            {
                ClientNotificationID = obj.ClientNotificationID,
                ClientID = obj.ClientID,
                NotificationID = obj.NotificationID
            };
        }

        private static ClientNotificationObject ConvertToDALObject(ClientNotificationDTO dto)
        {
            return new ClientNotificationObject(dto.ClientNotificationID, dto.ClientID, dto.NotificationID);
        }
    }
}
