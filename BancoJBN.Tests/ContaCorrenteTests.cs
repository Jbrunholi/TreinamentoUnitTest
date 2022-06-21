using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BancoJBN.Tests
{
    [TestClass]
    public class ContaCorrenteTests
    {
        private ContaCorrente GetContaCorrente()
        {
            var cc = new ContaCorrente(
                Mock.Of<IAgenciaRepository>(),
                Mock.Of<IcontaRepository>(),
                Mock.Of<IExtratoRepository>()
            );

            return cc;
        }

        [TestMethod]
        public void Deposito_retorna_true_se_realizado_com_sucesso()
        {
            // arrange
            var cc = GetContaCorrente();

            var agencia = new Agencia() { Id = 100, Nome = "Agência Teste" };
            var conta = new Conta() { Id = 555, AgenciaId = 100, NomeCliente = "Ronaldinho Gaúcho", CPFCliente = "51515151515", Saldo = 100 };

            Mock.Get(cc.AgenciaRepository).Setup(r => r.GetById(100)).Returns(agencia);
            Mock.Get(cc.ContaRepository).Setup(r => r.GetById(100, 555)).Returns(conta);

            // act
            string erro;
            var result = cc.Deposito(100, 555, 50m, out erro);

            // assert
            Assert.IsTrue(result);
            Mock.Get(cc.ContaRepository).Verify(r => r.Save(It.Is<Conta>(c => c.AgenciaId == 100 && c.Id == 555 && c.Saldo == 150m)));
            Mock.Get(cc.ExtratoRepository).Verify(r => r.Save(It.Is<Extrato>(e => e.AgenciaId == 100 && e.ContaId == 555 && e.Descricao == "Depósito" && e.DataRegistro.Date == DateTime.Today && e.Valor == 50m && e.Saldo == 150m)));
        }

        [TestMethod]
        public void Deposito_erro_se_agencia_nao_existir()
        {
            // arrange
            var cc = GetContaCorrente();

            // act
            string erro;
            var result = cc.Deposito(666, 100, 100m, out erro);

            // assert
            Assert.IsFalse(result);
            Assert.AreEqual("Agencia Inválida!", erro);
        }

        [TestMethod]
        public void Deposito_erro_se_conta_nao_existir_na_agencia()
        {
            // arrange
            var cc = GetContaCorrente();

            var agencia = new Agencia() { Id = 100, Nome = "Agência Teste" };
            Mock.Get(cc.AgenciaRepository).Setup(r => r.GetById(100)).Returns(agencia);

            // act
            string erro;
            var result = cc.Deposito(100, 666, 100m, out erro); // conta não existe

            // assert
            Assert.IsFalse(result);
            Assert.AreEqual("Conta Inválida!", erro);
        }

        [TestMethod]
        public void Deposito_erro_se_valor_menor_ou_igual_zero()
        {
            // arrange
            var cc = GetContaCorrente();

            var agencia = new Agencia() { Id = 100, Nome = "Agência Teste" };
            var conta = new Conta() { Id = 555, AgenciaId = 100, NomeCliente = "Ronaldinho Gaúcho", CPFCliente = "51515151515", Saldo = 100 };

            Mock.Get(cc.AgenciaRepository).Setup(r => r.GetById(100)).Returns(agencia);
            Mock.Get(cc.ContaRepository).Setup(r => r.GetById(100, 555)).Returns(conta);

            // act
            string erro;
            var result = cc.Deposito(100, 555, 0m, out erro); // valor menor ou igual a 0

            // assert
            Assert.IsFalse(result);
            Assert.AreEqual("Valor do depósito deve ser maior ou igual a zero!", erro);
        }

        [TestMethod]
        public void Saque_retorna_true_se_realizado_com_sucesso()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Saque_retorna_erro_se_agencia_nao_existir()
        {
            // arrange
            var cc = GetContaCorrente();

            var agencia = new Agencia() { Id = 100, Nome = "Agência Teste" };
            var conta = new Conta() { Id = 555, AgenciaId = 100, NomeCliente = "Ronaldinho Gaúcho", CPFCliente = "51515151515", Saldo = 100 };

            Mock.Get(cc.AgenciaRepository).Setup(r => r.GetById(100)).Returns(agencia);
            Mock.Get(cc.ContaRepository).Setup(r => r.GetById(100, 555)).Returns(conta);

            // act
            string erro;
            var result = cc.Saque(666, 555, 50m, out erro); // agencia não existe

            // assert
            Assert.IsFalse(result);
            Assert.AreEqual("Agência inválida!", erro);
        }

        [TestMethod]
        public void Saque_retorna_erro_se_conta_nao_existir()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Saque_retorna_erro_se_valor_menor_ou_igual_zero()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Saque_retorna_erro_se_valor_maior_que_saldo_conta()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Transferencia_retorna_true_se_realizado_com_sucesso()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Transferencia_erro_se_agencia_origem_nao_existir()
        {
            // arrange
            var cc = GetContaCorrente();

            var agencia = new Agencia() { Id = 100, Nome = "Agência Teste" };
            var conta = new Conta() { Id = 555, AgenciaId = 100, NomeCliente = "Ronaldinho Gaúcho", CPFCliente = "51515151515", Saldo = 100 };

            Mock.Get(cc.AgenciaRepository).Setup(r => r.GetById(100)).Returns(agencia);
            Mock.Get(cc.ContaRepository).Setup(r => r.GetById(100, 555)).Returns(conta);

            // act
            string erro;
            var result = cc.Transferencia(666, 555, 50m, 200, 700, out erro); // agencia não existe

            // assert
            Assert.IsFalse(result);
            Assert.AreEqual("Agência de origem inválida!", erro);
        }

        [TestMethod]
        public void Transferencia_erro_se_conta_origem_nao_existir()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Transferencia_erro_se_agencia_destino_nao_existir()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Transferencia_erro_se_conta_destino_nao_existir()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Transferencia_erro_se_valor_maior_que_saldo_conta_origem()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Saldo_retorna_saldo_da_conta()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Saldo_erro_se_agencia_nao_existir()
        {
            // arrange
            var cc = GetContaCorrente();

            // act
            string erro;
            var result = cc.Saldo(666, 555, out erro); // agencia não existe

            // assert
            Assert.AreEqual(0m, result);
            Assert.AreEqual("Agência inválida!", erro);
        }

        [TestMethod]
        public void Saldo_erro_se_conta_nao_existir_na_agencia()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Extrato_retorna_registros_do_extrato()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Extrato_erro_se_agencia_nao_existir()
        {
            // arrange
            var cc = GetContaCorrente();

            // act
            string erro;
            var result = cc.Extrato(666, 555, new DateTime(2020, 01, 01), new DateTime(2020, 01, 15), out erro); // agencia não existe

            // assert
            Assert.IsNull(result);
            Assert.AreEqual("Agência inválida!", erro);
        }

        [TestMethod]
        public void Extrato_erro_se_conta_nao_existir_na_agencia()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Extrato_erro_se_data_inicio_maior_data_fim()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Extrato_erro_se_periodo_maior_120_dias()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Extrato_primeira_linha_com_saldo_anteior()
        {
            Assert.Inconclusive();
        }
    }
}
