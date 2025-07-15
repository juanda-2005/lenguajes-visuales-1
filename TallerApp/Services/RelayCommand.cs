using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

< Window x: Class = "TallerApp.Views.LoginWindow"
        xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns: x = "http://schemas.microsoft.com/winfx/2006/xaml"
        Title = "Login - Taller App" Height = "350" Width = "450"
        WindowStartupLocation = "CenterScreen" ResizeMode = "NoResize" >
    < Grid Margin = "20" >
        < Grid.RowDefinitions >
            < RowDefinition Height = "Auto" />
            < RowDefinition Height = "20" />
            < RowDefinition Height = "Auto" />
            < RowDefinition Height = "Auto" />
            < RowDefinition Height = "20" />
            < RowDefinition Height = "Auto" />
            < RowDefinition Height = "Auto" />
            < RowDefinition Height = "20" />
            < RowDefinition Height = "Auto" />
            < RowDefinition Height = "Auto" />
        </ Grid.RowDefinitions >


        < TextBlock Grid.Row = "0" Text = "SISTEMA DE GESTIÓN - TALLER"
                   FontSize = "16" FontWeight = "Bold" HorizontalAlignment = "Center" />


        < TextBlock Grid.Row = "2" Text = "Usuario:" FontWeight = "Bold" />
        < TextBox Grid.Row = "3" Name = "txtUsuario" Height = "25" Margin = "0,5,0,0" />


        < TextBlock Grid.Row = "5" Text = "Contraseña:" FontWeight = "Bold" />
        < PasswordBox Grid.Row = "6" Name = "txtPassword" Height = "25" Margin = "0,5,0,0" />


        < StackPanel Grid.Row = "8" Orientation = "Horizontal" HorizontalAlignment = "Center" >
            < Button Name = "btnLogin" Content = "Iniciar Sesión" Width = "120" Height = "30"
                    Margin = "5" Click = "btnLogin_Click" />
            < Button Name = "btnRegistrar" Content = "Registrarse" Width = "120" Height = "30"
                    Margin = "5" Click = "btnRegistrar_Click" />
        </ StackPanel >


        < TextBlock Grid.Row = "9" Name = "lblMensaje" Foreground = "Red"
                   HorizontalAlignment = "Center" Margin = "0,10,0,0" />
    </ Grid >
</ Window >