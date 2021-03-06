﻿using Core.Core;
using Core.DAO;
using Core.Negocio;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utils
{
    public class ClasseInjetoraFachada
    {
        private Dictionary<string, IDAO> daos;

        private Dictionary<string, Dictionary<string, List<IStrategy>>> rns;

        public ClasseInjetoraFachada()
        {
            daos = new Dictionary<string, IDAO>();
            rns = new Dictionary<string, Dictionary<string, List<IStrategy>>>();

            //_gerar_log = new Gerar_log();
            parametro_excluir para_ex = new parametro_excluir();
            ClasseDAO claDAO = new ClasseDAO();
            daos.Add(typeof(Classe).Name, claDAO);
            List<IStrategy> rnsSalvarClasse = new List<IStrategy>();
            List<IStrategy> rnsAlterarClasse = new List<IStrategy>();
            List<IStrategy> rnsExcluirClasse = new List<IStrategy>();
            rnsExcluirClasse.Add(para_ex);
            List<IStrategy> rnsConsultarClasse = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsClasse = new Dictionary<string, List<IStrategy>>();
            rnsClasse.Add("SALVAR", rnsSalvarClasse);
            rnsClasse.Add("ALTERAR", rnsAlterarClasse);
            rnsClasse.Add("EXCLUIR", rnsExcluirClasse);
            rnsClasse.Add("CONSULTAR", rnsConsultarClasse);
            rns.Add(typeof(Classe).Name, rnsClasse);

            Validar_passagens val_pass = new Validar_passagens();
            PassagensDAO passDAO = new PassagensDAO();
            daos.Add(typeof(Passagens).Name, passDAO);
            List<IStrategy> rnsSalvarPassagens = new List<IStrategy>();
            rnsSalvarPassagens.Add(val_pass);
            List<IStrategy> rnsAlterarPassagens = new List<IStrategy>();
            rnsAlterarPassagens.Add(val_pass);
            List<IStrategy> rnsExcluirPassagens = new List<IStrategy>();
            rnsExcluirPassagens.Add(para_ex);
            List<IStrategy> rnsConsultarPassagens = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsPassagens = new Dictionary<string, List<IStrategy>>();
            rnsPassagens.Add("SALVAR", rnsSalvarPassagens);
            rnsPassagens.Add("ALTERAR", rnsAlterarPassagens);
            rnsPassagens.Add("EXCLUIR", rnsExcluirPassagens);
            rnsPassagens.Add("CONSULTAR", rnsConsultarPassagens);
            rns.Add(typeof(Passagens).Name, rnsPassagens);

            AviaoDAO aviaoDAO = new AviaoDAO();
            daos.Add(typeof(Aviao).Name, aviaoDAO);
            List<IStrategy> rnsSalvaraviao = new List<IStrategy>();
            List<IStrategy> rnsAlteraraviao = new List<IStrategy>();
            List<IStrategy> rnsExcluiraviao = new List<IStrategy>();
            rnsExcluiraviao.Add(para_ex);
            List<IStrategy> rnsConsultaraviao = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsaviao = new Dictionary<string, List<IStrategy>>();
            rnsaviao.Add("SALVAR", rnsSalvaraviao);
            rnsaviao.Add("ALTERAR", rnsAlteraraviao);
            rnsaviao.Add("EXCLUIR", rnsExcluiraviao);
            rnsaviao.Add("CONSULTAR", rnsConsultaraviao);
            rns.Add(typeof(Aviao).Name, rnsaviao);

            BagagemDAO bagDAO = new BagagemDAO();
            daos.Add(typeof(Bagagem).Name, bagDAO);
            List<IStrategy> rnsSalvarbagagem = new List<IStrategy>();
            List<IStrategy> rnsAlterarbagagem = new List<IStrategy>();
            List<IStrategy> rnsExcluirbagagem = new List<IStrategy>();
            rnsExcluirbagagem.Add(para_ex);
            List<IStrategy> rnsConsultarbagagem = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsbagagem = new Dictionary<string, List<IStrategy>>();
            rnsbagagem.Add("SALVAR", rnsSalvarbagagem);
            rnsbagagem.Add("ALTERAR", rnsAlterarbagagem);
            rnsbagagem.Add("EXCLUIR", rnsExcluirbagagem);
            rnsbagagem.Add("CONSULTAR", rnsConsultarbagagem);
            rns.Add(typeof(Bagagem).Name, rnsbagagem);

            ViagemDAO ViageDAO = new ViagemDAO();
            daos.Add(typeof(Viagem).Name, ViageDAO);
            List<IStrategy> rnsSalvarViagem = new List<IStrategy>();
            List<IStrategy> rnsAlterarViagem = new List<IStrategy>();
            List<IStrategy> rnsExcluirViagem = new List<IStrategy>();
            rnsExcluirViagem.Add(para_ex);
            List<IStrategy> rnsConsultarViagem = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsViagem = new Dictionary<string, List<IStrategy>>();
            rnsViagem.Add("SALVAR", rnsSalvarViagem);
            rnsViagem.Add("ALTERAR", rnsAlterarViagem);
            rnsViagem.Add("EXCLUIR", rnsExcluirViagem);
            rnsViagem.Add("CONSULTAR", rnsConsultarViagem);
            rns.Add(typeof(Viagem).Name, rnsViagem);

            BilheteDAO bilheDAO = new BilheteDAO();
            daos.Add(typeof(Bilhete).Name, bilheDAO);
            List<IStrategy> rnsSalvarBilhete = new List<IStrategy>();
            List<IStrategy> rnsAlterarBilhete = new List<IStrategy>();
            List<IStrategy> rnsExcluirBilhete = new List<IStrategy>();
            rnsExcluirBilhete.Add(para_ex);
            List<IStrategy> rnsConsultarBilhete = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsBilhete = new Dictionary<string, List<IStrategy>>();
            rnsBilhete.Add("SALVAR", rnsSalvarBilhete);
            rnsBilhete.Add("ALTERAR", rnsAlterarBilhete);
            rnsBilhete.Add("EXCLUIR", rnsExcluirBilhete);
            rnsBilhete.Add("CONSULTAR", rnsConsultarBilhete);
            rns.Add(typeof(Bilhete).Name, rnsBilhete);

            AeroportoDAO AeroDAO = new AeroportoDAO();
            daos.Add(typeof(Aeroporto).Name, AeroDAO);
            List<IStrategy> rnsSalvarAeroporto = new List<IStrategy>();
            List<IStrategy> rnsAlterarAeroporto = new List<IStrategy>();
            List<IStrategy> rnsExcluirAeroporto = new List<IStrategy>();
            rnsExcluirAeroporto.Add(para_ex);
            List<IStrategy> rnsConsultarAeroporto = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsAeroporto = new Dictionary<string, List<IStrategy>>();
            rnsAeroporto.Add("SALVAR", rnsSalvarAeroporto);
            rnsAeroporto.Add("ALTERAR", rnsAlterarAeroporto);
            rnsAeroporto.Add("EXCLUIR", rnsExcluirAeroporto);
            rnsAeroporto.Add("CONSULTAR", rnsConsultarAeroporto);
            rns.Add(typeof(Aeroporto).Name, rnsAeroporto);

            Check_inDAO CheckDAO = new Check_inDAO();
            Fazer_Check_in fzchk = new Fazer_Check_in();
            daos.Add(typeof(Check_in).Name, CheckDAO);
            List<IStrategy> rnsSalvarCheck_in = new List<IStrategy>();
            rnsSalvarCheck_in.Add(fzchk);
            List<IStrategy> rnsAlterarCheck_in = new List<IStrategy>();
            List<IStrategy> rnsExcluirCheck_in = new List<IStrategy>();
            rnsExcluirCheck_in.Add(para_ex);
            List<IStrategy> rnsConsultarCheck_in = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsCheck_in = new Dictionary<string, List<IStrategy>>();
            rnsCheck_in.Add("SALVAR", rnsSalvarCheck_in);
            rnsCheck_in.Add("ALTERAR", rnsAlterarCheck_in);
            rnsCheck_in.Add("EXCLUIR", rnsExcluirCheck_in);
            rnsCheck_in.Add("CONSULTAR", rnsConsultarCheck_in);
            rns.Add(typeof(Check_in).Name, rnsCheck_in);

            AssentoDAO AsseDAO = new AssentoDAO();
            daos.Add(typeof(Assento).Name, AsseDAO);
            List<IStrategy> rnsSalvarAssento = new List<IStrategy>();
            List<IStrategy> rnsAlterarAssento = new List<IStrategy>();
            List<IStrategy> rnsExcluirAssento = new List<IStrategy>();
            rnsExcluirAssento.Add(para_ex);
            List<IStrategy> rnsConsultarAssento = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsAssento = new Dictionary<string, List<IStrategy>>();
            rnsAssento.Add("SALVAR", rnsSalvarAssento);
            rnsAssento.Add("ALTERAR", rnsAlterarAssento);
            rnsAssento.Add("EXCLUIR", rnsExcluirAssento);
            rnsAssento.Add("CONSULTAR", rnsConsultarAssento);
            rns.Add(typeof(Assento).Name, rnsAssento);

            Passagem_VendaDAO Pas_VenDAO = new Passagem_VendaDAO();
            calculo_preco calculo_Precob = new calculo_preco();
            daos.Add(typeof(Passagem_Venda).Name, Pas_VenDAO);
            List<IStrategy> rnsSalvarPassagem_Venda = new List<IStrategy>();
            List<IStrategy> rnsAlterarPassagem_Venda = new List<IStrategy>();
            List<IStrategy> rnsExcluirPassagem_Venda = new List<IStrategy>();
            rnsExcluirPassagem_Venda.Add(para_ex);
            List<IStrategy> rnsConsultarPassagem_Venda = new List<IStrategy>();
            rnsConsultarPassagem_Venda.Add(calculo_Precob);
            Dictionary<string, List<IStrategy>> rnsPassagem_Venda = new Dictionary<string, List<IStrategy>>();
            rnsPassagem_Venda.Add("SALVAR", rnsSalvarPassagem_Venda);
            rnsPassagem_Venda.Add("ALTERAR", rnsAlterarPassagem_Venda);
            rnsPassagem_Venda.Add("EXCLUIR", rnsExcluirPassagem_Venda);
            rnsPassagem_Venda.Add("CONSULTAR", rnsConsultarPassagem_Venda);
            rns.Add(typeof(Passagem_Venda).Name, rnsPassagem_Venda);

            inteligencia inteli = new inteligencia();
            fillchartjs fillchartjs_s = new fillchartjs();
            AnaliseDAO AnaDAO = new AnaliseDAO();
            daos.Add(typeof(Analise).Name, AnaDAO);
            List<IStrategy> rnsSalvarAnalise = new List<IStrategy>();
            List<IStrategy> rnsAlterarAnalise = new List<IStrategy>();
            List<IStrategy> rnsExcluirAnalise = new List<IStrategy>();
            List<IStrategy> rnsConsultarAnalise = new List<IStrategy>();
            rnsConsultarAnalise.Add(inteli);
            //rnsConsultarAnalise.Add(fillchartjs_s);
            Dictionary<string, List<IStrategy>> rnsAnalise = new Dictionary<string, List<IStrategy>>();
            rnsAnalise.Add("SALVAR", rnsSalvarAnalise);
            rnsAnalise.Add("ALTERAR", rnsAlterarAnalise);
            rnsAnalise.Add("EXCLUIR", rnsExcluirAnalise);
            rnsAnalise.Add("CONSULTAR", rnsConsultarAnalise);
            rns.Add(typeof(Analise).Name, rnsAnalise);

            DepartamentoDAO depDAO = new DepartamentoDAO();
            daos.Add(typeof(Departamento).Name, depDAO);
            List<IStrategy> rnsSalvarDepartamento = new List<IStrategy>();
            List<IStrategy> rnsAlterarDepartamento = new List<IStrategy>();
            List<IStrategy> rnsExcluirDepartamento = new List<IStrategy>();
            rnsExcluirDepartamento.Add(para_ex);
            List<IStrategy> rnsConsultarDepartamento = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsDepartamento = new Dictionary<string, List<IStrategy>>();
            rnsDepartamento.Add("SALVAR", rnsSalvarDepartamento);
            rnsDepartamento.Add("ALTERAR", rnsAlterarDepartamento);
            rnsDepartamento.Add("EXCLUIR", rnsExcluirDepartamento);
            rnsDepartamento.Add("CONSULTAR", rnsConsultarDepartamento);
            rns.Add(typeof(Departamento).Name, rnsDepartamento);

            EnderecoDAO endDAO = new EnderecoDAO();
            daos.Add(typeof(Endereco).Name, endDAO);
            List<IStrategy> rnsSalvarEndereco = new List<IStrategy>();
            List<IStrategy> rnsAlterarEndereco = new List<IStrategy>();
            List<IStrategy> rnsExcluirEndereco = new List<IStrategy>();
            rnsExcluirEndereco.Add(para_ex);
            List<IStrategy> rnsConsultarEndereco = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsEndereco = new Dictionary<string, List<IStrategy>>();
            rnsEndereco.Add("SALVAR", rnsSalvarEndereco);
            rnsEndereco.Add("ALTERAR", rnsAlterarEndereco);
            rnsEndereco.Add("EXCLUIR", rnsExcluirEndereco);
            rnsEndereco.Add("CONSULTAR", rnsConsultarEndereco);
            rns.Add(typeof(Endereco).Name, rnsEndereco);

            Exclusao_admin adm_ex = new Exclusao_admin();

            UsuariosDAO usuDAO = new UsuariosDAO();
            daos.Add(typeof(Usuarios).Name, usuDAO);
            List<IStrategy> rnsSalvarUsuarios = new List<IStrategy>();
            List<IStrategy> rnsAlterarUsuarios = new List<IStrategy>();
            List<IStrategy> rnsExcluirUsuarios = new List<IStrategy>();
            rnsExcluirUsuarios.Add(para_ex);
            rnsExcluirUsuarios.Add(adm_ex);
            List<IStrategy> rnsConsultarUsuarios = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsUsuarios = new Dictionary<string, List<IStrategy>>();
            rnsUsuarios.Add("SALVAR", rnsSalvarUsuarios);
            rnsUsuarios.Add("ALTERAR", rnsAlterarUsuarios);
            rnsUsuarios.Add("EXCLUIR", rnsExcluirUsuarios);
            rnsUsuarios.Add("CONSULTAR", rnsConsultarUsuarios);
            rns.Add(typeof(Usuarios).Name, rnsUsuarios);

            Cartao_CreditoDAO ccDAO = new Cartao_CreditoDAO();
            daos.Add(typeof(Cartao_Credito).Name, ccDAO);
            List<IStrategy> rnsSalvarCartao_Credito = new List<IStrategy>();
            List<IStrategy> rnsAlterarCartao_Credito = new List<IStrategy>();
            List<IStrategy> rnsExcluirCartao_Credito = new List<IStrategy>();
            rnsExcluirCartao_Credito.Add(para_ex);
            List<IStrategy> rnsConsultarCartao_Credito = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsCartao_Credito = new Dictionary<string, List<IStrategy>>();
            rnsCartao_Credito.Add("SALVAR", rnsSalvarCartao_Credito);
            rnsCartao_Credito.Add("ALTERAR", rnsAlterarCartao_Credito);
            rnsCartao_Credito.Add("EXCLUIR", rnsExcluirCartao_Credito);
            rnsCartao_Credito.Add("CONSULTAR", rnsConsultarCartao_Credito);
            rns.Add(typeof(Cartao_Credito).Name, rnsCartao_Credito);

            Fazer_Cliente fc = new Fazer_Cliente();

            ClienteDAO cliDAO = new ClienteDAO();
            daos.Add(typeof(Cliente).Name, cliDAO);
            List<IStrategy> rnsSalvarCliente = new List<IStrategy>();
            rnsSalvarCliente.Add(fc);
            List<IStrategy> rnsAlterarCliente = new List<IStrategy>();
            List<IStrategy> rnsExcluirCliente = new List<IStrategy>();
            rnsExcluirCliente.Add(para_ex);
            List<IStrategy> rnsConsultarCliente = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsCliente = new Dictionary<string, List<IStrategy>>();
            rnsCliente.Add("SALVAR", rnsSalvarCliente);
            rnsCliente.Add("ALTERAR", rnsAlterarCliente);
            rnsCliente.Add("EXCLUIR", rnsExcluirCliente);
            rnsCliente.Add("CONSULTAR", rnsConsultarCliente);
            rns.Add(typeof(Cliente).Name, rnsCliente);

            Fazer_compra fz_c = new Fazer_compra();


            VendaDAO VenDAO = new VendaDAO();
            daos.Add(typeof(Venda).Name, VenDAO);
            List<IStrategy> rnsSalvarVenda = new List<IStrategy>();
            rnsSalvarVenda.Add(fz_c);
            List<IStrategy> rnsAlterarVenda = new List<IStrategy>();
            List<IStrategy> rnsExcluirVenda = new List<IStrategy>();
            rnsExcluirVenda.Add(para_ex);
            List<IStrategy> rnsConsultarVenda = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsVenda = new Dictionary<string, List<IStrategy>>();
            rnsVenda.Add("SALVAR", rnsSalvarVenda);
            rnsVenda.Add("ALTERAR", rnsAlterarVenda);
            rnsVenda.Add("EXCLUIR", rnsExcluirVenda);
            rnsVenda.Add("CONSULTAR", rnsConsultarVenda);
            rns.Add(typeof(Venda).Name, rnsVenda);

            ProximoDepartamento pd = new ProximoDepartamento();

            StatusDAO StatusDAO = new StatusDAO();
            daos.Add(typeof(Status).Name, StatusDAO);
            List<IStrategy> rnsSalvarStatus = new List<IStrategy>();
            List<IStrategy> rnsAlterarStatus = new List<IStrategy>();
            rnsAlterarStatus.Add(pd);
            List<IStrategy> rnsExcluirStatus = new List<IStrategy>();
            rnsExcluirStatus.Add(para_ex);
            List<IStrategy> rnsConsultarStatus = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsStatus = new Dictionary<string, List<IStrategy>>();
            rnsStatus.Add("SALVAR", rnsSalvarStatus);
            rnsStatus.Add("ALTERAR", rnsAlterarStatus);
            rnsStatus.Add("EXCLUIR", rnsExcluirStatus);
            rnsStatus.Add("CONSULTAR", rnsConsultarStatus);
            rns.Add(typeof(Status).Name, rnsStatus);

            Fazer_Barrado f_bar = new Fazer_Barrado();

            BarradoDAO BarradoDAO = new BarradoDAO();
            daos.Add(typeof(Barrado).Name, BarradoDAO);
            List<IStrategy> rnsSalvarBarrado = new List<IStrategy>();
            rnsSalvarBarrado.Add(f_bar);
            List<IStrategy> rnsAlterarBarrado = new List<IStrategy>();
            List<IStrategy> rnsExcluirBarrado = new List<IStrategy>();
            rnsExcluirBarrado.Add(para_ex);
            List<IStrategy> rnsConsultarBarrado = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsBarrado = new Dictionary<string, List<IStrategy>>();
            rnsBarrado.Add("SALVAR", rnsSalvarBarrado);
            rnsBarrado.Add("ALTERAR", rnsAlterarBarrado);
            rnsBarrado.Add("EXCLUIR", rnsExcluirBarrado);
            rnsBarrado.Add("CONSULTAR", rnsConsultarBarrado);
            rns.Add(typeof(Barrado).Name, rnsBarrado);

            MotivoDAO MotivoDAO = new MotivoDAO();
            daos.Add(typeof(Motivo).Name, MotivoDAO);
            List<IStrategy> rnsSalvarMotivo = new List<IStrategy>();
            List<IStrategy> rnsAlterarMotivo = new List<IStrategy>();
            List<IStrategy> rnsExcluirMotivo = new List<IStrategy>();
            rnsExcluirMotivo.Add(para_ex);
            List<IStrategy> rnsConsultarMotivo = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsMotivo = new Dictionary<string, List<IStrategy>>();
            rnsMotivo.Add("SALVAR", rnsSalvarMotivo);
            rnsMotivo.Add("ALTERAR", rnsAlterarMotivo);
            rnsMotivo.Add("EXCLUIR", rnsExcluirMotivo);
            rnsMotivo.Add("CONSULTAR", rnsConsultarMotivo);
            rns.Add(typeof(Motivo).Name, rnsMotivo);

            PegarSumario ps = new PegarSumario();
            SumarioVooDAO SumarioVooDAO = new SumarioVooDAO();
            daos.Add(typeof(SumarioVoo).Name, SumarioVooDAO);
            List<IStrategy> rnsSalvarSumarioVoo = new List<IStrategy>();
            List<IStrategy> rnsAlterarSumarioVoo = new List<IStrategy>();
            List<IStrategy> rnsExcluirSumarioVoo = new List<IStrategy>();
            rnsExcluirSumarioVoo.Add(para_ex);
            List<IStrategy> rnsConsultarSumarioVoo = new List<IStrategy>();
            rnsConsultarSumarioVoo.Add(ps);
            Dictionary<string, List<IStrategy>> rnsSumarioVoo = new Dictionary<string, List<IStrategy>>();
            rnsSumarioVoo.Add("SALVAR", rnsSalvarSumarioVoo);
            rnsSumarioVoo.Add("ALTERAR", rnsAlterarSumarioVoo);
            rnsSumarioVoo.Add("EXCLUIR", rnsExcluirSumarioVoo);
            rnsSumarioVoo.Add("CONSULTAR", rnsConsultarSumarioVoo);
            rns.Add(typeof(SumarioVoo).Name, rnsSumarioVoo);
        }

        public void Pega(out Dictionary<string, Dictionary<string, List<IStrategy>>> rns,out Dictionary<string, IDAO> daos)
        {
            daos = this.daos;
            rns = this.rns;
        }
    }
}
