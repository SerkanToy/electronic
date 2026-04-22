namespace electronic.Domain.Requests
{
    public record RegisterRequest
    {
        public required string Name { get; set; }
        public required string SurName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
