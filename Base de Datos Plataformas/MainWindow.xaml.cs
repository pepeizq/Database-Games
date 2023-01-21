using APIs;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;

namespace Base_de_Datos_Plataformas
{
    //https://github.com/wTonyChen/Uplay-AppID-List/blob/master/UplayAppIDs.json
    //https://github.com/Haoose/UPLAY_GAME_ID

    //https://steamcommunity.com/sharedfiles/filedetails/?id=1113049716

    //https://store-content.ak.epicgames.com/api/en-US/content/store
    //https://store-content.ak.epicgames.com/api/en-US/content/products/ + nameurl
    //https://store-content.ak.epicgames.com/api/content/productmapping

    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            string battlenetBD = LeerFicheroDentroAplicacion("ms-appx:///Jsons/Battlenet.json");
            List<BattlenetAPI> battlenetJson = JsonConvert.DeserializeObject<List<BattlenetAPI>>(battlenetBD);
            tbBattlenetAPI.Text = battlenetJson.Count.ToString() + " juegos detectados en Battle.net";

            string epicgamesBD = LeerFicheroDentroAplicacion("ms-appx:///Jsons/EpicGames.json");
            List<EpicGamesAPI> epicgamesJson = JsonConvert.DeserializeObject<List<EpicGamesAPI>>(epicgamesBD);
            tbEpicGamesAPI.Text = epicgamesJson.Count.ToString() + " juegos detectados en Epic Games";

            string ubisoftBD = LeerFicheroDentroAplicacion("ms-appx:///Jsons/Ubisoft.json");
            List<UbisoftAPI> ubisoftJson = JsonConvert.DeserializeObject<List<UbisoftAPI>>(ubisoftBD);
            tbUbisoftAPI.Text = ubisoftJson.Count.ToString() + " juegos detectados en Ubisoft";

            cbSeleccionarPlataforma.SelectionChanged += CambiarPlataformaSelectionChanged;
        }

        public void CambiarPlataformaSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gvJuegos.Items.Clear();

            List<string> imagenes = new List<string>();

            if (cbSeleccionarPlataforma.SelectedIndex == 0)
            {
                string battlenetBD = LeerFicheroDentroAplicacion("ms-appx:///Jsons/Battlenet.json");
                List<BattlenetAPI> battlenetJson = JsonConvert.DeserializeObject<List<BattlenetAPI>>(battlenetBD);

                foreach (BattlenetAPI juego in battlenetJson)
                {
                    string imagen = null;

                    if (juego.imagenGrande != string.Empty)
                    {
                        imagen = juego.imagenGrande;
                    }
                    else
                    {
                        if (juego.idSteam != string.Empty)
                        {
                            imagen = "https://cdn.cloudflare.steamstatic.com/steam/apps/" + juego.idSteam + "/library_600x900.jpg";
                        }
                    }

                    imagenes.Add(imagen);
                }
            }
            else if (cbSeleccionarPlataforma.SelectedIndex == 1)
            {
                string epicgamesBD = LeerFicheroDentroAplicacion("ms-appx:///Jsons/EpicGames.json");
                List<EpicGamesAPI> epicgamesJson = JsonConvert.DeserializeObject<List<EpicGamesAPI>>(epicgamesBD);

                foreach (EpicGamesAPI juego in epicgamesJson)
                {
                    string imagen = null;

                    if (juego.imagenGrande != string.Empty)
                    {
                        imagen = juego.imagenGrande;
                    }
                    else
                    {
                        if (juego.idSteam != string.Empty)
                        {
                            imagen = "https://cdn.cloudflare.steamstatic.com/steam/apps/" + juego.idSteam + "/library_600x900.jpg";
                        }
                    }

                    imagenes.Add(imagen);
                }
            }
            else if (cbSeleccionarPlataforma.SelectedIndex == 2)
            {
                string ubisoftBD = LeerFicheroDentroAplicacion("ms-appx:///Jsons/Ubisoft.json");
                List<UbisoftAPI> ubisoftJson = JsonConvert.DeserializeObject<List<UbisoftAPI>>(ubisoftBD);

                foreach (UbisoftAPI juego in ubisoftJson)
                {
                    string imagen = null;

                    if (juego.imagenGrande != string.Empty)
                    {
                        imagen = juego.imagenGrande;
                    }
                    else
                    {
                        if (juego.idSteam != string.Empty)
                        {
                            imagen = "https://cdn.cloudflare.steamstatic.com/steam/apps/" + juego.idSteam + "/library_600x900.jpg";
                        }
                    }

                    imagenes.Add(imagen);
                }
            }

            if (imagenes.Count > 0)
            {
                foreach (string imagen in imagenes)
                {
                    ImageEx imagenJuego = new ImageEx
                    {
                        IsCacheEnabled = true,
                        EnableLazyLoading = true,
                        Stretch = Stretch.UniformToFill,
                        Source = imagen
                    };

                    Button botonJuego = new Button
                    {
                        Content = imagenJuego,
                        Margin = new Thickness(5),
                        Padding = new Thickness(0),
                        MaxWidth = 300
                    };

                    gvJuegos.Items.Add(botonJuego);
                }
            }
        }

        public static string LeerFicheroDentroAplicacion(string enlaceFichero)
        {
            Uri enlace = new Uri(enlaceFichero);
            StorageFile fichero = StorageFile.GetFileFromApplicationUriAsync(enlace).AsTask().Result;
            return FileIO.ReadTextAsync(fichero).AsTask().Result;
        }
    } 
}
