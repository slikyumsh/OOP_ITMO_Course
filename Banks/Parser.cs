using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Banks
{
    public class Parser
    {
        private string _commandName;
        private List<string> _arguments;

        public Parser(string input)
        {
            int spaceCounter = 0;
            _arguments = new List<string>();
            string result = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ' ')
                {
                    if (spaceCounter == 0)
                        _commandName = result;
                    else
                        _arguments.Add(result);

                    spaceCounter++;
                    result = string.Empty;
                }
                else if (i == input.Length - 1)
                {
                    result += input[i];
                    _arguments.Add(result);
                }
                else
                {
                    result += input[i];
                }
            }

            switch (_commandName)
            {
                case PossibleCommandName.CreateClient:
                    _commandName = PossibleCommandName.CreateClient;
                    break;
                case PossibleCommandName.CreateBank:
                    _commandName = PossibleCommandName.CreateBank;
                    break;
                case PossibleCommandName.CancelTransaction:
                    _commandName = PossibleCommandName.CancelTransaction;
                    break;
                case PossibleCommandName.GetCommission:
                    _commandName = PossibleCommandName.GetCommission;
                    break;
                case PossibleCommandName.PayPercents:
                    _commandName = PossibleCommandName.PayPercents;
                    break;
                case PossibleCommandName.AddClientToBankWithDepositeAccount:
                    _commandName = PossibleCommandName.AddClientToBankWithDepositeAccount;
                    break;
                case PossibleCommandName.TransferMoney:
                    _commandName = PossibleCommandName.TransferMoney;
                    break;
                case PossibleCommandName.ShowMoneyInAccount:
                    _commandName = PossibleCommandName.ShowMoneyInAccount;
                    break;
                case PossibleCommandName.ShowIDsOfBanks:
                    _commandName = PossibleCommandName.ShowIDsOfBanks;
                    break;
                case PossibleCommandName.ShowIDsOfClients:
                    _commandName = PossibleCommandName.ShowIDsOfClients;
                    break;
                case PossibleCommandName.AddClientToBankWithDebitAccount:
                    _commandName = PossibleCommandName.AddClientToBankWithDebitAccount;
                    break;
                case PossibleCommandName.AddClientToBankWithCreditAccount:
                    _commandName = PossibleCommandName.AddClientToBankWithCreditAccount;
                    break;
                case PossibleCommandName.SimulateInTime:
                    _commandName = PossibleCommandName.SimulateInTime;
                    break;
                default:
                    throw new ArgumentException("Invalid command");
            }
        }

        public string Command => _commandName;
        public IReadOnlyList<string> Arguments => _arguments;
    }
}