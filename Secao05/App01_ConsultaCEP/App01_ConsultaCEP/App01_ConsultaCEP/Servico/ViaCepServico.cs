using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using App01_ConsultaCEP.Servico.Modelo;
using Newtonsoft.Json;

namespace App01_ConsultaCEP.Servico
{
    public class ViaCepServico
    {
        private static string EnderecoURL = "https://viacep.com.br/ws/{0}/json/";

        public static async Task<Endereco> BuscarEnderecoViaCEP(string cep)
        {
            Endereco endereco = null;
            string novoEndenrecoURL = string.Format(EnderecoURL, cep);

            try
            {
                using (WebClient wc = new WebClient())
                {
                    string conteudo = await wc.DownloadStringTaskAsync(novoEndenrecoURL);
                    endereco = JsonConvert.DeserializeObject<Endereco>(conteudo);

                    if (endereco.cep == null) return null;
                }
            }
            catch (Exception erro)
            {
                throw erro;
            }

            return endereco;
        }
    }
}
