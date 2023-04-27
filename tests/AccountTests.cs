using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using apibanca.application.Entities;
using apibanca.application.Exceptions;

namespace apibanca.tests;

[TestClass]
public class AccountTests
{
    [TestMethod]
    public void GivenAnUser_WhenAccountIsCreaated_ResultIsSatisfactory()
    {
        // Arrange
        var idUser = 1;
        // Act
        var newAccount = Account.Create(idUser);
        // Assert
        Assert.IsNotNull(newAccount);
    }

    [TestMethod]
    public void GivenAnAccount_WhenIsDeleted_ResultIsSatisfactory()
    {
        // Arrange
        var idUser = 1;
        var account = Account.Create(idUser);
        // Act
        account.Delete();
        // Assert
        Assert.IsFalse(account.IsActive);
    }

    [TestMethod]
    public void GivenCorrectAmount_WhenDepositIsDone_ResultIsSatisfactory()
    {
        // Arrange
        var idUser = 1;
        var amount = 200;
        var account = Account.Create(idUser);
        // Act
        account.Deposit(amount);
        // Assert
        Assert.AreEqual(account.Balance, amount);        
    }

    [TestMethod]
    public void GivenCorrectAmount_WhenWithdrawIsDone_ResultIsSatisfactory()
    {
        // Arrange
        var idUser = 1;
        var balance = 500;
        var amount = 200;
        var account = Account.Create(idUser);
        account.Deposit(balance);
        // Act
        account.Withdraw(amount);
        // Assert
        Assert.AreEqual(account.Balance, balance - amount);        
    }    

    [TestMethod]
    public void GivenIncorrectAccount_WhenDepositIsDone_ThenExceptionIsThrownWithMessage()
    {
        // Arrange
        var idUser = 1;
        var amount = 200;
        var account = Account.Create(idUser);
        account.Delete();
        // Act
        var exception = Assert.ThrowsException<AppException>(() => account.Deposit(amount));
        // Assert
        Assert.IsNotNull(exception);
        Assert.IsTrue(exception.Message.StartsWith("This account is invalid"));
    }

    [TestMethod]
    public void GivenInsufficientBalance_WhenDepositIsDone_ThenExceptionIsThrownWithMessage()
    {
        // Arrange
        var idUser = 1;
        var amount = 50;
        var account = Account.Create(idUser);
        // Act
        var exception = Assert.ThrowsException<AppException>(() => account.Deposit(amount));
        // Assert
        Assert.IsNotNull(exception);
        Assert.IsTrue(exception.Message.StartsWith("The balance can't be less than"));
    }

    [TestMethod]
    public void GivenNegativeAmount_WhenDepositIsDone_ThenExceptionIsThrownWithMessage()
    {
        // Arrange
        var idUser = 1;
        var amount = -50;
        var account = Account.Create(idUser);
        // Act
        var exception = Assert.ThrowsException<AppException>(() => account.Deposit(amount));
        // Assert
        Assert.IsNotNull(exception);
        Assert.IsTrue(exception.Message.StartsWith("The operation amount can't be 0 or less."));
    }

    [TestMethod]
    public void GivenAmountOverLimit_WhenDepositIsDone_ThenExceptionIsThrownWithMessage()
    {
        // Arrange
        var idUser = 1;
        var amount = 10001;
        var account = Account.Create(idUser);
        // Act
        var exception = Assert.ThrowsException<AppException>(() => account.Deposit(amount));
        // Assert
        Assert.IsNotNull(exception);
        Assert.IsTrue(exception.Message.StartsWith("The operation amount can't be more than"));
    }


    [TestMethod]
    public void GivenIncorrectAccount_WhenWithdrawIsDone_ThenExceptionIsThrownWithMessage()
    {
        // Arrange
        var idUser = 1;
        var amount = 200;
        var account = Account.Create(idUser);
        account.Deposit(500);
        account.Delete();
        // Act
        var exception = Assert.ThrowsException<AppException>(() => account.Withdraw(amount));
        // Assert
        Assert.IsNotNull(exception);
        Assert.IsTrue(exception.Message.StartsWith("This account is invalid"));
    }

    [TestMethod]
    public void GivenInsufficientBalance_WhenWithdrawIsDone_ThenExceptionIsThrownWithMessage()
    {
        // Arrange
        var idUser = 1;
        var amount = 50;
        var account = Account.Create(idUser);
        account.Deposit(100);
        // Act
        var exception = Assert.ThrowsException<AppException>(() => account.Withdraw(amount));
        // Assert
        Assert.IsNotNull(exception);
        Assert.IsTrue(exception.Message.StartsWith("The balance can't be less than"));
    }

    [TestMethod]
    public void GivenAmountNearTotalBalance_WhenDepositIsDone_ThenExceptionIsThrownWithMessage()
    {
        // Arrange
        var idUser = 1;
        var amount = 2800;
        var account = Account.Create(idUser);
        account.Deposit(3000);
        // Act
        var exception = Assert.ThrowsException<AppException>(() => account.Withdraw(amount));
        // Assert
        Assert.IsNotNull(exception);
        Assert.IsTrue(exception.Message.StartsWith("The operation amount can't be more than 90% of the balance"));
    }
}