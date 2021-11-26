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

            // ע�� ������
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
            // ���� api �������� Controller (��RegisterActionControllerAssemblyע�������)
            UrlSchemes.Add(new ("http", "api", null, UrlSchemeType.ResourceRequest));
            // ��̬�ļ���Դ ���� ������ wwwroot
            UrlSchemes.Add(new ("http", "app", "wwwroot", UrlSchemeType.HostToFolder));
            
            // ���� ��ҳ��ַ
            StartUrl = "http://app/index.html"; ;

            // ȥ�����ڱ�����
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

       
        // ������д ������������      
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