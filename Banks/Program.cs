using System;
using System.Collections.Generic;
using System.Linq;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            string command;
            var centerBank = new CenterBank();
            var accounts = new List<IAccount>();
            var transactions = new List<Transaction>();
            while (true)
            {
                command = Console.ReadLine();
                var parser = new Parser(command);
                string commandName = parser.Command;
                IReadOnlyList<string> arguments = parser.Arguments;
                switch (commandName)
                {
                    case PossibleCommandName.CreateClient:
                        switch (arguments.Count)
                        {
                            case 2:
                                var builder1 = new ClientBuilder();
                                builder1.AddName(arguments[0]);
                                builder1.AddSurname(arguments[1]);
                                Client client1 = builder1.Build();
                                centerBank.AddClient(client1);
                                break;
                            case 3:
                                var clientBuilder = new ClientBuilder();
                                clientBuilder.AddName(arguments[0]);
                                clientBuilder.AddSurname(arguments[1]);
                                clientBuilder.AddAddress(arguments[2]);
                                Client client2 = clientBuilder.Build();
                                centerBank.AddClient(client2);
                                break;
                            case 4:
                                var builder2 = new ClientBuilder();
                                builder2.AddName(arguments[0]);
                                builder2.AddSurname(arguments[1]);
                                builder2.AddAddress(arguments[2]);
                                builder2.AddPassport(arguments[3]);
                                Client client3 = builder2.Build();
                                centerBank.AddClient(client3);
                                break;
                            default:
                                throw new ArgumentException("Invalid command");
                        }

                        break;
                    case PossibleCommandName.CreateBank:
                        if (arguments.Count != 5)
                            throw new ArgumentException("Invalid command");
                        var bank = new Bank(arguments[0], int.Parse(arguments[1]), int.Parse(arguments[2]), int.Parse(arguments[3]), new CorrespondentAccount(int.Parse(arguments[4])));
                        centerBank.AddBank(bank);
                        break;
                    case PossibleCommandName.AddClientToBankWithDepositeAccount:
                        if (arguments.Count != 4)
                            throw new ArgumentException("Invalid command");
                        Bank bank1 = centerBank.GetBankById(int.Parse(arguments[0]));
                        Client client4 = centerBank.GetClientById(int.Parse(arguments[1]));
                        var depositeAccountCreator1 = new DepositeAccountCreator(int.Parse(arguments[2]), int.Parse(arguments[3]));
                        IAccount account1 = depositeAccountCreator1.Create();
                        var account2 = account1 as DepositeAccount;
                        client4.OpenNewAccount(bank1, account2);
                        accounts.Add(account2);
                        break;
                    case PossibleCommandName.AddClientToBankWithCreditAccount:
                        if (arguments.Count != 6)
                            throw new ArgumentException("Invalid command");
                        Bank bank2 = centerBank.GetBankById(int.Parse(arguments[0]));
                        Client client5 = centerBank.GetClientById(int.Parse(arguments[1]));
                        var creditAccountCreator1 = new CreditAccountCreator(int.Parse(arguments[2]), int.Parse(arguments[3]), int.Parse(arguments[4]), int.Parse(arguments[5]));
                        IAccount account3 = creditAccountCreator1.Create();
                        var account4 = account3 as CreditAccount;
                        client5.OpenNewAccount(bank2, account4);
                        accounts.Add(account4);
                        break;
                    case PossibleCommandName.AddClientToBankWithDebitAccount:
                        if (arguments.Count != 5)
                            throw new ArgumentException("Invalid command");
                        Bank bank3 = centerBank.GetBankById(int.Parse(arguments[0]));
                        Client client6 = centerBank.GetClientById(int.Parse(arguments[1]));
                        var debitAccountCreator2 = new DebitAccountCreator(int.Parse(arguments[2]), int.Parse(arguments[3]), int.Parse(arguments[4]));
                        IAccount account5 = debitAccountCreator2.Create();
                        var account6 = account5 as DebitAccount;
                        client6.OpenNewAccount(bank3, account6);
                        accounts.Add(account6);
                        break;
                    case PossibleCommandName.PayPercents:
                        if (arguments.Count != 1)
                            throw new ArgumentException("Invalid command");
                        centerBank.PayPercents(new Message(arguments[0]));
                        break;
                    case PossibleCommandName.GetCommission:
                        if (arguments.Count != 1)
                            throw new ArgumentException("Invalid command");
                        centerBank.CommissionWriteOff(new Message(arguments[0]));
                        break;
                    case PossibleCommandName.TransferMoney:
                        if (arguments.Count != 3)
                            throw new ArgumentException("Invalid command");
                        int accountNumber1 = int.Parse(arguments[0]);
                        int accountNumber2 = int.Parse(arguments[1]);
                        int money = int.Parse(arguments[2]);
                        if (accountNumber1 >= accounts.Count || accountNumber1 < 0)
                            throw new ArgumentException("Invalid number of account");
                        if (accountNumber2 >= accounts.Count || accountNumber2 < 0)
                            throw new ArgumentException("Invalid number of account");
                        var transaction =
                            new Transaction(accounts[accountNumber1], accounts[accountNumber2], money);
                        transactions.Add(transaction);
                        centerBank.TransferMoney(accounts[accountNumber1], accounts[accountNumber2], money);
                        break;
                    case PossibleCommandName.CancelTransaction:
                        if (arguments.Count != 1)
                            throw new ArgumentException("Invalid command");
                        int transactionNumber1 = int.Parse(arguments[0]);
                        if (transactionNumber1 >= transactions.Count || transactionNumber1 < 0)
                            throw new ArgumentException("Invalid number of account");
                        centerBank.CancellationOfTransaction(transactions[transactionNumber1]);
                        transactions[transactionNumber1] = null;
                        break;
                    case PossibleCommandName.ShowMoneyInAccount:
                        if (arguments.Count != 1)
                            throw new ArgumentException("Invalid command");
                        int accountNumber = int.Parse(arguments[0]);
                        if (accountNumber >= accounts.Count || accountNumber < 0)
                            throw new ArgumentException("Invalid number of account");
                        Console.WriteLine(accounts[accountNumber].Money);
                        break;
                    case PossibleCommandName.ShowIDsOfBanks:
                        if (arguments.Count > 0)
                            throw new ArgumentException("Invalid command");
                        IReadOnlyList<Bank> banks = centerBank.Banks;
                        foreach (Bank desiredBank in banks)
                        {
                            Console.WriteLine(desiredBank.Id);
                        }

                        break;
                    case PossibleCommandName.ShowIDsOfClients:
                        if (arguments.Count > 0)
                            throw new ArgumentException("Invalid command");
                        IReadOnlyList<Client> clients = centerBank.Clients;
                        foreach (Client desiredClient in clients)
                        {
                            Console.WriteLine(desiredClient.Id);
                        }

                        break;
                    case PossibleCommandName.SimulateInTime:
                        if (arguments.Count != 1)
                            throw new ArgumentException("Invalid command");
                        centerBank.ModelingWorkOfTheBankingSystemAfterCertainNumberOfDays(int.Parse(arguments[0]), new Message("hey"), new Message("hello"));
                        break;
                }
            }
        }
    }
}
