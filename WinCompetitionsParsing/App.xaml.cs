﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MakeUpСompetition;
using MakeUpСompetition.BLL.Services.Abstract;
using MakeUpСompetition.BLL.Services.Implemenrtation;
using MakeUpСompetition.DAL.Repositories.Abstract;
using MakeUpСompetition.DAL.Repositories.Implementation;
using SimpleInjector;

namespace WinCompetitionsParsing
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var container = Bootstrap();
            MainWindow mainWindows = container.GetInstance<MainWindow>();
            mainWindows.Show();
            mainWindows.Closed += (s, a) => { Shutdown(); };
        }
        private static Container Bootstrap()
        {
            // Create the container as usual.
            var container = new Container();

            // Register your types, for instance:
            container.Register<IProductRepository, ProductRepository>();
            container.Register<IProductService, ProductService>();

            // Register your windows and view models:
            container.Register<MainWindow>();

            container.Verify();

            return container;
        }
    }
}
