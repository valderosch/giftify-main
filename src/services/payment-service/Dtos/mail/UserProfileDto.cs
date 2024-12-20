namespace payment_service.Dtos.mail;

    public class UserProfileDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }