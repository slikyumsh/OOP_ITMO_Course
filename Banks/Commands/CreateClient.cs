using Banks.Entities;
using Banks.Interfaces;
using Banks.Tools;

namespace Banks.Commands
{
    public class CreateClient : ICommand
    {
        private readonly Parser _parser;
        private readonly CenterBank _centerBank;

        public CreateClient(string command, CenterBank centerBank)
        {
            _parser = new Parser(command);
            _centerBank = centerBank;
        }

        public string Name { get; }

        public void Run()
        {
            switch (_parser.Arguments.Count)
            {
                case 2:
                    var builder1 = new ClientBuilder(new ConsoleLogger());
                    builder1.AddName(_parser.Arguments[0]);
                    builder1.AddSurname(_parser.Arguments[1]);
                    Client client1 = builder1.Build();
                    _centerBank.AddClient(client1);
                    break;
                case 3:
                    var clientBuilder = new ClientBuilder(new ConsoleLogger());
                    clientBuilder.AddName(_parser.Arguments[0]);
                    clientBuilder.AddSurname(_parser.Arguments[1]);
                    clientBuilder.AddAddress(_parser.Arguments[2]);
                    Client client2 = clientBuilder.Build();
                    _centerBank.AddClient(client2);
                    break;
                case 4:
                    var builder2 = new ClientBuilder(new ConsoleLogger());
                    builder2.AddName(_parser.Arguments[0]);
                    builder2.AddSurname(_parser.Arguments[1]);
                    builder2.AddAddress(_parser.Arguments[2]);
                    builder2.AddPassport(_parser.Arguments[3]);
                    Client client3 = builder2.Build();
                    _centerBank.AddClient(client3);
                    break;
            }
        }
    }
}