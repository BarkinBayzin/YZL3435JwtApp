namespace JWTApp.BackOffice.Core.DTOs
{
    public class CheckUserResponseDTO
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public int Id { get; set; }
        public bool IsExist { get; set; }
    }
}
