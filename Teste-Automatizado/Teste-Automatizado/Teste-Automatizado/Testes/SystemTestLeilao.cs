﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste_Automatizado.Pages;

namespace Teste_Automatizado.Testes
{
    [TestFixture]
    class SystemTestLeilao
    {
        private PageLeiloes leilao;
        IWebDriver driver;
        private PageUsuario usuarios;

         [SetUp]
        public void AntesDosTestes()
        {
            
            driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost:8080/apenas-teste/limpa");
            leilao = new PageLeiloes(driver);
            usuarios = new PageUsuario(driver);

            usuarios.Visita();
            usuarios.Novo().Cadastra("Usuario 1 comprador", "comprador@user.com");
            usuarios.Novo().Cadastra("Usuario 1 vendedor", "vendedor@user.com");
            

            Assert.IsTrue(usuarios.ExisteNaListagem("Usuario 1 vendedor", "vendedor@user.com"));
        }


        [TearDown]
        public void DepoisDosTestes()
        {
            driver.Close();
        }
        
        [Test]
        public void deveCadastrarUmLeilao()
        {
            leilao.Visita();
            leilao.Novo().Cadastra("Produto", 300, "Usuario 1 vendedor", true);
            leilao.ExisteNaListagem("Produto", 300, "Usuario 1 vendedor", true);
        }

        [Test]
        public void deveValidarNomeCadastrarLeilao()
        {
            leilao.Visita();
            PageNovoLeilao teste = leilao.Novo();
            teste.Cadastra("", 300, "Usuario 1 vendedor", false);

             Assert.IsTrue(teste.validaNomeLeilaoObrigatorio());
        }

        [Test]
        public void deveValidarValorInicialInvalido()
        {
            leilao.Visita();
            PageNovoLeilao teste = leilao.Novo();
            teste.Cadastra("Produto", 0, "Usuario 1 vendedor", false);

            Assert.IsTrue(teste.validaLanceLeilaoObrigatorio());
        }

    }
}