using System;
using System.Collections.Generic;
using Banks.Commands;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            var centerBank = new CenterBank();
            var accounts = new List<IAccount>();
            var transactions = new List<Transaction>();
            while (true)
            {
                string command = Console.ReadLine();
                var parser = new Parser(command);
                string commandName = parser.Command;
                switch (commandName)
                {
                    case PossibleCommandName.CreateClient:
                        var createClient = new CreateClient(command, centerBank);
                        createClient.Run();
                        break;
                    case PossibleCommandName.CreateBank:
                        var createBank = new CreateBank(command, centerBank);
                        createBank.Run();
                        break;
                    case PossibleCommandName.AddClientToBankWithDepositeAccount:
                        var addClientToBankWithDepositeAccount = new AddClientToBankWithDepositeAccount(command, centerBank, accounts);
                        addClientToBankWithDepositeAccount.Run();
                        break;
                    case PossibleCommandName.AddClientToBankWithCreditAccount:
                        var addClientToBankWithCreditAccount = new AddClientToBankWithCreditAccount(command, centerBank, accounts);
                        addClientToBankWithCreditAccount.Run();
                        break;
                    case PossibleCommandName.AddClientToBankWithDebitAccount:
                        var addClientToBankWithDebitAccount = new AddClientToBankWithDebitAccount(command, centerBank, accounts);
                        addClientToBankWithDebitAccount.Run();
                        break;
                    case PossibleCommandName.PayPercents:
                        var payPercents = new PayPercents(command, centerBank);
                        payPercents.Run();
                        break;
                    case PossibleCommandName.GetCommission:
                        var getCommission = new GetCommission(command, centerBank);
                        getCommission.Run();
                        break;
                    case PossibleCommandName.TransferMoney:
                        var transferMoney = new TransferMoney(command, centerBank, accounts, transactions);
                        transferMoney.Run();
                        break;
                    case PossibleCommandName.CancelTransaction:
                        var cancelTransaction = new CancelTransaction(command, centerBank, transactions);
                        cancelTransaction.Run();
                        break;
                    case PossibleCommandName.ShowMoneyInAccount:
                        var showMoneyInAccount = new ShowMoneyInAccount(command, centerBank, accounts);
                        showMoneyInAccount.Run();
                        break;
                    case PossibleCommandName.ShowIDsOfBanks:
                        var showIDsOfBanks = new ShowIDsOfBanks(command, centerBank);
                        showIDsOfBanks.Run();
                        break;
                    case PossibleCommandName.ShowIDsOfClients:
                        var showIDsOfClients = new ShowIDsOfClients(command, centerBank);
                        showIDsOfClients.Run();
                        break;
                    case PossibleCommandName.SimulateInTime:
                        var simulateInTime = new SimulateInTime(command, centerBank);
                        simulateInTime.Run();
                        break;
                    default:
                        throw new ArgumentException("Invalid command");
                }
            }
        }
    }
}
