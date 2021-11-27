using System.ComponentModel;
using System.Windows.Forms;

using EdgeSharp.Core;
using EdgeSharp.Core.Configuration;
using EdgeSharp.Core.Defaults;
using EdgeSharp.Core.Infrastructure;
using EdgeSharp.WinForms;

using HelloEdgeSharp.Controller;

using Microsoft.Extensions.DependencyInjection;

namespace HelloEdgeSharp
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            try
            {
                Application.SetHighDpiMode(System.Windows.Forms.HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var appBuilder = new AppBuilder<Startup>();
                ServiceLocator.Bootstrap(appBuilder);
                var bowserForm = (BrowserForm)ServiceLocator.Current.GetInstance<IBrowserWindow>();
                Application.Run(bowserForm);
                appBuilder?.Stop();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

        }
    }

    public class Startup : WinFormsStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddSingleton<IConfiguration, SampleConfig>();

            services.AddSingleton<IBrowserWindow, SampleBrowserForm>();

            // 注入 控制器
            RegisterActionControllerAssembly(services, typeof(HelloController).Assembly);
        }

        public override void Initialize(IServiceProvider serviceProvider)
        {
            base.Initialize(serviceProvider);
        }
    }


    internal class SampleConfig : Configuration
    {
        public SampleConfig() : base()
        {
            // 拦截 api 并导航到 Controller (用RegisterActionControllerAssembly注册控制器)
            UrlSchemes.Add(new("http", "api", null, UrlSchemeType.ResourceRequest));
            // 静态文件资源 拦截 导航到 wwwroot
            UrlSchemes.Add(new("http", "app", "wwwroot", UrlSchemeType.HostToFolder));

            // 设置 首页地址
            StartUrl = "http://app/index.html"; ;

            // 去掉窗口标题栏
            //WindowOptions.Borderless = true;

        }
    }


    internal class SampleBrowserForm : BrowserForm
    {

        public SampleBrowserForm()
        {
            Width = 1200;
            Height = 900;

            var executable = System.Reflection.Assembly.GetExecutingAssembly();
            var iconStream = executable.GetManifestResourceStream("EdgeSharp.WinForms.Sample.edgesharp.ico");
            if (iconStream != null)
            {
                Icon = new Icon(iconStream);
            }
        }


        // 可以重写 各种生命周期      
        protected override void Bootstrap()
        {
            base.Bootstrap();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }
    }

}