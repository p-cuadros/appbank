namespace apibanca.application.DTOs;
public class AccountDto
{
        public int idAccount { get; set; }
        public int idUser { get; set; }
        public decimal balance { get; set; }
        public bool isActive { get; set; }
}
