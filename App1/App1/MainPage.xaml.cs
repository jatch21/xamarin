using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ingresar.Clicked += btnIngresar;
        }
        private void btnIngresar(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(usuario.Text) || String.IsNullOrEmpty(clave.Text)) {
                DisplayAlert("Mensaje", "Ingrese datos validos", "OK");
                return; }

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var response = httpClient.GetAsync("http://jatch21.pe.hu/api/login.php?usuario=" + usuario.Text + "&clave=" + clave.Text).Result;
            string content = response.Content.ReadAsStringAsync().Result;

            //[{"nombre":"jason"}] retorna un nombre si en caso existe el usuario con la respectiva clave
            var lista = JArray.Parse(content);
            if (lista.Count > 0)
                Navigation.PushModalAsync(new VistaUsuario(lista[0]["nombre"].ToString()));
            else DisplayAlert("Mensaje", "Su usuario o clave es incorrecto", "OK");
        }
    }
}
