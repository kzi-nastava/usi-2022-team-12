using HealthInstitution.Core.Features.NotificationManagement.Model;
using HealthInstitution.Core.Features.NotificationManagement.Repository;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Core.Features.NotificationManagement.Service
{
    public class UserNotificationService : IUserNotificationService
    {
        private readonly IUserNotificationRepository _userNotificationRepository;

        public UserNotificationService(IUserNotificationRepository userNotificationRepository)
        {
            _userNotificationRepository = userNotificationRepository;
        }

        #region CRUD methods
        public IEnumerable<UserNotification> ReadAll()
        {
            return _userNotificationRepository.ReadAll();
        }

        public UserNotification Read(Guid notificationId)
        {
            return _userNotificationRepository.Read(notificationId);
        }

        public UserNotification Create(UserNotification newNotification)
        {
            return _userNotificationRepository.Create(newNotification);
        }

        public UserNotification Update(UserNotification notificationToUpdate)
        {
            return _userNotificationRepository.Update(notificationToUpdate);
        }

        public UserNotification Delete(Guid notificationId)
        {
            return _userNotificationRepository.Delete(notificationId);
        }

        #endregion

        public IList<UserNotification> GetValidNotificationsForUser(Guid userId)
        {
            return _userNotificationRepository.GetValidNotificationsForUser(userId);
        }
        public void CreateNotification(Guid userId, string description)
        {
            var newNotification = new UserNotification()
            {
                UserId = userId,
                Content = description,
                IsShown = false
            };

            Create(newNotification);
        }
    }
}
