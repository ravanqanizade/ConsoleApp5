using System;

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException(string message) : base(message) { }
}

public class NegativeAmountException : Exception
{
    public NegativeAmountException(string message) : base(message) { }
}

public class InvalidPinException : Exception
{
    public InvalidPinException(string message) : base(message) { }
}

public class ATM
{
    private decimal balance;
    private string pin;

    public ATM(decimal initialBalance, string pin)
    {
        balance = initialBalance;
        this.pin = pin;
    }

    public void CheckBalance(string enteredPin)
    {
        ValidatePin(enteredPin);
        Console.WriteLine($"Your current balance is: {balance:C}");
    }

    public void Deposit(decimal amount, string enteredPin)
    {
        ValidatePin(enteredPin);

        if (amount <= 0)
        {
            throw new NegativeAmountException("Deposit amount must be positive.");
        }

        balance += amount;
        Console.WriteLine($"You have successfully deposited: {amount:C}. Your new balance is: {balance:C}");
    }

    public void Withdraw(decimal amount, string enteredPin)
    {
        ValidatePin(enteredPin);

        if (amount <= 0)
        {
            throw new NegativeAmountException("Withdrawal amount must be positive.");
        }

        if (amount > balance)
        {
            throw new InsufficientFundsException("Insufficient funds for this withdrawal.");
        }

        balance -= amount;
        Console.WriteLine($"You have successfully withdrawn: {amount:C}. Your new balance is: {balance:C}");
    }

    private void ValidatePin(string enteredPin)
    {
        if (enteredPin != pin)
        {
            throw new InvalidPinException("The entered PIN is incorrect.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        ATM atm = new ATM(1000.00m, "1234");

        try
        {
            atm.CheckBalance("1234");
            atm.Deposit(500.00m, "1234");
            atm.Withdraw(200.00m, "1234");
            atm.CheckBalance("1234");

         
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
