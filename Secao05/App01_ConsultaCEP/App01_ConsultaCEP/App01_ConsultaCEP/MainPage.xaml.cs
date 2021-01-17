using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultaCEP.Servico.Modelo;
using App01_ConsultaCEP.Servico;

namespace App01_ConsultaCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            btBuscar.Clicked += btBuscar_Click;
        }

        private async void btBuscar_Click(object sender, EventArgs args)
        {
            try
            {
                
                // Logica do programa
                string cep = etCep.Text.Trim();

                // Validações
                if (EhCep(cep))
                {
                    Endereco end = await ViaCepServico.BuscarEnderecoViaCEP(cep);

                    if (end == null)
                        throw new Exception("O endereço não foi encontrado para o CEP informado.");

                    string resultado = string.Format("Endereço: {0}, {1}, {2}, {3}", end.logradouro, end.bairro, end.localidade, end.uf);

                    lblResultado.Text = resultado;
                }
            }
            catch (Exception erro)
            {
                await DisplayAlert("Erro", erro.Message, "OK");
            }
        }

        private bool EhCep(string cep)
        {
            bool valido = true;

            if (cep.Length != 8)
            {
                valido = false;
                throw new Exception("CEP inválido! O CEP deve conter 8 caracteres.");
            }

            int novoCep = 0;

            if (!int.TryParse(cep, out novoCep))
            {
                valido = false;
                throw new Exception("CEP inválido! O CEP deve ser composto apenas por números.");
            }

            return valido;
        }
    }
}
