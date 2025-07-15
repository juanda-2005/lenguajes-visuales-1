using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TallerApp.Services;

namespace TallerApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Inicializar la base de datos si no existe
            using (var context = new TallerContext())
            {
                context.Database.CreateIfNotExists();
            }
        }
    }
}
