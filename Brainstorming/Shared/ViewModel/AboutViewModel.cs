using System;
using System.Diagnostics;
using System.Reflection;
using Shared.ViewModel;

namespace Koopakiller.Apps.Brainstorming.Shared.ViewModel
{
    public class AboutViewModel : MessageViewModelBase<AboutViewModel>
    {
        public AboutViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.Title = "Test Title";
                this.Description = "Test Description";
                this.Company = "Test Company";
                this.Product = "Test Product";
                this.Copyright = "Test Copyright";

                this.AssemblyVersion = new Version(1, 2, 3, 4);
                this.FileVersion = "Test FileVersion";
                this.ProductVersion = "Test ProductVersion";
            }
            else
            {
                var asm = Assembly.GetExecutingAssembly();
                var asmName = asm.GetName();

                this.Title = asm.GetCustomAttribute<AssemblyTitleAttribute>().Title;
                this.Description = asm.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;
                this.Company = asm.GetCustomAttribute<AssemblyCompanyAttribute>().Company;
                this.Product = asm.GetCustomAttribute<AssemblyProductAttribute>().Product;
                this.Copyright = asm.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;

                this.AssemblyVersion = asmName.Version;
                this.FileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
                this.ProductVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
            }
        }

        public string Title { get; }
        public string Description { get; }
        public string Company { get; }
        public string Product { get; }
        public string Copyright { get; }

        public Version AssemblyVersion { get; }
        public string FileVersion { get; }
        public string ProductVersion { get; }
    }
}
