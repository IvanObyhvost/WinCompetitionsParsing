using System.Windows;
using WinCompetitionsParsing.BL.Services.Abstract;
using WinCompetitionsParsing.BL.Services.Implemenrtation;
using WinCompetitionsParsing.DAL.Repositories.Abstract;
using WinCompetitionsParsing.DAL.Repositories.Implementation;
using SimpleInjector;
using WinCompetitionsParsing.DAL.Domain;
using AutoMapper;
using WinCompetitionsParsing.BL.Models;

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
            var config = new AutoMapperConfiguration().Configure();
            //container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            // Register your types, for instance:
            container.Register<IProductRepository, ProductRepository>();
            container.Register<IProductService, ProductService>();

            container.RegisterSingleton<MapperConfiguration>(config);
            container.Register<IMapper>(() => config.CreateMapper(container.GetInstance));

            // Register your windows and view models:
            container.Register<MainWindow>();
            
            container.Verify();

            return container;
        }

        
    }

    public class MappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ProductModel, Product>().ReverseMap();
        }
    }

    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            return config;
        }
    }
}
