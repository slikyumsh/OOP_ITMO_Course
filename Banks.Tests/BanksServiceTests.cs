using System;
using NUnit.Framework;

namespace Banks.Tests
{
    public class BanksServiceTests
    {
        private CenterBank _centerBank;
        [SetUp]
        public void Setup()
        {
            _centerBank = new CenterBank();
        }

        [Test]
        public void TryChangeProcent()
        {
            Bank bank = new Bank("Tinkoff", 10, 10, 10000, new CorrespondentAccount(10000000));
            _centerBank.AddBank(bank);
            ClientBuilder builder = new ClientBuilder();
            builder.AddName("DIMA");
            builder.AddSurname("DIMA");
            builder.AddAddress("DIMA");
            builder.AddPassport("DIMA");
            Client client = builder.Build();
            DebitAccountCreator debitAccountCreator = new DebitAccountCreator(1000, 365, 10d);
            IAccount account = debitAccountCreator.Create();
            DebitAccount account1 = account as DebitAccount;
            Assert.AreEqual(1000, account1.Money);
            Assert.AreEqual(10d, account1.Percent);
            client.OpenNewAccount(bank, account);
            _centerBank.AddClient(client);
            _centerBank.ChangePercent(bank, 22, new Message("DIMA"));
            Assert.AreEqual(22d, account1.Percent);
        }
        
        [Test]
        public void TryChangeCommission()
        {
            Bank bank = new Bank("Tinkoff", 10, 10, 10000, new CorrespondentAccount(10000000));
            _centerBank.AddBank(bank);
            ClientBuilder builder = new ClientBuilder();
            builder.AddName("DIMA");
            builder.AddSurname("DIMA");
            builder.AddAddress("DIMA");
            builder.AddPassport("DIMA");
            Client client = builder.Build();
            CreditAccountCreator debitAccountCreator = new CreditAccountCreator(1000, 365, 10000, 10d);
            IAccount account = debitAccountCreator.Create();
            CreditAccount account1 = account as CreditAccount;
            Assert.AreEqual(10d, account1.Commission);
            client.OpenNewAccount(bank, account);
            _centerBank.AddClient(client);
            _centerBank.ChangeCommission(bank, 22, new Message("DIMA"));
            Assert.AreEqual(22d, account1.Commission);
        }
    }
}