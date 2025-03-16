using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghosn_BLL
{
    public class NotificationDTO
    {
        public int NotificationID { get; set; }
        public string Title { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Body { get; set; }
    }

    public class clsNotifications_BLL
    {
        public static List<NotificationDTO> GetAllNotifications()
        {
            var notificationObjects = clsNotifications_DAL.GetAllNotifications();
            return notificationObjects.Select(ConvertToDTO).ToList();
        }

        public static NotificationDTO? GetNotificationById(int id)
        {
            var notificationObject = clsNotifications_DAL.GetNotificationById(id);
            return notificationObject != null ? ConvertToDTO(notificationObject) : null;
        }

        public static int AddNotification(NotificationDTO dto)
        {
            var notificationObject = ConvertToDALObject(dto);
            return clsNotifications_DAL.AddNotification(notificationObject);
        }

        public static bool UpdateNotification(NotificationDTO dto)
        {
            var notificationObject = ConvertToDALObject(dto);
            return clsNotifications_DAL.UpdateNotification(notificationObject);
        }

        public static bool DeleteNotification(int id)
        {
            return clsNotifications_DAL.DeleteNotification(id);
        }

        // Conversion methods
        private static NotificationDTO ConvertToDTO(NotificationObject obj)
        {
            return new NotificationDTO
            {
                NotificationID = obj.NotificationID,
                Title = obj.Title,
                DateAndTime = obj.DateAndTime,
                Body = obj.Body
            };
        }

        private static NotificationObject ConvertToDALObject(NotificationDTO dto)
        {
            return new NotificationObject(dto.NotificationID, dto.Title, dto.DateAndTime, dto.Body);
        }
    }
}
