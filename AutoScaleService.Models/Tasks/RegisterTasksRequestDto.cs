using System;

namespace AutoScaleService.Models.Tasks
{
    public class RegisterTasksRequestDto
    {
        public RegisterTasksRequestDto()
        { }

        public RegisterTasksRequestDto(int translationTasksCount, string notificationUrl, Guid requestId)
        {
            TranslationTasksCount = translationTasksCount;
            NotificationUrl = notificationUrl;
            RequestId = requestId;
        }

        public int TranslationTasksCount { get; set; }
        public string NotificationUrl { get; set; }
        public Guid RequestId { get; set; }
    }
}
