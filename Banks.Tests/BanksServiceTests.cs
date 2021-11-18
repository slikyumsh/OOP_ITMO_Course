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
            var bank = new Bank("Tinkoff", 10, 10, 10000, new CorrespondentAccount(10000000));
            _centerBank.AddBank(bank);
            var builder = new ClientBuilder();
            builder.AddName("DIMA");
            builder.AddSurname("DIMA");
            builder.AddAddress("DIMA");
            builder.AddPassport("DIMA");
            Client client = builder.Build();
            var debitAccountCreator = new DebitAccountCreator(1000, 365, 10d);
            IAccount account = debitAccountCreator.Create();
            var account1 = account as DebitAccount;
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
            var bank = new Bank("Tinkoff", 10, 10, 10000, new CorrespondentAccount(10000000));
            _centerBank.AddBank(bank);
            var builder = new ClientBuilder();
            builder.AddName("DIMA");
            builder.AddSurname("DIMA");
            builder.AddAddress("DIMA");
            builder.AddPassport("DIMA");
            Client client = builder.Build();
            var debitAccountCreator = new CreditAccountCreator(1000, 365, 10000, 10d);
            IAccount account = debitAccountCreator.Create();
            var account1 = account as CreditAccount;
            Assert.AreEqual(10d, account1.Commission);
            client.OpenNewAccount(bank, account);
            _centerBank.AddClient(client);
            _centerBank.ChangeCommission(bank, 22, new Message("DIMA"));
            Assert.AreEqual(22d, account1.Commission);
        }
        
        [Test]
        public void TryWriteOffCommission()
        {
            var bank = new Bank("Tinkoff", 10, 10, 10000, new CorrespondentAccount(10000000));
            _centerBank.AddBank(bank);
            var builder = new ClientBuilder();
            builder.AddName("DIMA");
            builder.AddSurname("DIMA");
            builder.AddAddress("DIMA");
            builder.AddPassport("DIMA");
            Client client = builder.Build();
            var debitAccountCreator = new CreditAccountCreator(1000, 365, 10000, 10);
            IAccount account = debitAccountCreator.Create();
            var account1 = account as CreditAccount;
            client.OpenNewAccount(bank, account);
            _centerBank.AddClient(client);
           _centerBank.CommissionWriteOff(new Message("Give me your money"));
            Assert.AreEqual(900, account1.Money);
        }
        
        [Test]
        public void TryPayProcents()
        {
            var bank = new Bank("Tinkoff", 10, 10, 10000, new CorrespondentAccount(10000000));
            _centerBank.AddBank(bank);
            var builder = new ClientBuilder();
            builder.AddName("DIMA");
            builder.AddSurname("DIMA");
            builder.AddAddress("DIMA");
            builder.AddPassport("DIMA");
            Client client = builder.Build();
            var debitAccountCreator = new DebitAccountCreator(1000, 365, 365);
            IAccount account = debitAccountCreator.Create();
            var account1 = account as DebitAccount;
            client.OpenNewAccount(bank, account);
            _centerBank.AddClient(client);
            _centerBank.PayPercents(new Message("Money-money-money"));
            Assert.AreEqual(1010, account1.Money);
        }

        [Test]
        public void SuccessTryDoBankTransaction()
        {
            var bank1 = new Bank("Tinkoff", 10, 10, 10000, new CorrespondentAccount(10000000));
            _centerBank.AddBank(bank1);
            var bank2 = new Bank("Sberbank", 10, 10, 10000, new CorrespondentAccount(10000000));
            _centerBank.AddBank(bank2);
            _centerBank.TransferMoney(bank1.CorrespondentAccount, bank2.CorrespondentAccount, 100000);
            Assert.AreEqual(9900000, bank1.Money);
            Assert.AreEqual(10100000, bank2.Money);
        }
        
        [Test]
        public void TryDoTransactionBetweenTwoClientsOfOneBank()
        {
            var bank = new Bank("Tinkoff", 10, 10, 10000, new CorrespondentAccount(10000000));
            _centerBank.AddBank(bank);
            var builder1 = new ClientBuilder();
            builder1.AddName("DIMA");
            builder1.AddSurname("DIMA");
            builder1.AddAddress("DIMA");
            builder1.AddPassport("DIMA");
            Client client1 = builder1.Build();
            var debitAccountCreator1 = new DebitAccountCreator(1000, 365, 365);
            IAccount account0 = debitAccountCreator1.Create();
            var account1 = account0 as DebitAccount;
            client1.OpenNewAccount(bank, account1);
            _centerBank.AddClient(client1);

            var builder2 = new ClientBuilder();
            builder2.AddName("DIMA");
            builder2.AddSurname("DIMA");
            builder2.AddAddress("DIMA");
            builder2.AddPassport("DIMA");
            Client client2 = builder2.Build();
            var debitAccountCreator2 = new DebitAccountCreator(1000, 365, 365);
            IAccount account2 = debitAccountCreator2.Create();
            var account3 = account2 as DebitAccount;
            client2.OpenNewAccount(bank, account3);
            _centerBank.AddClient(client2);
            
            _centerBank.TransferMoney(account3, account1, 1000);
            Assert.AreEqual(2000, account1.Money);
            Assert.AreEqual(0, account3.Money);
        }
        
        [Test]
        public void TryDoTransactionBetweenTwoClientsOfDifferentBanks()
        {
            var bank = new Bank("Tinkoff", 10, 10, 10000, new CorrespondentAccount(10000000));
            _centerBank.AddBank(bank);
            var bank1 = new Bank("Sber", 10, 10, 10000, new CorrespondentAccount(10000000));
            _centerBank.AddBank(bank1);
            var builder1 = new ClientBuilder();
            builder1.AddName("DIMA");
            builder1.AddSurname("DIMA");
            builder1.AddAddress("DIMA");
            builder1.AddPassport("DIMA");
            Client client1 = builder1.Build();
            var debitAccountCreator1 = new DebitAccountCreator(1000, 365, 365);
            IAccount account0 = debitAccountCreator1.Create();
            var account1 = account0 as DebitAccount;
            client1.OpenNewAccount(bank, account1);
            _centerBank.AddClient(client1);
            var builder2 = new ClientBuilder();
            builder2.AddName("DIMA");
            builder2.AddSurname("DIMA");
            builder2.AddAddress("DIMA");
            builder2.AddPassport("DIMA");
            Client client2 = builder2.Build();
            var debitAccountCreator2 = new DebitAccountCreator(1000, 365, 365);
            IAccount account2 = debitAccountCreator2.Create();
            var account3 = account2 as DebitAccount;
            client2.OpenNewAccount(bank1, account3);
            _centerBank.AddClient(client2);
            Assert.AreEqual(true, bank1.AccountOwnerVerification(account3));
            _centerBank.TransferMoney(account3, account1, 1000);
            Assert.AreEqual(2000, account1.Money);
            Assert.AreEqual(0, account3.Money);
        }
        
        [Test]
        public void TryDoCancelTransactionBetweenTwoClientsOfDifferentBanks()
        {
            var bank = new Bank("Tinkoff", 10, 10, 10000, new CorrespondentAccount(10000000));
            _centerBank.AddBank(bank);
            var bank1 = new Bank("Sber", 10, 10, 10000, new CorrespondentAccount(10000000));
            _centerBank.AddBank(bank1);
            var builder1 = new ClientBuilder();
            builder1.AddName("DIMA");
            builder1.AddSurname("DIMA");
            builder1.AddAddress("DIMA");
            builder1.AddPassport("DIMA");
            Client client1 = builder1.Build();
            var debitAccountCreator1 = new DebitAccountCreator(1000, 365, 365);
            IAccount account0 = debitAccountCreator1.Create();
            var account1 = account0 as DebitAccount;
            client1.OpenNewAccount(bank, account1);
            _centerBank.AddClient(client1);
            var builder2 = new ClientBuilder();
            builder2.AddName("DIMA");
            builder2.AddSurname("DIMA");
            builder2.AddAddress("DIMA");
            builder2.AddPassport("DIMA");
            Client client2 = builder2.Build();
            var debitAccountCreator2 = new DebitAccountCreator(1000, 365, 365);
            IAccount account2 = debitAccountCreator2.Create();
            var account3 = account2 as DebitAccount;
            client2.OpenNewAccount(bank1, account3);
            _centerBank.AddClient(client2);
            var transaction = new Transaction(account3, account1, 1000);
            _centerBank.TransferMoney(transaction);
            Assert.AreEqual(2000, account1.Money);
            Assert.AreEqual(0, account3.Money);
            _centerBank.CancellationOfTransaction(transaction);
            Assert.AreEqual(1000, account1.Money);
            Assert.AreEqual(1000, account3.Money);
        }
        
        [Test]
        public void TryTimeModelling()
        {
            var bank = new Bank("Tinkoff", 10, 10, 10000, new CorrespondentAccount(10000000));
            _centerBank.AddBank(bank);
           
            var builder1 = new ClientBuilder();
            builder1.AddName("DIMA");
            builder1.AddSurname("DIMA");
            builder1.AddAddress("DIMA");
            builder1.AddPassport("DIMA");
            Client client1 = builder1.Build();
            var debitAccountCreator1 = new DebitAccountCreator(1000, 365, 365);
            IAccount account0 = debitAccountCreator1.Create();
            var account1 = account0 as DebitAccount;
            client1.OpenNewAccount(bank, account1);
            _centerBank.AddClient(client1);
            _centerBank.ModelingWorkOfTheBankingSystemAfterCertainNumberOfDays(31, new Message("GoodBye money"), new Message("Hello Money"));
            Assert.AreEqual(1361.3274044862344d, account1.Money);
        }
    }
}