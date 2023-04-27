using apibanca.application.Exceptions;

namespace apibanca.application.Entities;
    
public class Account
{
    public const decimal MIN_ACCOUNT_BALANCE = 100;
    public const decimal MAX_PERCENTAGE_WITHDRAW = 0.9M;
    public const decimal MAX_AMOUNT_DEPOSIT = 10000;

    public int IDAccount { get; set; }
    public int IDUser { get; set; }
    public decimal Balance { get; set; }
    public bool IsActive { get; set; }

    public static Account Create(int idUser) 
    { 
        return new Account() {
            IDUser = idUser,
            Balance = 0,
            IsActive = true,
            };
    }
    public void Delete() 
    { 
        if (!IsActive) throw new AppException("This account is invalid.");
        IsActive = false;
    }
    public void Deposit(decimal amount) 
    {
        if (!IsActive) throw new AppException("This account is invalid.");
        if ((amount) <= 0) throw new AppException("The operation amount can't be 0 or less.");
        if ((Balance + amount) < MIN_ACCOUNT_BALANCE) throw new AppException("The balance can't be less than " + MIN_ACCOUNT_BALANCE.ToString());
        if (amount > MAX_AMOUNT_DEPOSIT) throw new AppException("The operation amount can't be more than " + MAX_AMOUNT_DEPOSIT.ToString());
        Balance += amount;
    }
    public void Withdraw(decimal amount) 
    {
        if (!IsActive) throw new AppException("This account is invalid.");
        if ((Balance - amount) < MIN_ACCOUNT_BALANCE) throw new AppException("The balance can't be less than " + MIN_ACCOUNT_BALANCE.ToString());
        if ((amount / Balance) > MAX_PERCENTAGE_WITHDRAW) throw new AppException("The operation amount can't be more than 90% of the balance.");
        Balance -= amount;
    }
}
